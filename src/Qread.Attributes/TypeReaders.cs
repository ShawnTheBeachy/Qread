using System;
using System.Collections.Concurrent;

namespace Qread;

public static class TypeReaders
{
    private static readonly ConcurrentDictionary<Type, ITypeReader> Readers = [];

    public static void AddReader<T>(TypeReader<T> reader) => Readers[typeof(T)] = reader;

    public static bool TryGetReader<T>(out TypeReader<T>? reader)
    {
        reader = null;

        if (!Readers.TryGetValue(typeof(T), out var readerBase))
            return false;

        reader = (TypeReader<T>)readerBase;
        return true;
    }
}
