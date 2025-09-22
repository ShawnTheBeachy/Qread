using System.Data;
using NSubstitute;

namespace Qread.Generated.Tests.Unit;

public sealed partial class NestedObjectTests
{
    [Test]
    public async Task NestedObjects_ShouldBeSet()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.FieldCount.Returns(2);
        const string employeeName = "Mark Scout";
        dataReader.GetString(0).Returns(employeeName);
        dataReader.GetName(0).Returns("Employee_Name");
        const string spouseName = "Gemma Scout";
        dataReader.GetString(1).Returns(spouseName);
        dataReader.GetName(1).Returns("Employee_Spouse_Name");
        dataReader.Read().Returns(true, false);

        // Act.
        var sut = PerformanceReview.FromDataReader(dataReader);

        // Assert.
        using var asserts = Assert.Multiple();
        await Assert.That(sut.Employee).IsNotNull();
        await Assert.That(sut.Employee.Name).IsEqualTo(employeeName);
        await Assert.That(sut.Employee.Spouse).IsNotNull();
        await Assert.That(sut.Employee.Spouse.Name).IsEqualTo(spouseName);
    }

    [GenerateDataReader]
    private sealed partial record PerformanceReview
    {
        public required Employee Employee { get; init; }
    }

    [GenerateDataReader]
    private sealed partial record Employee
    {
        public required string Name { get; init; }
        public required Spouse Spouse { get; init; }
    }

    [GenerateDataReader]
    private sealed partial record Spouse
    {
        public required string Name { get; init; }
    }
}
