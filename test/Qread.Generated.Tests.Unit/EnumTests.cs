using System.Data;
using NSubstitute;

namespace Qread.Generated.Tests.Unit;

public sealed partial class EnumTests
{
    [Test]
    public async Task NotNullableEnum_ShouldBeSet()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.GetInt32(1).Returns((int)State.Innie);

        // Act.
        var sut = Team.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.Mark).IsEqualTo(State.Innie);
    }

    [Test]
    public async Task NullableEnum_ShouldBeSetToNull_WhenColumnIsNull()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.IsDBNull(0).Returns(true);

        // Act.
        var sut = Team.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.Dylan).IsNull();
    }

    [Test]
    public async Task NullableEnum_ShouldBeSetToValue_WhenColumnIsNotNull()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.IsDBNull(0).Returns(false);
        dataReader.GetInt32(0).Returns((int)State.Outie);

        // Act.
        var sut = Team.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.Dylan).IsEqualTo(State.Outie);
    }

    [GenerateDataReader(IsExact = true)]
    private sealed partial record Team
    {
        public required State? Dylan { get; init; }
        public required State Mark { get; init; }
    }

    private enum State
    {
        Innie,
        Outie,
    }
}
