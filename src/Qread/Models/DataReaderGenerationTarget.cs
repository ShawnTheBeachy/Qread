using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Qread.Internals;

namespace Qread.Models;

internal readonly record struct DataReaderGenerationTarget
{
    public bool IsExact { get; }
    public string Namespace { get; } = "";
    public TypeInternal Type { get; }

    private DataReaderGenerationTarget(
        GeneratorAttributeSyntaxContext context,
        INamedTypeSymbol symbol,
        TypeCache typeCache
    )
    {
        Type = typeCache.TryGetType(symbol, out var typeInternal)
            ? typeInternal!
            : typeCache.CacheType(symbol, new TypeInternal(symbol));
        IsExact = context.GetGenerateDataReaderAttribute()?.GetNamedArg("IsExact")?.Value is true;
        Namespace = symbol.ContainingNamespace.ToDisplayString();
    }

    public static bool TryCreate(
        GeneratorAttributeSyntaxContext context,
        TypeCache typeCache,
        out DataReaderGenerationTarget? target
    )
    {
        target = null;
        var typeDeclarationSyntax = (TypeDeclarationSyntax)context.TargetNode;

        if (!IsNamedTypeSymbol(context, typeDeclarationSyntax, out var namedTypeSymbol))
            return false;

        if (!HasParameterlessConstructor(namedTypeSymbol))
            return false;

        target = new DataReaderGenerationTarget(context, namedTypeSymbol, typeCache);
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
