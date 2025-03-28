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
            Value = reader.GetChar(++i),
            Value2 = reader.GetChar(++i)
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
