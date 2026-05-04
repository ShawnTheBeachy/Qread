using System.Data;

namespace Qread;

/// <summary>
/// Reads a field from an <c>IDataReader</c>.
/// </summary>
public interface ITypeReader
{
    object? Read(IDataReader reader, int index);
}
