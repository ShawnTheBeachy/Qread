using System.Data;
using NSubstitute;

namespace Qread.Generated.Tests.Unit;

public sealed partial class DeepNestedObjectInexactTests
{
    private static IDataReader GetDataReader()
    {
        var dataReader = Substitute.For<IDataReader>();
        dataReader.FieldCount.Returns(5);
        dataReader
            .GetName(0)
            .Returns(
                $"{nameof(Employee.Perks)}_{nameof(Perks.EarliestPerk)}_{nameof(Perk.Description)}"
            );
        dataReader
            .GetName(1)
            .Returns($"{nameof(Employee.Perks)}_{nameof(Perks.EarliestPerk)}_{nameof(Perk.Name)}");
        dataReader.GetName(2).Returns($"{nameof(Employee.Perks)}_{nameof(Perks.LastChecked)}");
        dataReader
            .GetName(3)
            .Returns(
                $"{nameof(Employee.Perks)}_{nameof(Perks.LatestPerk)}_{nameof(Perk.Description)}"
            );
        dataReader
            .GetName(4)
            .Returns($"{nameof(Employee.Perks)}_{nameof(Perks.LatestPerk)}_{nameof(Perk.Name)}");
        return dataReader;
    }

    [Test]
    public async Task NullableNestedObject_ShouldBeSetToNull_WhenAllColumnsAreNull()
    {
        // Arrange.
        var dataReader = GetDataReader();
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
        var dataReader = GetDataReader();
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
        var dataReader = GetDataReader();
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

    [GenerateDataReader]
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
