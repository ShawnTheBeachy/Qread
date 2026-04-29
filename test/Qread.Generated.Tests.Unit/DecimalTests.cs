using System.Data;

namespace Qread.Generated.Tests.Unit;

public sealed partial class DecimalTests
{
    [Test]
    public async Task NotNullableDecimal_ShouldBeSet()
    {
        // Arrange.
        var dataReader = IDataReader.Imposter();
        dataReader.GetDecimal(0).Returns(37_000.67M);

        // Act.
        var sut = Compensation.FromDataReader(dataReader.Instance());

        // Assert.
        await Assert.That(sut.AllTime).IsEqualTo(37_000.67M);
    }

    [Test]
    public async Task NullableDecimal_ShouldBeSetToNull_WhenColumnIsNull()
    {
        // Arrange.
        var dataReader = IDataReader.Imposter();
        dataReader.IsDBNull(1).Returns(true);

        // Act.
        var sut = Compensation.FromDataReader(dataReader.Instance());

        // Assert.
        await Assert.That(sut.ThisQuarter).IsNull();
    }

    [Test]
    public async Task NullableDecimal_ShouldBeSetToValue_WhenColumnIsNotNull()
    {
        // Arrange.
        var dataReader = IDataReader.Imposter();
        dataReader.IsDBNull(1).Returns(false);
        dataReader.GetDecimal(1).Returns(6_532.48M);

        // Act.
        var sut = Compensation.FromDataReader(dataReader.Instance());

        // Assert.
        await Assert.That(sut.ThisQuarter).IsEqualTo(6_532.48M);
    }

    [GenerateDataReader(IsExact = true)]
    private sealed partial record Compensation
    {
        public required decimal AllTime { get; init; }
        public required decimal? ThisQuarter { get; init; }
    }
}
