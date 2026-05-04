using System.Data;

namespace Qread;

public interface ITypeReader
{
    object? Read(IDataReader reader, int index);
}
