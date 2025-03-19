using System.Data;
using NSubstitute;

namespace Qread.Generated.Tests.Unit;

public sealed partial class FloatTests
{
    [Test]
    public async Task NotNullableFloat_ShouldBeSet()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.GetFloat(0).Returns(37_000.67f);

        // Act.
        var sut = Compensation.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.AllTime).IsEqualTo(37_000.67f);
    }

    [Test]
    public async Task NullableFloat_ShouldBeSetToNull_WhenColumnIsNull()
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
    public async Task NullableFloat_ShouldBeSetToValue_WhenColumnIsNotNull()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.IsDBNull(1).Returns(false);
        dataReader.GetFloat(1).Returns(6_532.48f);

        // Act.
        var sut = Compensation.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.ThisQuarter).IsEqualTo(6_532.48f);
    }

    [GenerateDataReader(IsExact = true)]
    private sealed partial record Compensation
    {
        public required float AllTime { get; init; }
        public required float? ThisQuarter { get; init; }
    }
}
