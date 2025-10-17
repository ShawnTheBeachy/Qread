using System.Data;
using NSubstitute;

namespace Qread.Generated.Tests.Unit;

public sealed partial class NestedObjectTests
{
    [Test]
    public async Task NestedObject_ShouldBeNull_WhenRequiredNestedObjectIsNull(
        CancellationToken cancellationToken
    )
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.FieldCount.Returns(3);
        const string employeeName = "Mark Scout";
        dataReader.GetString(0).Returns(employeeName);
        dataReader.GetName(0).Returns("Employee_Name");
        const string spouseName = "Gemma Scout";
        dataReader.GetString(1).Returns(spouseName);
        dataReader.GetName(1).Returns("Employee_Spouse_Name");
        dataReader.IsDBNull(2).Returns(true);
        dataReader.GetName(2).Returns("Metadata_Reviewer_Name");
        dataReader.Read().Returns(true, false);

        // Act.
        var sut = PerformanceReview.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.Metadata).IsNull();
    }

    [Test]
    public async Task NestedObject_ShouldBeNull_WhenRequiredPropertyIsNotInDataResult(
        CancellationToken cancellationToken
    )
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.FieldCount.Returns(3);
        const string employeeName = "Mark Scout";
        dataReader.GetString(0).Returns(employeeName);
        dataReader.GetName(0).Returns("Employee_Name");
        const string spouseName = "Gemma Scout";
        dataReader.GetString(1).Returns(spouseName);
        dataReader.GetName(1).Returns("Employee_Spouse_Name");
        dataReader.GetDateTime(2).Returns(new DateTime(2025, 9, 22));
        dataReader.GetName(2).Returns("DateRange_To");
        dataReader.Read().Returns(true, false);

        // Act.
        var sut = PerformanceReview.FromDataReader(dataReader);

        // Assert.
        using var asserts = Assert.Multiple();
        await Assert.That(sut.Period).IsNull();
    }

    [Test]
    public async Task NestedObject_ShouldBeNull_WhenRequiredPropertyIsNullInDataResult(
        CancellationToken cancellationToken
    )
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.FieldCount.Returns(4);
        const string employeeName = "Mark Scout";
        dataReader.GetString(0).Returns(employeeName);
        dataReader.GetName(0).Returns("Employee_Name");
        const string spouseName = "Gemma Scout";
        dataReader.GetString(1).Returns(spouseName);
        dataReader.GetName(1).Returns("Employee_Spouse_Name");
        dataReader.IsDBNull(2).Returns(true);
        dataReader.GetName(2).Returns("DateRange_From");
        dataReader.GetDateTime(3).Returns(new DateTime(2025, 9, 22));
        dataReader.GetName(3).Returns("DateRange_To");
        dataReader.Read().Returns(true, false);

        // Act.
        var sut = PerformanceReview.FromDataReader(dataReader);

        // Assert.
        using var asserts = Assert.Multiple();
        await Assert.That(sut.Period).IsNull();
    }

    [Test]
    public async Task NestedObject_ShouldNotBeNull_WhenNullablePropertyIsNotInDataResult(
        CancellationToken cancellationToken
    )
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.FieldCount.Returns(3);
        const string employeeName = "Mark Scout";
        dataReader.GetString(0).Returns(employeeName);
        dataReader.GetName(0).Returns("Employee_Name");
        const string spouseName = "Gemma Scout";
        dataReader.GetString(1).Returns(spouseName);
        dataReader.GetName(1).Returns("Employee_Spouse_Name");
        const string projectName = "Cold Harbor";
        dataReader.GetString(2).Returns(projectName);
        dataReader.GetName(2).Returns("Employee_CurrentProject_Name");
        dataReader.Read().Returns(true, false);

        // Act.
        var sut = PerformanceReview.FromDataReader(dataReader);

        // Assert.
        using var asserts = Assert.Multiple();
        await Assert.That(sut.Employee).IsNotNull();
        await Assert.That(sut.Employee.Name).IsEqualTo(employeeName);
        await Assert.That(sut.Employee.CurrentProject).IsNotNull();
        await Assert.That(sut.Employee.CurrentProject!.Name).IsEqualTo(projectName);
        await Assert.That(sut.Employee.CurrentProject!.EstimatedCompletion).IsNull();
    }

    [Test]
    public async Task NestedObject_ShouldNotBeNull_WhenNullablePropertyIsNullInDataResult(
        CancellationToken cancellationToken
    )
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.FieldCount.Returns(4);
        const string employeeName = "Mark Scout";
        dataReader.GetString(0).Returns(employeeName);
        dataReader.GetName(0).Returns("Employee_Name");
        const string spouseName = "Gemma Scout";
        dataReader.GetString(1).Returns(spouseName);
        dataReader.GetName(1).Returns("Employee_Spouse_Name");
        const string projectName = "Cold Harbor";
        dataReader.GetString(2).Returns(projectName);
        dataReader.GetName(2).Returns("Employee_CurrentProject_Name");
        dataReader.IsDBNull(3).Returns(true);
        dataReader.GetName(3).Returns("Employee_CurrentProject_EstimatedCompletion");
        dataReader.Read().Returns(true, false);

        // Act.
        var sut = PerformanceReview.FromDataReader(dataReader);

        // Assert.
        using var asserts = Assert.Multiple();
        await Assert.That(sut.Employee).IsNotNull();
        await Assert.That(sut.Employee.Name).IsEqualTo(employeeName);
        await Assert.That(sut.Employee.CurrentProject).IsNotNull();
        await Assert.That(sut.Employee.CurrentProject!.Name).IsEqualTo(projectName);
        await Assert.That(sut.Employee.CurrentProject!.EstimatedCompletion).IsNull();
    }

    [Test]
    public async Task NestedObject_ShouldNotBeNull_WhenRequiredNestedObjectIsNotNull(
        CancellationToken cancellationToken
    )
    {
        // Arrange.
        var dataReader = Substitute.For<IDataReader>();
        dataReader.FieldCount.Returns(3);
        const string employeeName = "Mark Scout";
        dataReader.GetString(0).Returns(employeeName);
        dataReader.GetName(0).Returns("Employee_Name");
        const string spouseName = "Gemma Scout";
        dataReader.GetString(1).Returns(spouseName);
        dataReader.GetName(1).Returns("Employee_Spouse_Name");
        const string reviewerName = "Harmony Cobel";
        dataReader.GetString(2).Returns(reviewerName);
        dataReader.GetName(2).Returns("Metadata_Reviewer_Name");
        dataReader.Read().Returns(true, false);

        // Act.
        var sut = PerformanceReview.FromDataReader(dataReader);

        // Assert.
        using var asserts = Assert.Multiple();
        await Assert.That(sut.Employee).IsNotNull();
        await Assert.That(sut.Employee.Name).IsEqualTo(employeeName);
        await Assert.That(sut.Employee.Spouse).IsNotNull();
        await Assert.That(sut.Employee.Spouse.Name).IsEqualTo(spouseName);
        await Assert.That(sut.Metadata).IsNotNull();
        await Assert.That(sut.Metadata!.Reviewer).IsNotNull();
        await Assert.That(sut.Metadata.Reviewer).IsNotNull();
        await Assert.That(sut.Metadata.Reviewer.Name).IsEqualTo(reviewerName);
    }

    [GenerateDataReader]
    private sealed partial record DateRange
    {
        public required DateOnly From { get; init; }
        public required DateOnly To { get; init; }

        public DateRange(DateOnly from, DateOnly to)
        {
            From = from;
            To = to;
        }
    }

    [GenerateDataReader]
    private sealed partial record Employee
    {
        public Project? CurrentProject { get; init; }
        public required string Name { get; init; }
        public required Spouse Spouse { get; init; }
    }

    [GenerateDataReader]
    private sealed partial record Metadata
    {
        public required Reviewer Reviewer { get; init; }
    }

    [GenerateDataReader]
    private sealed partial record PerformanceReview
    {
        public required Employee Employee { get; init; }
        public Metadata? Metadata { get; init; }
        public DateRange? Period { get; init; }
    }

    [GenerateDataReader]
    private sealed partial record Project
    {
        public DateOnly? EstimatedCompletion { get; init; }
        public required string Name { get; init; }
    }

    [GenerateDataReader]
    private sealed partial record Reviewer
    {
        public required string Name { get; init; }
    }

    [GenerateDataReader]
    private sealed partial record Spouse
    {
        public required string Name { get; init; }
    }
}
