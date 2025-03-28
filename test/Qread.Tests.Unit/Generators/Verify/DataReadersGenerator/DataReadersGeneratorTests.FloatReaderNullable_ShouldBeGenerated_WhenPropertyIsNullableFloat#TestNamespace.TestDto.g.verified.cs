﻿//HintName: TestNamespace.TestDto.g.cs
// <auto-generated />
#nullable enable

using System.Data;

namespace TestNamespace;

partial record TestDto
{
    public static TestDto FromDataReader(IDataReader reader)
    {
        var i = -1;
        var instance = new TestDto
        {
            Value = reader.IsDBNull(++i) ? null : reader.GetFloat(i),
            Value2 = reader.IsDBNull(++i) ? null : reader.GetFloat(i)
        };
        return instance;
    }

    public static IReadOnlyList<TestDto> ListFromDataReader(IDataReader reader)
    {
        var results = new List<TestDto>();

        while (reader.Read())
        {
            var instance = FromDataReader(reader);
            results.Add(instance);
        }

        return results;
    }
}
