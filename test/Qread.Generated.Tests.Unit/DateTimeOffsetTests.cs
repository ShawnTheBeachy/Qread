using System.Data;
using NSubstitute;

namespace Qread.Generated.Tests.Unit;

public sealed partial class DateTimeOffsetTests
{
    [Test]
    public async Task NotNullableDateTimeOffset_ShouldBeSet()
    {
        // Arrange.
        var date = new DateTimeOffset(2022, 2, 18, 21, 0, 0, TimeSpan.FromHours(-4));
        var dataReader = Substitute.For<IDataReader>();
        dataReader.IsDBNull(0).Returns(true);
        dataReader.GetValue(1).Returns(date);

        // Act.
        var sut = ProjectedCompletions.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.Tumwater).IsEqualTo(date);
    }

    [Test]
    public async Task NullableDateTimeOffset_ShouldBeSetToNull_WhenColumnIsNull()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.IsDBNull(0).Returns(true);
        dataReader.GetValue(1).Returns(default(DateTimeOffset));

        // Act.
        var sut = ProjectedCompletions.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.ColdHarbor).IsNull();
    }

    [Test]
    public async Task NullableDateTimeOffset_ShouldBeSetToValue_WhenColumnIsNotNull()
    {
        // Arrange.
        var date = new DateTimeOffset(2025, 3, 20, 21, 0, 0, TimeSpan.FromHours(-4));
        var dataReader = Substitute.For<IDataReader>();
        dataReader.IsDBNull(0).Returns(false);
        dataReader.GetValue(0).Returns(date);
        dataReader.GetValue(1).Returns(default(DateTimeOffset));

        // Act.
        var sut = ProjectedCompletions.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.ColdHarbor).IsEqualTo(date);
    }

    [GenerateDataReader(IsExact = true)]
    private sealed partial record ProjectedCompletions
    {
        public required DateTimeOffset? ColdHarbor { get; init; }
        public required DateTimeOffset Tumwater { get; init; }
    }
}
