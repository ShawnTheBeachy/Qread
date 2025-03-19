using System.Data;
using NSubstitute;

namespace Qread.Generated.Tests.Unit;

public sealed partial class DoubleTests
{
    [Test]
    public async Task NotNullableDouble_ShouldBeSet()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.GetDouble(0).Returns(37_000.67D);

        // Act.
        var sut = Compensation.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.AllTime).IsEqualTo(37_000.67D);
    }

    [Test]
    public async Task NullableDouble_ShouldBeSetToNull_WhenColumnIsNull()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.IsDBNull(1).Returns(true);

        // Act.
        var sut = Compensation.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.ThisQuarter).IsNull();
    }

    [Test]
    public async Task NullableDouble_ShouldBeSetToValue_WhenColumnIsNotNull()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.IsDBNull(1).Returns(false);
        dataReader.GetDouble(1).Returns(6_532.48D);

        // Act.
        var sut = Compensation.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.ThisQuarter).IsEqualTo(6_532.48D);
    }

    [GenerateDataReader(IsExact = true)]
    private sealed partial record Compensation
    {
        public required double AllTime { get; init; }
        public required double? ThisQuarter { get; init; }
    }
}
