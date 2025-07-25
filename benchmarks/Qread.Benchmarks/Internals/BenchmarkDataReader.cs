using System.Data;

namespace Qread.Benchmarks.Internals;

internal sealed class BenchmarkDataReader : IDataReader
{
    private int _currentIndex;
    private readonly IReadOnlyList<string> _names =
    [
        "BoolProperty",
        "ByteProperty",
        "CharProperty",
        "DateTimeOffsetProperty",
        "DateTimeProperty",
        "DecimalProperty",
        "DoubleProperty",
        "Int16Property",
        "Int32Property",
        "Int64Property",
        "StringProperty",
        "TimeSpanProperty",
    ];

    private readonly IReadOnlyList<object?>[] _values;

    public int Depth => 0;

    public IReadOnlyList<dynamic> Dynamics { get; }
    public int FieldCount => _names.Count;
    public bool IsClosed => false;
    public int RecordsAffected => _values.Length;

    public BenchmarkDataReader(int rowCount)
    {
        _values = DataRowDto
            .Fake(rowCount)
            .Select<DataRowDto, IReadOnlyList<object?>>(x =>
                [
                    x.BoolProperty,
                    x.ByteProperty,
                    x.CharProperty,
                    x.DateTimeOffsetProperty,
                    x.DateTimeProperty,
                    x.DecimalProperty,
                    x.DoubleProperty,
                    x.Int16Property,
                    x.Int32Property,
                    x.Int64Property,
                    x.StringProperty,
                    x.TimeSpanProperty,
                ]
            )
            .ToArray();
        Dynamics = _values
            .Select(x => new
            {
                BoolProperty = x[0],
                ByteProperty = x[1],
                CharProperty = x[2],
                DateTimeOffsetProperty = x[3],
                DateTimeProperty = x[4],
                DecimalProperty = x[5],
                DoubleProperty = x[6],
                Int16Property = x[7],
                Int32Property = x[8],
                Int64Property = x[9],
                StringProperty = x[10],
                TimeSpanProperty = x[11],
            })
            .ToArray();
    }

    public void Close() => _currentIndex = -1;

    public void Dispose() => Close();

    public bool GetBoolean(int i) => (bool)GetValue(i);

    public byte GetByte(int i) => (byte)GetValue(i);

    public long GetBytes(int i, long fieldOffset, byte[]? buffer, int bufferoffset, int length) =>
        throw new NotSupportedException();

    public char GetChar(int i) => (char)GetValue(i);

    public long GetChars(int i, long fieldoffset, char[]? buffer, int bufferoffset, int length) =>
        throw new NotSupportedException();

    public IDataReader GetData(int i) => this;

    public string GetDataTypeName(int i) =>
        i switch
        {
            0 => nameof(SqlDbType.Bit),
            1 => nameof(SqlDbType.Real),
            2 => nameof(SqlDbType.Char),
            3 => nameof(SqlDbType.DateTimeOffset),
            4 => nameof(SqlDbType.DateTime2),
            5 => nameof(SqlDbType.Decimal),
            6 => nameof(SqlDbType.Float),
            7 => nameof(SqlDbType.SmallInt),
            8 => nameof(SqlDbType.Int),
            9 => nameof(SqlDbType.BigInt),
            10 => nameof(SqlDbType.VarChar),
            11 => nameof(SqlDbType.Time),
            _ => throw new Exception(i.ToString()),
        };

    public DateTime GetDateTime(int i) => (DateTime)GetValue(i);

    public decimal GetDecimal(int i) => (decimal)GetValue(i);

    public double GetDouble(int i) => (double)GetValue(i);

    public Type GetFieldType(int i) =>
        i switch
        {
            0 => typeof(bool?),
            1 => typeof(byte?),
            2 => typeof(char?),
            3 => typeof(DateTimeOffset?),
            4 => typeof(DateTime?),
            5 => typeof(decimal?),
            6 => typeof(double?),
            7 => typeof(short?),
            8 => typeof(int?),
            9 => typeof(long?),
            10 => typeof(string),
            11 => typeof(TimeSpan?),
            _ => throw new Exception(i.ToString()),
        };

    public float GetFloat(int i) => (float)GetValue(i);

    public Guid GetGuid(int i) => (Guid)GetValue(i);

    public short GetInt16(int i) => (short)GetValue(i);

    public int GetInt32(int i) => (int)GetValue(i);

    public long GetInt64(int i) => (long)GetValue(i);

    public string GetName(int i) => _names[i];

    public int GetOrdinal(string name)
    {
        for (var i = 0; i < _names.Count; i++)
            if (_names[i] == name)
                return i;

        throw new Exception(name);
    }

    public DataTable? GetSchemaTable() => null;

    public string GetString(int i) => (string)GetValue(i);

    public object GetValue(int i) => _values[_currentIndex][i]!;

    public int GetValues(object[] values) => throw new NotSupportedException();

    public bool IsDBNull(int i) => _values[_currentIndex][i] is null;

    public bool NextResult() => Read();

    public bool Read()
    {
        if (_currentIndex >= RecordsAffected - 1)
            return false;

        _currentIndex++;
        return true;
    }

    public object this[int i] => GetValue(i);

    public object this[string name] => GetValue(GetOrdinal(name));
}
