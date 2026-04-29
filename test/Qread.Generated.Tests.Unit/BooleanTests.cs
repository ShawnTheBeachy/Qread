using System.Data;

namespace Qread.Generated.Tests.Unit;

public sealed partial class BooleanTests
{
    [Test]
    public async Task NotNullableBoolean_ShouldBeSet()
    {
        // Arrange.
        var dataReader = IDataReader.Imposter();
        dataReader.GetBoolean(1).Returns(true);

        // Act.
        var sut = OrtboParticipation.FromDataReader(dataReader.Instance());

        // Assert.
        await Assert.That(sut.Irving).IsTrue();
    }

    [Test]
    public async Task NullableBoolean_ShouldBeSetToNull_WhenColumnIsNull()
    {
        // Arrange.
        var dataReader = IDataReader.Imposter();
        dataReader.IsDBNull(0).Returns(true);

        // Act.
        var sut = OrtboParticipation.FromDataReader(dataReader.Instance());

        // Assert.
        await Assert.That(sut.Helly).IsNull();
    }

    [Test]
    public async Task NullableBoolean_ShouldBeSetToValue_WhenColumnIsNotNull()
    {
        // Arrange.
        var dataReader = IDataReader.Imposter();
        dataReader.IsDBNull(0).Returns(false);
        dataReader.GetBoolean(0).Returns(false);

        // Act.
        var sut = OrtboParticipation.FromDataReader(dataReader.Instance());

        // Assert.
        await Assert.That(sut.Helly).IsFalse();
    }

    [GenerateDataReader(IsExact = true)]
    private sealed partial record OrtboParticipation
    {
        public required bool? Helly { get; init; }
        public required bool Irving { get; init; }
    }
}
