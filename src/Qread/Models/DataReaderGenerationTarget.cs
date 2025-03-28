using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Qread.Internals;

namespace Qread.Models;

internal readonly record struct DataReaderGenerationTarget
{
    public bool IsExact { get; }
    public string Namespace { get; } = "";
    public EquatableArray<string> Parents { get; } = [];
    public TypeInternal Type { get; }

    private DataReaderGenerationTarget(
        GeneratorAttributeSyntaxContext context,
        INamedTypeSymbol symbol
    )
    {
        var parents = new List<string>();
        var parent = symbol;

        do
        {
            parents.Insert(0, $"partial {parent.TypeKind().ToDeclaration()} {parent.Name}");
            parent = parent.ContainingType;
        } while (parent is not null);

        var isExact =
            context.GetGenerateDataReaderAttribute()?.GetNamedArg("IsExact")?.Value is true;
        Type = new TypeInternal(symbol);
        IsExact = isExact;
        Namespace = symbol.ContainingNamespace.ToDisplayString();
        Parents = parents.ToImmutableArray();
    }

    public static bool TryCreate(
        GeneratorAttributeSyntaxContext context,
        out DataReaderGenerationTarget? target
    )
    {
        target = null;
        var typeDeclarationSyntax = (TypeDeclarationSyntax)context.TargetNode;

        if (!IsNamedTypeSymbol(context, typeDeclarationSyntax, out var namedTypeSymbol))
            return false;

        if (!HasParameterlessConstructor(namedTypeSymbol))
            return false;

        target = new DataReaderGenerationTarget(context, namedTypeSymbol);
        return true;
    }

    private static bool HasParameterlessConstructor(INamedTypeSymbol symbol)
    {
        foreach (var constructor in symbol.Constructors)
            if (constructor.Parameters.Length == 0)
                return true;

        return false;
    }

    private static bool IsNamedTypeSymbol(
        GeneratorAttributeSyntaxContext context,
        TypeDeclarationSyntax typeDeclarationSyntax,
        out INamedTypeSymbol symbol
    )
    {
        symbol = null!;

        if (
            context.SemanticModel.GetDeclaredSymbol(typeDeclarationSyntax)
            is not INamedTypeSymbol cs
        )
            return false;

        symbol = cs;
        return true;
    }
}
