using System;
using Microsoft.CodeAnalysis;
using Qread.Internals;

namespace Qread.Models;

internal readonly record struct Property
{
    public DbTypeInternal? DbType { get; }
    public bool IsArray { get; }
    public bool IsNullable { get; }
    public string Name { get; }
    public TypeInternal Type { get; }

    public Property(IPropertySymbol symbol)
    {
        IsNullable = IsPropNullable(symbol);
        Name = symbol.Name;
        var type = TryGetNullableValueUnderlyingType(symbol, out var underlying)
            ? underlying
            : symbol.Type;
        Type = new TypeInternal(type);
        IsArray = type.TypeKind == TypeKind.Array;

        if (type is IArrayTypeSymbol arrayTypeSymbol)
            type = arrayTypeSymbol.ElementType;

        DbType = type.Name switch
        {
            nameof(Boolean) => DbTypeInternal.Bool,
            nameof(Byte) => DbTypeInternal.Byte,
            nameof(Char) => DbTypeInternal.Char,
            "DateOnly" => DbTypeInternal.DateOnly,
            nameof(DateTime) => DbTypeInternal.DateTime,
            nameof(DateTimeOffset) => DbTypeInternal.DateTimeOffset,
            nameof(Decimal) => DbTypeInternal.Decimal,
            nameof(Double) => DbTypeInternal.Double,
            nameof(Single) => DbTypeInternal.Single,
            nameof(Guid) => DbTypeInternal.Guid,
            nameof(Int16) => DbTypeInternal.Int16,
            nameof(Int32) => DbTypeInternal.Int32,
            nameof(Int64) => DbTypeInternal.Int64,
            nameof(String) => DbTypeInternal.String,
            nameof(TimeSpan) => DbTypeInternal.TimeSpan,
            _ => null,
        };
    }

    public override int GetHashCode() => Type.FullNameIgnoreNullable.GetHashCode();

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
