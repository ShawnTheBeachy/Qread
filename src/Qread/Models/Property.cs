using Microsoft.CodeAnalysis;

namespace Qread.Models;

internal readonly record struct Property
{
    public bool IsNullable { get; }
    public string Name { get; }

    public TypeInternal Type { get; }

    public Property(IPropertySymbol symbol)
    {
        var type = TryGetNullableValueUnderlyingType(symbol, out var underlying)
            ? underlying
            : symbol.Type;
        Type = new TypeInternal(type);
        IsNullable = IsPropNullable(symbol);
        Name = symbol.Name;
    }

    private static bool IsPropNullable(IPropertySymbol prop) =>
        prop.NullableAnnotation == NullableAnnotation.Annotated;

    private static bool TryGetNullableValueUnderlyingType(
        IPropertySymbol prop,
        out ITypeSymbol underlyingType
    )
    {
        underlyingType = null!;

        if (
            prop.Type is not INamedTypeSymbol namedType
            || !IsPropNullable(prop)
            || !namedType.IsGenericType
        )
            return false;

        var typeParameters = namedType.TypeArguments;

        if (
            namedType.ConstructUnboundGenericType() is not { Name: "Nullable" } genericType
            || genericType.ContainingNamespace.Name != "System"
            || genericType.TypeArguments.Length != 1
            || typeParameters.Length != 1
        )
            return false;

        underlyingType = typeParameters[0];
        return true;
    }
}
