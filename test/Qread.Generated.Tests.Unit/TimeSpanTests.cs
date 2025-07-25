using System.Data;
using NSubstitute;

namespace Qread.Generated.Tests.Unit;

public sealed partial class TimeSpanTests
{
    [Test]
    public async Task NotNullableTimeSpan_ShouldBeSet()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.IsDBNull(0).Returns(true);
        dataReader.GetValue(1).Returns(TimeSpan.FromHours(9));

        // Act.
        var sut = BreakRoomVisit.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.StartTime).IsEqualTo(TimeSpan.FromHours(9));
    }

    [Test]
    public async Task NullableTimeSpan_ShouldBeSetToNull_WhenColumnIsNull()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.IsDBNull(0).Returns(true);
        dataReader.GetValue(1).Returns(TimeSpan.FromHours(9));

        // Act.
        var sut = BreakRoomVisit.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.Duration).IsNull();
    }

    [Test]
    public async Task NullableTimespan_ShouldBeSetToValue_WhenColumnIsNotNull()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.IsDBNull(0).Returns(false);
        dataReader.GetValue(0).Returns(TimeSpan.FromHours(3));
        dataReader.GetValue(1).Returns(TimeSpan.FromHours(9));

        // Act.
        var sut = BreakRoomVisit.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.Duration).IsEqualTo(TimeSpan.FromHours(3));
    }

    [GenerateDataReader(IsExact = true)]
    private sealed partial record BreakRoomVisit
    {
        public required TimeSpan? Duration { get; init; }
        public required TimeSpan StartTime { get; init; }
    }
}
