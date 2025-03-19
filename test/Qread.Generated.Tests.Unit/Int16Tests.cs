using System.Data;
using NSubstitute;

namespace Qread.Generated.Tests.Unit;

public sealed partial class Int16Tests
{
    [Test]
    public async Task NotNullableInt16_ShouldBeSet()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.GetInt16(0).Returns((short)13);

        // Act.
        var sut = PerksEarned.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.AllTime).IsEqualTo((short)13);
    }

    [Test]
    public async Task NullableInt16_ShouldBeSetToNull_WhenColumnIsNull()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.IsDBNull(1).Returns(true);

        // Act.
        var sut = PerksEarned.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.ThisQuarter).IsNull();
    }

    [Test]
    public async Task NullableInt16_ShouldBeSetToValue_WhenColumnIsNotNull()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.IsDBNull(1).Returns(false);
        dataReader.GetInt16(1).Returns((short)2);

        // Act.
        var sut = PerksEarned.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.ThisQuarter).IsEqualTo((short)2);
    }

    [GenerateDataReader(IsExact = true)]
    private sealed partial record PerksEarned
    {
        public required short AllTime { get; init; }
        public required short? ThisQuarter { get; init; }
    }
}
