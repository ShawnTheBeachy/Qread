using System;

namespace Qread;

/// <summary>
/// Instructs Qread to ignore a property.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class IgnoreAttribute : Attribute;
