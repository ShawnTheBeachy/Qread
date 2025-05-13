using System.Data;
using NSubstitute;

namespace Qread.Generated.Tests.Unit;

public sealed partial class ByteArrayTests
{
    [Test]
    public async Task NotNullableByteArray_ShouldBeSet()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.GetValue(0).Returns((byte[])[4, 8, 15, 16, 23, 42]);

        // Act.
        var sut = Values.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.Set1).IsEquivalentTo((byte[])[4, 8, 15, 16, 23, 42]);
    }

    [Test]
    public async Task NullableByteArray_ShouldBeSetToNull_WhenColumnIsNull()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.IsDBNull(1).Returns(true);

        // Act.
        var sut = Values.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.Set2).IsNull();
    }

    [Test]
    public async Task NullableByteArray_ShouldBeSetToValue_WhenColumnIsNotNull()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.IsDBNull(1).Returns(false);
        dataReader.GetValue(1).Returns((byte[])[4, 8, 15, 16, 23, 42]);

        // Act.
        var sut = Values.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.Set2).IsEquivalentTo((byte[])[4, 8, 15, 16, 23, 42]);
    }

    [GenerateDataReader(IsExact = true)]
    private sealed partial record Values
    {
        public required byte[] Set1 { get; init; }
        public required byte[]? Set2 { get; init; }
    }
}
