using System.Data;

namespace Qread;

/// <summary>
/// Reads a field from an <c>IDataReader</c>.
/// </summary>
/// <typeparam name="T">The type of the field.</typeparam>
public abstract class TypeReader<T> : ITypeReader
{
    public abstract T Read(IDataReader reader, int index);

    object? ITypeReader.Read(IDataReader reader, int index) => Read(reader, index);
}
