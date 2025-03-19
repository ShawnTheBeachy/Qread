using System.Data;
using NSubstitute;

namespace Qread.Generated.Tests.Unit;

public sealed partial class Int32Tests
{
    [Test]
    public async Task NotNullableInt32_ShouldBeSet()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.GetInt32(0).Returns(13);

        // Act.
        var sut = PerksEarned.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.AllTime).IsEqualTo(13);
    }

    [Test]
    public async Task NullableInt32_ShouldBeSetToNull_WhenColumnIsNull()
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
    public async Task NullableInt32_ShouldBeSetToValue_WhenColumnIsNotNull()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.IsDBNull(1).Returns(false);
        dataReader.GetInt32(1).Returns(2);

        // Act.
        var sut = PerksEarned.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.ThisQuarter).IsEqualTo(2);
    }

    [GenerateDataReader(IsExact = true)]
    private sealed partial record PerksEarned
    {
        public required int AllTime { get; init; }
        public required int? ThisQuarter { get; init; }
    }
}
