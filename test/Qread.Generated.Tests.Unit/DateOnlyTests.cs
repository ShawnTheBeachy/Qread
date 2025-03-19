using System.Data;
using NSubstitute;

namespace Qread.Generated.Tests.Unit;

public sealed partial class DateOnlyTests
{
    [Test]
    public async Task NotNullableDateOnly_ShouldBeSet()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.GetDateTime(1).Returns(new DateTime(2022, 2, 18));

        // Act.
        var sut = ProjectedCompletions.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.Tumwater).IsEqualTo(new DateOnly(2022, 2, 18));
    }

    [Test]
    public async Task NullableDateOnly_ShouldBeSetToNull_WhenColumnIsNull()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.IsDBNull(0).Returns(true);

        // Act.
        var sut = ProjectedCompletions.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.ColdHarbor).IsNull();
    }

    [Test]
    public async Task NullableDateOnly_ShouldBeSetToValue_WhenColumnIsNotNull()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.IsDBNull(0).Returns(false);
        dataReader.GetDateTime(0).Returns(new DateTime(2025, 3, 21));

        // Act.
        var sut = ProjectedCompletions.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.ColdHarbor).IsEqualTo(new DateOnly(2025, 3, 21));
    }

    [GenerateDataReader(IsExact = true)]
    private sealed partial record ProjectedCompletions
    {
        public required DateOnly? ColdHarbor { get; init; }
        public required DateOnly Tumwater { get; init; }
    }
}
