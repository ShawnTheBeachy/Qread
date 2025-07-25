using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Qread.Benchmarks.Internals;

internal sealed class BenchmarkDbConnection : IDbConnection
{
    private readonly BenchmarkDataReader _dataReader;

    [AllowNull]
    public string ConnectionString { get; set; }

    public int ConnectionTimeout => 30;
    public string Database => "Database";
    public ConnectionState State => ConnectionState.Open;

    public BenchmarkDbConnection(BenchmarkDataReader dataReader)
    {
        _dataReader = dataReader;
    }

    public IDbTransaction BeginTransaction() => null!;

    public IDbTransaction BeginTransaction(IsolationLevel il) => null!;

    public void ChangeDatabase(string databaseName) { }

    public void Close() { }

    public IDbCommand CreateCommand() => new BenchmarkDbCommand(_dataReader);

    public void Dispose() { }

    public void Open() { }
}
