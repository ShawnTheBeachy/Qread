using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Dapper;
using Qread.Benchmarks.Internals;

namespace Qread.Benchmarks;

[SimpleJob(RuntimeMoniker.Net90)]
[MemoryDiagnoser]
public class Benchmark
{
    private BenchmarkDataReader _dataReader = null!;
    private BenchmarkDbConnection _dbConnection = null!;

    [Params(1_000, 5_000, 10_000)]
    public int Rows { get; set; }

    [Benchmark]
    public List<DataRowDto> Dapper()
    {
        var results = _dbConnection.Query<DataRowDto>("");
        return results.ToList();
    }

    [Benchmark]
    public List<DataRowDto> Mappy()
    {
        var results = new global::Mappy.Mappy().Map<DataRowDto>(_dataReader.Dynamics);
        return results.ToList();
    }

    [Benchmark]
    public async Task<List<DataRowDto>> Qread()
    {
        var results = new List<DataRowDto>();

        await foreach (
            var dto in DataRowDto.AsyncEnumerableFromDataReader(_dataReader, CancellationToken.None)
        )
            results.Add(dto);

        return results;
    }

    [GlobalSetup]
    public void Setup()
    {
        _dataReader = new BenchmarkDataReader(Rows);
        _dbConnection = new BenchmarkDbConnection(_dataReader);
    }
}
