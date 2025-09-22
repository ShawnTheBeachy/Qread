using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Qread.Internals;

namespace Qread.Models;

internal sealed record TypeInternal
{
    public bool CanConstruct { get; }
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

        if (symbol.TypeKind == TypeKind.Class && symbol is INamedTypeSymbol namedTypeSymbol)
        {
            var hasParameterlessConstructor = false;

            foreach (var constructor in namedTypeSymbol.Constructors)
                if (constructor.Parameters.Length == 0)
                {
                    hasParameterlessConstructor = true;
                    break;
                }

            CanConstruct = namedTypeSymbol.Arity == 0 && hasParameterlessConstructor;
        }
        else
            CanConstruct = false;

        var parents = new List<TypeContainer>();
        var parent = symbol;

        do
        {
            parents.Insert(0, new TypeContainer(parent));
            parent = parent.ContainingType;
        } while (parent is not null);

        Containers = parents.ToImmutableArray();
    }
}
