namespace Qread;

[System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct)]
public sealed class GenerateDataReaderAttribute : System.Attribute
{
    public bool IsExact { get; set; }
}
