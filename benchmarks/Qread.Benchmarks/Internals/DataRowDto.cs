using Bogus;

namespace Qread.Benchmarks.Internals;

[GenerateDataReader(IsExact = true)]
public sealed partial record DataRowDto
{
    public required bool? BoolProperty { get; init; }
    public required byte? ByteProperty { get; init; }
    public required char? CharProperty { get; init; }
    public required DateTimeOffset? DateTimeOffsetProperty { get; init; }
    public required DateTime? DateTimeProperty { get; init; }
    public required decimal? DecimalProperty { get; init; }
    public required double? DoubleProperty { get; init; }
    public required short? Int16Property { get; init; }
    public required int? Int32Property { get; init; }
    public required long? Int64Property { get; init; }
    public required string? StringProperty { get; init; }
    public required TimeSpan? TimeSpanProperty { get; init; }

    public enum BenchmarkEnum
    {
        Default,
    }

    public static IReadOnlyList<DataRowDto> Fake(int count) => Faker.Generate(count);

    private static readonly Faker<DataRowDto> Faker = new Faker<DataRowDto>()
        .StrictMode(true)
        .RuleFor(x => x.BoolProperty, f => f.Random.Bool().OrNull(f))
        .RuleFor(x => x.ByteProperty, f => f.Random.Byte().OrNull(f))
        .RuleFor(x => x.CharProperty, f => f.Random.Char().OrNull(f))
        .RuleFor(x => x.DateTimeOffsetProperty, f => f.Date.RecentOffset().OrNull(f))
        .RuleFor(x => x.DateTimeProperty, f => f.Date.Recent().OrNull(f))
        .RuleFor(x => x.DecimalProperty, f => f.Random.Decimal().OrNull(f))
        .RuleFor(x => x.DoubleProperty, f => f.Random.Double().OrNull(f))
        .RuleFor(x => x.Int16Property, f => f.Random.Short().OrNull(f))
        .RuleFor(x => x.Int32Property, f => f.Random.Char().OrNull(f))
        .RuleFor(x => x.Int64Property, f => f.Random.Char().OrNull(f))
        .RuleFor(x => x.StringProperty, f => f.Random.String2(10).OrNull(f))
        .RuleFor(x => x.TimeSpanProperty, f => f.Date.Timespan().OrNull(f));
}
