using System.Data;
using NSubstitute;

namespace Qread.Generated.Tests.Unit;

public sealed partial class ExactTests
{
    [Test]
    public async Task Properties_ShouldBeSetByIndex_WhenMappingIsExact()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.FieldCount.Returns(3);
        dataReader.Read().Returns(true, true, true, true, false);

        // Column names are wrong to ensure it is not mapping by name.
        dataReader.GetName(0).Returns(nameof(TeamMember.Sex));
        dataReader.GetName(1).Returns(nameof(TeamMember.Id));
        dataReader.GetName(2).Returns(nameof(TeamMember.Name));
        dataReader
            .GetGuid(0)
            .Returns(
                Guid.Parse("59c35e39-0c32-4244-8817-d2598f688de2"),
                Guid.Parse("a499056c-e0e7-4081-a476-13454445ba13"),
                Guid.Parse("157d25ea-5e4e-4a28-800a-6c2d3e8b75b0"),
                Guid.Parse("d6073fa0-47c4-45d0-8c7e-9666ba3f16a8")
            );
        dataReader.GetString(1).Returns("Dylan G.", "Helly R.", "Irving B.", "Mark S.");
        dataReader
            .GetInt32(2)
            .Returns((int)Sex.Male, (int)Sex.Female, (int)Sex.Male, (int)Sex.Male);

        // Act.
        var teamMembers = TeamMember.ListFromDataReader(dataReader);

        // Assert.
        using var asserts = Assert.Multiple();
        await Assert.That(teamMembers).HasCount().EqualTo(4);

        await Assert
            .That(teamMembers[0].Id)
            .IsEqualTo(Guid.Parse("59c35e39-0c32-4244-8817-d2598f688de2"));
        await Assert.That(teamMembers[0].Name).IsEqualTo("Dylan G.");
        await Assert.That(teamMembers[0].Sex).IsEqualTo(Sex.Male);

        await Assert
            .That(teamMembers[1].Id)
            .IsEqualTo(Guid.Parse("a499056c-e0e7-4081-a476-13454445ba13"));
        await Assert.That(teamMembers[1].Name).IsEqualTo("Helly R.");
        await Assert.That(teamMembers[1].Sex).IsEqualTo(Sex.Female);

        await Assert
            .That(teamMembers[2].Id)
            .IsEqualTo(Guid.Parse("157d25ea-5e4e-4a28-800a-6c2d3e8b75b0"));
        await Assert.That(teamMembers[2].Name).IsEqualTo("Irving B.");
        await Assert.That(teamMembers[2].Sex).IsEqualTo(Sex.Male);

        await Assert
            .That(teamMembers[3].Id)
            .IsEqualTo(Guid.Parse("d6073fa0-47c4-45d0-8c7e-9666ba3f16a8"));
        await Assert.That(teamMembers[3].Name).IsEqualTo("Mark S.");
        await Assert.That(teamMembers[3].Sex).IsEqualTo(Sex.Male);
    }

    [GenerateDataReader(IsExact = true)]
    private sealed partial record TeamMember
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
        public required Sex Sex { get; init; }
    }

    private enum Sex
    {
        Female,
        Male,
    }
}
