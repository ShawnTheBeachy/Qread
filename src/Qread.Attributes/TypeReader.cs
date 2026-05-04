using System.Data;

namespace Qread;

public abstract class TypeReader<T> : ITypeReader
{
    public abstract T Read(IDataReader reader, int index);

    object? ITypeReader.Read(IDataReader reader, int index) => Read(reader, index);
}
