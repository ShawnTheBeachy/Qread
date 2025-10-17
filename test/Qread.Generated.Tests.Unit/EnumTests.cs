using System.Data;
using NSubstitute;

namespace Qread.Generated.Tests.Unit;

public sealed partial class EnumTests
{
    [Test]
    public async Task Enum_ShouldBeSet_WhenSqlTypeIsBigInt(CancellationToken cancellationToken)
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.GetDataTypeName(1).Returns("bigint");
        dataReader.GetInt64(1).Returns((long)State.Innie);

        // Act.
        var sut = Team.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.Mark).IsEqualTo(State.Innie);
    }

    [Test]
    public async Task Enum_ShouldBeSet_WhenSqlTypeIsInt(CancellationToken cancellationToken)
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.GetDataTypeName(1).Returns("int");
        dataReader.GetInt32(1).Returns((int)State.Innie);

        // Act.
        var sut = Team.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.Mark).IsEqualTo(State.Innie);
    }

    [Test]
    public async Task Enum_ShouldBeSet_WhenSqlTypeIsSmallInt(CancellationToken cancellationToken)
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.GetDataTypeName(1).Returns("smallint");
        dataReader.GetInt16(1).Returns((short)State.Innie);

        // Act.
        var sut = Team.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.Mark).IsEqualTo(State.Innie);
    }

    [Test]
    public async Task Enum_ShouldBeSet_WhenSqlTypeIsTinyInt(CancellationToken cancellationToken)
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.GetDataTypeName(1).Returns("tinyint");
        dataReader.GetByte(1).Returns((byte)State.Innie);

        // Act.
        var sut = Team.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.Mark).IsEqualTo(State.Innie);
    }

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
