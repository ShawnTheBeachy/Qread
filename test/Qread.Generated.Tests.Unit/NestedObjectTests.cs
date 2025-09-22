using System.Data;
using NSubstitute;

namespace Qread.Generated.Tests.Unit;

public sealed partial class NestedObjectTests
{
    [Test]
    public async Task NestedObject_ShouldBeSet()
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.FieldCount.Returns(1);
        const string name = "Mark Scout";
        dataReader.GetString(0).Returns(name);
        dataReader.GetName(0).Returns("Employee_Name");
        dataReader.Read().Returns(true, false);

        // Act.
        var sut = PerformanceReview.FromDataReader(dataReader);

        // Assert.
        using var asserts = Assert.Multiple();
        await Assert.That(sut.Employee).IsNotNull();
        await Assert.That(sut.Employee.Name).IsEqualTo(name);
    }

    [GenerateDataReader]
    private sealed partial record PerformanceReview
    {
        public required Employee Employee { get; init; }
    }

    private sealed record Employee
    {
        public required string Name { get; init; }
    }
}
