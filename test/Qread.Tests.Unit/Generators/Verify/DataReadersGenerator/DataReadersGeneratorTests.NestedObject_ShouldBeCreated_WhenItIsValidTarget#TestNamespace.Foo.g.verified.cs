﻿//HintName: TestNamespace.Foo.g.cs
// <auto-generated />
#nullable enable

using System.Data;

namespace TestNamespace;

partial record Foo
{
    public static global::TestNamespace.Foo FromDataReader(IDataReader reader)
    {
        var i = -1;
        var instance = new global::TestNamespace.Foo
        {
            FirstValue = reader.GetString(++i),
            SecondValue = global::TestNamespace.Bar.FromDataReader(reader, ref i)!
        };
        return instance;
    }

    public static IReadOnlyList<global::TestNamespace.Foo> ListFromDataReader(IDataReader reader)
    {
        var results = new List<global::TestNamespace.Foo>();

        while (reader.Read())
        {
            var instance = FromDataReader(reader);
            results.Add(instance);
        }

        return results;
    }
}

partial record Bar
{
    public static global::TestNamespace.Bar? FromDataReader(IDataReader reader, ref int i)
    {
        var isAnyValueNotNull = false;

        for (var j = 1; j < 2; j++)
        {
            if (!reader.IsDBNull(i + j))
            {
                isAnyValueNotNull = true;
                break;
            }
        }

        if (!isAnyValueNotNull)
        {
            i += 1;
            return null;
        }

        var instance = new global::TestNamespace.Bar
        {
            Value = reader.GetString(++i)
        };
        return instance;
    }
}
