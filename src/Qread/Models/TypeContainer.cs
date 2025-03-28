using Microsoft.CodeAnalysis;
using Qread.Internals;

namespace Qread.Models;

internal readonly record struct TypeContainer
{
    public string Name { get; }
    public TypeKindInternal TypeKind { get; }

    public TypeContainer(ITypeSymbol symbol)
    {
        Name = symbol.Name;
        TypeKind = symbol.TypeKind();
    }
}
