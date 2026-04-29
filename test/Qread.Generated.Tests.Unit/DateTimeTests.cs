using System.Data;

namespace Qread.Generated.Tests.Unit;

public sealed partial class DateTimeTests
{
    [Test]
    public async Task NotNullableDateTime_ShouldBeSet()
    {
        // Arrange.
        var dataReader = IDataReader.Imposter();
        dataReader.GetDateTime(1).Returns(new DateTime(2022, 2, 18));

        // Act.
        var sut = ProjectedCompletions.FromDataReader(dataReader.Instance());

        // Assert.
        await Assert.That(sut.Tumwater).IsEqualTo(new DateTime(2022, 2, 18));
    }

    [Test]
    public async Task NullableDateTime_ShouldBeSetToNull_WhenColumnIsNull()
    {
        // Arrange.
        var dataReader = IDataReader.Imposter();
        dataReader.IsDBNull(0).Returns(true);

        // Act.
        var sut = ProjectedCompletions.FromDataReader(dataReader.Instance());

        // Assert.
        await Assert.That(sut.ColdHarbor).IsNull();
    }

    [Test]
    public async Task NullableDateTime_ShouldBeSetToValue_WhenColumnIsNotNull()
    {
        // Arrange.
        var dataReader = IDataReader.Imposter();
        dataReader.IsDBNull(0).Returns(false);
        dataReader.GetDateTime(0).Returns(new DateTime(2025, 3, 21));

        // Act.
        var sut = ProjectedCompletions.FromDataReader(dataReader.Instance());

        // Assert.
        await Assert.That(sut.ColdHarbor).IsEqualTo(new DateTime(2025, 3, 21));
    }

    [GenerateDataReader(IsExact = true)]
    private sealed partial record ProjectedCompletions
    {
        public required DateTime? ColdHarbor { get; init; }
        public required DateTime Tumwater { get; init; }
    }
}
