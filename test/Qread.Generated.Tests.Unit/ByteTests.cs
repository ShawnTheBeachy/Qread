using System.Data;
using NSubstitute;

namespace Qread.Generated.Tests.Unit;

public sealed partial class ByteTests
{
    [Test]
    public async Task NotNullableByte_ShouldBeSet()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.GetByte(0).Returns((byte)37);

        // Act.
        var sut = Numbers.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.Set1).IsEqualTo((byte)37);
    }

    [Test]
    public async Task NullableByte_ShouldBeSetToNull_WhenColumnIsNull()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.IsDBNull(1).Returns(true);

        // Act.
        var sut = Numbers.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.Set2).IsNull();
    }

    [Test]
    public async Task NullableByte_ShouldBeSetToValue_WhenColumnIsNotNull()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.IsDBNull(1).Returns(false);
        dataReader.GetByte(1).Returns((byte)26);

        // Act.
        var sut = Numbers.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.Set2).IsEqualTo((byte)26);
    }

    [GenerateDataReader(IsExact = true)]
    private sealed partial record Numbers
    {
        public required byte Set1 { get; init; }
        public required byte? Set2 { get; init; }
    }
}
