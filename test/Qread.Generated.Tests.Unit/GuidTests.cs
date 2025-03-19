using System.Data;
using NSubstitute;

namespace Qread.Generated.Tests.Unit;

public sealed partial class GuidTests
{
    [Test]
    public async Task NotNullableGuid_ShouldBeSet()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.GetGuid(0).Returns(Guid.Parse("254e94ca-5cd0-4eeb-8e58-1eafd84cbf35"));

        // Act.
        var sut = TeamLeaderIds.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.Mark).IsEqualTo(Guid.Parse("254e94ca-5cd0-4eeb-8e58-1eafd84cbf35"));
    }

    [Test]
    public async Task NullableGuid_ShouldBeSetToNull_WhenColumnIsNull()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.IsDBNull(1).Returns(true);

        // Act.
        var sut = TeamLeaderIds.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.Petey).IsNull();
    }

    [Test]
    public async Task NullableGuid_ShouldBeSetToValue_WhenColumnIsNotNull()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.IsDBNull(1).Returns(false);
        dataReader.GetGuid(1).Returns(Guid.Parse("1293454e-2c4d-4b17-bb8c-9df6762de0c1"));

        // Act.
        var sut = TeamLeaderIds.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.Petey).IsEqualTo(Guid.Parse("1293454e-2c4d-4b17-bb8c-9df6762de0c1"));
    }

    [GenerateDataReader(IsExact = true)]
    private sealed partial record TeamLeaderIds
    {
        public required Guid Mark { get; init; }
        public required Guid? Petey { get; init; }
    }
}
