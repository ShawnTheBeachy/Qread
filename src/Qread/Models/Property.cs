using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Qread.Internals;

namespace Qread.Models;

internal readonly record struct Property
{
    public string FullyQualifiedTypeName { get; }
    public bool IsEnum { get; }
    public bool IsNullable { get; }
    public string Name { get; }
    public EquatableArray<Property> Properties { get; }
    public string TypeName { get; }

    public Property(IPropertySymbol symbol)
    {
        var type = TryGetNullableValueUnderlyingType(symbol, out var underlying)
            ? underlying
            : symbol.Type;
        TypeName = type!.Name;
        IsEnum = type is INamedTypeSymbol { EnumUnderlyingType: not null };
        IsNullable = IsPropNullable(symbol);
        Name = symbol.Name;
        FullyQualifiedTypeName = symbol.Type.ToDisplayString();
        Properties = type is INamedTypeSymbol nts
            ? nts.GetProperties().ToImmutableArray()
            : ImmutableArray<Property>.Empty;
    }

    private static bool IsPropNullable(IPropertySymbol prop) =>
        prop.NullableAnnotation == NullableAnnotation.Annotated;

    private static bool TryGetNullableValueUnderlyingType(
        IPropertySymbol prop,
        out ITypeSymbol? underlyingType
    )
    {
        underlyingType = null;

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
