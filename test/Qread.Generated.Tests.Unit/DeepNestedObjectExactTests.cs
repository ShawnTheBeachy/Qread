using System.Data;
using NSubstitute;

namespace Qread.Generated.Tests.Unit;

public sealed partial class DeepNestedObjectExactTests
{
    [Test]
    public async Task NullableNestedObject_ShouldBeSetToNull_WhenAllColumnsAreNull()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.IsDBNull(Arg.Any<int>()).Returns(true);

        // Act.
        var sut = Employee.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.Perks).IsNull();
    }

    [Test]
    public async Task NullableNestedObject_ShouldBeSetToValue_WhenAnyColumnIsNotNull()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.IsDBNull(Arg.Is<int>(x => x != 2)).Returns(true);
        dataReader.GetDateTime(2).Returns(new DateTime(2025, 3, 20));

        // Act.
        var sut = Employee.FromDataReader(dataReader);

        // Assert.
        using var asserts = Assert.Multiple();
        await Assert.That(sut.Perks).IsNotNull();
        await Assert.That(sut.Perks!.EarliestPerk).IsNull();
        await Assert.That(sut.Perks.LatestPerk).IsNull();
        await Assert.That(sut.Perks.LastChecked).IsEqualTo(new DateTime(2025, 3, 20));
    }

    [Test]
    public async Task NullableNestedObject_ShouldBeSetToValue_WhenAnyNestedObjectIsNotNull()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.IsDBNull(Arg.Is<int>(x => x >= 0 && x <= 3)).Returns(true);
        dataReader.GetString(4).Returns("Egg bar");

        // Act.
        var sut = Employee.FromDataReader(dataReader);

        // Assert.
        using var asserts = Assert.Multiple();
        await Assert.That(sut.Perks).IsNotNull();
        await Assert.That(sut.Perks!.LatestPerk).IsNotNull();
        await Assert.That(sut.Perks.LatestPerk!.Description).IsNull();
        await Assert.That(sut.Perks.LatestPerk.Name).IsEqualTo("Egg bar");
    }

    [GenerateDataReader(IsExact = true)]
    private sealed partial record Employee
    {
        public required Perks? Perks { get; init; }
    }

    public sealed partial record Perks
    {
        public required Perk? EarliestPerk { get; init; }
        public required DateTime? LastChecked { get; init; }
        public required Perk? LatestPerk { get; init; }
    }

    public sealed partial record Perk
    {
        public required string? Description { get; init; }
        public required string Name { get; init; }
    }
}
