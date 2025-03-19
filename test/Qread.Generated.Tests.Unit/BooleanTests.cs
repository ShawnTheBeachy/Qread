using System.Data;
using NSubstitute;

namespace Qread.Generated.Tests.Unit;

public sealed partial class BooleanTests
{
    [Test]
    public async Task NotNullableBoolean_ShouldBeSet()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.GetBoolean(1).Returns(true);

        // Act.
        var sut = OrtboParticipation.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.Irving).IsTrue();
    }

    [Test]
    public async Task NullableBoolean_ShouldBeSetToNull_WhenColumnIsNull()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.IsDBNull(0).Returns(true);

        // Act.
        var sut = OrtboParticipation.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.Helly).IsNull();
    }

    [Test]
    public async Task NullableBoolean_ShouldBeSetToValue_WhenColumnIsNotNull()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.IsDBNull(0).Returns(false);
        dataReader.GetBoolean(0).Returns(false);

        // Act.
        var sut = OrtboParticipation.FromDataReader(dataReader);

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
