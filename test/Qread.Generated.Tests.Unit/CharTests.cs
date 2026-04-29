using System.Data;

namespace Qread.Generated.Tests.Unit;

public sealed partial class CharTests
{
    [Test]
    public async Task NotNullableChar_ShouldBeSet()
    {
        // Arrange.
        var dataReader = IDataReader.Imposter();
        dataReader.GetChar(1).Returns('M');

        // Act.
        var sut = Initials.FromDataReader(dataReader.Instance());

        // Assert.
        await Assert.That(sut.Mark).IsEqualTo('M');
    }

    [Test]
    public async Task NullableChar_ShouldBeSetToNull_WhenColumnIsNull()
    {
        // Arrange.
        var dataReader = IDataReader.Imposter();
        dataReader.IsDBNull(0).Returns(true);

        // Act.
        var sut = Initials.FromDataReader(dataReader.Instance());

        // Assert.
        await Assert.That(sut.Dylan).IsNull();
    }

    [Test]
    public async Task NullableChar_ShouldBeSetToValue_WhenColumnIsNotNull()
    {
        // Arrange.
        var dataReader = IDataReader.Imposter();
        dataReader.IsDBNull(0).Returns(false);
        dataReader.GetChar(0).Returns('D');

        // Act.
        var sut = Initials.FromDataReader(dataReader.Instance());

        // Assert.
        await Assert.That(sut.Dylan).IsEqualTo('D');
    }

    [GenerateDataReader(IsExact = true)]
    private sealed partial record Initials
    {
        public required char? Dylan { get; init; }
        public required char Mark { get; init; }
    }
}
