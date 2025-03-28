using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Qread.Internals;

namespace Qread.Models;

internal sealed record TypeInternal
{
    public string FullName { get; }
    public bool IsEnum { get; }
    public string Name { get; }
    public EquatableArray<Property> Properties { get; }
    public TypeKindInternal TypeKind { get; }

    public TypeInternal(ITypeSymbol symbol)
    {
        FullName = symbol.ToDisplayString();
        IsEnum = symbol is INamedTypeSymbol { EnumUnderlyingType: not null };
        Name = symbol.Name;
        Properties = symbol.GetProperties().ToImmutableArray();
        TypeKind =
            symbol.TypeKind == Microsoft.CodeAnalysis.TypeKind.Struct ? TypeKindInternal.Struct
            : symbol.IsRecord ? TypeKindInternal.Record
            : TypeKindInternal.Class;
    }
}
