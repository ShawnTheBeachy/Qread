using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Qread.Internals;

namespace Qread.Models;

internal sealed record TypeInternal
{
    public EquatableArray<TypeContainer> Containers { get; } = [];
    public string FullName { get; }
    public string FullNameIgnoreNullable =>
        FullName[FullName.Length - 1] == '?'
            ? FullName.Substring(0, FullName.Length - 1)
            : FullName;
    public bool IsEnum { get; }
    public string Name { get; }
    public EquatableArray<Property> Properties { get; }

    public TypeInternal(ITypeSymbol symbol)
    {
        FullName = symbol.ToDisplayString();
        IsEnum = symbol is INamedTypeSymbol { EnumUnderlyingType: not null };
        Name = symbol.Name;
        Properties = symbol.GetProperties().ToImmutableArray();

        var parents = new List<TypeContainer>();
        var parent = symbol;

        do
        {
            parents.Insert(0, new TypeContainer(parent));
            parent = parent.ContainingType;
        } while (parent is not null);

        Containers = parents.ToImmutableArray();
    }

    public IEnumerable<Property> DeepProperties()
    {
        return GetProps(this);

        IEnumerable<Property> GetProps(TypeInternal type)
        {
            foreach (var prop in type.Properties)
            {
                yield return prop;

                foreach (var child in GetProps(prop.Type))
                    yield return child;
            }
        }
    }
}
