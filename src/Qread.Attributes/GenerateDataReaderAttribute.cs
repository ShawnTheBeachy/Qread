using System;

namespace Qread;

/// <summary>
/// Instructs Qread to generate an <c>IDataReader</c> extension method.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class GenerateDataReaderAttribute : Attribute
{
    public bool IsExact { get; set; }
}
