using System.Collections;
using System.Data;

namespace Qread.Benchmarks.Internals;

internal sealed class BenchmarkDataParameterCollection : IDataParameterCollection
{
    public int Count => 0;
    public bool IsFixedSize => true;
    public bool IsReadOnly => true;
    public bool IsSynchronized => true;
    public object SyncRoot => null!;

    public void CopyTo(Array array, int index) { }

    public int Add(object? value) => 0;

    public void Clear() { }

    public bool Contains(object? value) => false;

    public bool Contains(string parameterName) => false;

    public IEnumerator GetEnumerator() => Array.Empty<string>().GetEnumerator();

    public int IndexOf(object? value) => 0;

    public int IndexOf(string parameterName) => 0;

    public void Insert(int index, object? value) { }

    public void Remove(object? value) { }

    public void RemoveAt(int index) { }

    public void RemoveAt(string parameterName) { }

    public object? this[int index]
    {
        get => null;
        set { }
    }

    public object this[string parameterName]
    {
        get => null!;
        set { }
    }
}
