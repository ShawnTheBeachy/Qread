using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Qread.Internals;

namespace Qread.Models;

internal sealed record DataReaderGenerationTarget
{
    public bool IsExact { get; set; }
    public string Name { get; set; } = "";
    public string Namespace { get; set; } = "";
    public EquatableArray<string> Parents { get; set; } = [];
    public EquatableArray<Property> Properties { get; set; } = [];
    public GenerationTargetType Type { get; set; }

    public static DataReaderGenerationTarget? FromContext(GeneratorAttributeSyntaxContext context)
    {
        var typeDeclarationSyntax = (TypeDeclarationSyntax)context.TargetNode;

        if (!IsNamedTypeSymbol(context, typeDeclarationSyntax, out var namedTypeSymbol))
            return null;

        if (!HasParameterlessConstructor(namedTypeSymbol!))
            return null;

        var properties = GetProperties(namedTypeSymbol!, typeDeclarationSyntax).ToImmutableArray();

        var parents = new List<string>();
        var parent = namedTypeSymbol!;

        do
        {
            var typeText =
                parent.TypeKind == TypeKind.Struct
                    ? parent.IsRecord
                        ? "record struct"
                        : "struct"
                    : parent.IsRecord
                        ? "record"
                        : "class";
            parents.Insert(
                0,
                $"{(parent.IsStatic ? "static " : "")}partial {typeText} {parent.Name}"
            );
            parent = parent.ContainingType;
        } while (parent is not null);

        var isExact =
            context.GetGenerateDataReaderAttribute()?.GetNamedArg("IsExact")?.Value is true;
        return new DataReaderGenerationTarget
        {
            IsExact = isExact,
            Name = typeDeclarationSyntax.Identifier.Text,
            Namespace = namedTypeSymbol!.ContainingNamespace.ToDisplayString(),
            Parents = parents.ToImmutableArray(),
            Properties = properties,
            Type =
                typeDeclarationSyntax is StructDeclarationSyntax ? GenerationTargetType.Struct
                : namedTypeSymbol.IsRecord ? GenerationTargetType.Record
                : GenerationTargetType.Class,
        };
    }

    private static IEnumerable<Property> GetProperties(
        INamedTypeSymbol classSymbol,
        TypeDeclarationSyntax typeSyntax
    )
    {
        while (true)
        {
            foreach (
                var prop in classSymbol
                    .GetMembers()
                    .OfType<IPropertySymbol>()
                    .Where(x => x.DeclaredAccessibility == Accessibility.Public)
                    .Where(x =>
                        typeSyntax is not RecordDeclarationSyntax || x.Name != "EqualityContract"
                    )
                    .Select(x => new Property(x))
            )
                yield return prop;

            if (classSymbol.BaseType is null)
                yield break;

            classSymbol = classSymbol.BaseType;
        }
    }

    private static bool HasParameterlessConstructor(INamedTypeSymbol typeSymbol) =>
        typeSymbol.Constructors.Any(x => x.Parameters.Length == 0);

    private static bool IsNamedTypeSymbol(
        GeneratorAttributeSyntaxContext context,
        TypeDeclarationSyntax typeDeclarationSyntax,
        out INamedTypeSymbol? symbol
    )
    {
        symbol = null;

        if (
            context.SemanticModel.GetDeclaredSymbol(typeDeclarationSyntax)
            is not INamedTypeSymbol cs
        )
            return false;

        symbol = cs;
        return true;
    }

    public enum GenerationTargetType
    {
        Class,
        Record,
        Struct,
    }
}
