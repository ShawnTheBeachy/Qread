using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Qread.Benchmarks.Internals;

internal sealed class BenchmarkDbCommand : IDbCommand
{
    private readonly BenchmarkDataReader _dataReader;

    [AllowNull]
    public string CommandText { get; set; }
    public int CommandTimeout { get; set; }
    public CommandType CommandType { get; set; }
    public IDbConnection? Connection { get; set; }
    public IDataParameterCollection Parameters { get; } = new BenchmarkDataParameterCollection();
    public IDbTransaction? Transaction { get; set; }
    public UpdateRowSource UpdatedRowSource { get; set; }

    public BenchmarkDbCommand(BenchmarkDataReader dataReader)
    {
        _dataReader = dataReader;
    }

    public void Cancel() { }

    public IDbDataParameter CreateParameter() => null!;

    public void Dispose() { }

    public int ExecuteNonQuery() => throw new NotSupportedException();

    public IDataReader ExecuteReader() => _dataReader;

    public IDataReader ExecuteReader(CommandBehavior behavior) => ExecuteReader();

    public object ExecuteScalar() => throw new NotSupportedException();

    public void Prepare() { }
}
