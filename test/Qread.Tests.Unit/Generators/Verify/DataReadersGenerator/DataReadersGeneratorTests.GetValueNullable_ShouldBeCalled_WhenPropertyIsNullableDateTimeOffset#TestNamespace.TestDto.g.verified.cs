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
            DateOfBirth = reader.IsDBNull(++i) ? null : (DateTimeOffset)reader.GetValue(i)
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
