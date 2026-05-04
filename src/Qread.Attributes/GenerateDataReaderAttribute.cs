namespace Qread;

/// <summary>
/// Instructs Qread to generate an <c>IDataReader</c> extension method.
/// </summary>
[System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct)]
public sealed class GenerateDataReaderAttribute : System.Attribute
{
    public bool IsExact { get; set; }
}
