﻿//HintName: TestNamespace.TestDto.g.cs
// <auto-generated />
#nullable enable

using System.Data;

namespace TestNamespace;

partial record TestDto
{
    public static global::TestNamespace.TestDto FromDataReader(IDataReader reader)
    {
        var i = -1;
        var instance = new global::TestNamespace.TestDto
        {
            Name = reader.IsDBNull(++i) ? null : reader.GetString(i),
            Name2 = reader.IsDBNull(++i) ? null : reader.GetString(i)
        };
        return instance;
    }

    public static IReadOnlyList<global::TestNamespace.TestDto> ListFromDataReader(IDataReader reader)
    {
        var results = new List<global::TestNamespace.TestDto>();

        while (reader.Read())
        {
            var instance = FromDataReader(reader);
            results.Add(instance);
        }

        return results;
    }
}
