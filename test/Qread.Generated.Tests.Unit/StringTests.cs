using System.Data;
using NSubstitute;

namespace Qread.Generated.Tests.Unit;

public sealed partial class StringTests
{
    [Test]
    public async Task NotNullableString_ShouldBeSet()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.GetString(1).Returns("Helly R.");

        // Act.
        var sut = Innie.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.Name).IsEqualTo("Helly R.");
    }

    [Test]
    public async Task NullableString_ShouldBeSetToNull_WhenColumnIsNull()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.IsDBNull(0).Returns(true);

        // Act.
        var sut = Innie.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.Department).IsNull();
    }

    [Test]
    public async Task NullableString_ShouldBeSetToValue_WhenColumnIsNotNull()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.IsDBNull(0).Returns(false);
        dataReader.GetString(0).Returns("MDR");

        // Act.
        var sut = Innie.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.Department).IsEqualTo("MDR");
    }

    [GenerateDataReader(IsExact = true)]
    private sealed partial record Innie
    {
        public required string? Department { get; init; }
        public required string Name { get; init; }
    }
}
