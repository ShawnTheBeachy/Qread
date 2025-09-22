using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Qread.Models;

namespace Qread.Internals;

internal sealed class TypeCache
{
    private readonly Dictionary<string, TypeInternal> _types = [];

    public TypeInternal CacheType(ITypeSymbol typeSymbol, TypeInternal type)
    {
        _types[typeSymbol.ToDisplayString().TrimEnd('?')] = type;
        return type.Init(typeSymbol, this);
    }

    public bool TryGetType(ITypeSymbol typeSymbol, out TypeInternal? type) =>
        _types.TryGetValue(typeSymbol.ToDisplayString().TrimEnd('?'), out type);
}
