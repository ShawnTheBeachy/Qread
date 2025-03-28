using System.Data;
using NSubstitute;

namespace Qread.Generated.Tests.Unit;

public sealed partial class NestedObjectInexactTests
{
    private static IDataReader GetDataReader()
    {
        var dataReader = Substitute.For<IDataReader>();
        dataReader.FieldCount.Returns(5);
        dataReader
            .GetName(0)
            .Returns($"{nameof(Employee.EarliestPerk)}_{nameof(Perk.Description)}");
        dataReader.GetName(1).Returns($"{nameof(Employee.EarliestPerk)}_{nameof(Perk.Name)}");
        dataReader.GetName(2).Returns($"{nameof(Employee.LatestPerk)}_{nameof(Perk.Description)}");
        dataReader.GetName(3).Returns($"{nameof(Employee.LatestPerk)}_{nameof(Perk.Name)}");
        dataReader.GetName(4).Returns($"{nameof(Employee.Name)}");
        return dataReader;
    }

    [Test]
    public async Task NotNullableNestedObject_ShouldBeSet()
    {
        // Arrange.
        var dataReader = GetDataReader();
        dataReader.GetString(3).Returns("Music dance experience");
        dataReader.GetString(4).Returns("Helly R.");

        // Act.
        var sut = Employee.FromDataReader(dataReader);

        // Assert.
        using var asserts = Assert.Multiple();
        await Assert.That(sut.Name).IsEqualTo("Helly R.");
        await Assert.That(sut.LatestPerk).IsNotNull();
        await Assert.That(sut.LatestPerk.Name).IsEqualTo("Music dance experience");
    }

    [Test]
    public async Task NullableNestedObject_ShouldBeSetToNull_WhenAllColumnsAreNull()
    {
        // Arrange.
        var dataReader = GetDataReader();
        dataReader.IsDBNull(0).Returns(true);
        dataReader.IsDBNull(1).Returns(true);
        dataReader.GetString(3).Returns("Waffle party");
        dataReader.GetString(4).Returns("Irving B.");

        // Act.
        var sut = Employee.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.EarliestPerk).IsNull();
    }

    [Test]
    public async Task NullableNestedObject_ShouldBeSetToValue_WhenAnyColumnIsNotNull()
    {
        // Arrange.
        var dataReader = GetDataReader();
        dataReader.IsDBNull(0).Returns(true);
        dataReader.GetString(1).Returns("Finger trap");
        dataReader.GetString(3).Returns("Egg bar");
        dataReader.GetString(4).Returns("Dylan G.");

        // Act.
        var sut = Employee.FromDataReader(dataReader);

        // Assert.
        using var asserts = Assert.Multiple();
        await Assert.That(sut.EarliestPerk).IsNotNull();
        await Assert.That(sut.EarliestPerk!.Description).IsNull();
        await Assert.That(sut.EarliestPerk.Name).IsEqualTo("Finger trap");
    }

    [GenerateDataReader]
    private sealed partial record Employee
    {
        public required Perk? EarliestPerk { get; init; }
        public required Perk LatestPerk { get; init; }
        public required string Name { get; init; }
    }

    public sealed partial record Perk
    {
        public required string? Description { get; init; }
        public required string Name { get; init; }
    }
}
