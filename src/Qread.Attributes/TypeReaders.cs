using System;
using System.Collections.Concurrent;

namespace Qread;

public static class TypeReaders
{
    private static readonly ConcurrentDictionary<Type, ITypeReader> Readers = [];

    /// <summary>
    /// Registers a reader for a type.
    /// </summary>
    /// <param name="reader">The reader to register.</param>
    /// <typeparam name="T">The type for which to register the reader..</typeparam>
    public static void AddReader<T>(TypeReader<T> reader) => Readers[typeof(T)] = reader;

    /// <summary>
    /// Gets the reader registered for a type, if one has been registered.
    /// </summary>
    /// <param name="reader">The registered reader.</param>
    /// <typeparam name="T">The type for which to find a registered reader.</typeparam>
    /// <returns><c>true</c> if a reader was found, otherwise <c>false</c>.</returns>
    public static bool TryGetReader<T>(out TypeReader<T>? reader)
    {
        reader = null;

        if (!Readers.TryGetValue(typeof(T), out var readerBase))
            return false;

        reader = (TypeReader<T>)readerBase;
        return true;
    }
}
