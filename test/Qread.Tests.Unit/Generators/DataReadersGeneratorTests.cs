using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Qread.Generators;

namespace Qread.Tests.Unit.Generators;

public sealed class DataReadersGeneratorTests
{
    [Test]
    public Task BooleanReader_ShouldBeGenerated_WhenPropertyIsBoolean()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public required bool IsDisabled { get; init; }
                public required Boolean IsDisabled2 { get; init; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task BooleanReaderNullable_ShouldBeGenerated_WhenPropertyIsNullableBoolean()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public required bool? IsDisabled { get; init; }
                public required Boolean? IsDisabled2 { get; init; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task ByteArrayReader_ShouldBeGenerated_WhenPropertyIsByteArray()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public required byte[] Value { get; init; }
                public required Byte[] Value2 { get; init; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task ByteArrayReaderNullable_ShouldBeGenerated_WhenPropertyIsNullableByteArray()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public required byte[]? Value { get; init; }
                public required Byte[]? Value2 { get; init; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task ByteReader_ShouldBeGenerated_WhenPropertyIsByte()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public required byte Value { get; init; }
                public required Byte Value2 { get; init; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task ByteReaderNullable_ShouldBeGenerated_WhenPropertyIsNullableByte()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public required byte? Value { get; init; }
                public required Byte? Value2 { get; init; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task CharReader_ShouldBeGenerated_WhenPropertyIsChar()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public required char Value { get; init; }
                public required Char Value2 { get; init; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task CharReaderNullable_ShouldBeGenerated_WhenPropertyIsNullableChar()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public required char? Value { get; init; }
                public required Char? Value2 { get; init; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task DateTimeReader_ShouldBeGenerated_WhenPropertyIsDateOnly()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public required DateOnly DateOfBirth { get; init; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task DateTimeReader_ShouldBeGenerated_WhenPropertyIsDateTime()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public required DateTime DateOfBirth { get; init; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task DateTimeReaderNullable_ShouldBeGenerated_WhenPropertyIsNullableDateOnly()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public required DateOnly? DateOfBirth { get; init; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task DateTimeReaderNullable_ShouldBeGenerated_WhenPropertyIsNullableDateTime()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public required DateTime? DateOfBirth { get; init; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task DecimalReader_ShouldBeGenerated_WhenPropertyIsDecimal()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public required decimal Value { get; init; }
                public required Decimal Value2 { get; init; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task DecimalReaderNullable_ShouldBeGenerated_WhenPropertyIsNullableDecimal()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public required decimal? Value { get; init; }
                public required Decimal? Value2 { get; init; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task DoubleReader_ShouldBeGenerated_WhenPropertyIsDouble()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public required double Value { get; init; }
                public required Double Value2 { get; init; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task DoubleReaderNullable_ShouldBeGenerated_WhenPropertyIsNullableDouble()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public required double? Value { get; init; }
                public required Double? Value2 { get; init; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task FloatReader_ShouldBeGenerated_WhenPropertyIsFloat()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public required float Value { get; init; }
                public required Single Value2 { get; init; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task FloatReaderNullable_ShouldBeGenerated_WhenPropertyIsNullableFloat()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public required float? Value { get; init; }
                public required Single? Value2 { get; init; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task GetValue_ShouldBeCalled_WhenPropertyIsDateTimeOffset()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public required DateTimeOffset DateOfBirth { get; init; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task GetValueNullable_ShouldBeCalled_WhenPropertyIsNullableDateTimeOffset()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public required DateTimeOffset? DateOfBirth { get; init; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task GuidReader_ShouldBeGenerated_WhenPropertyIsGuid()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public required Guid Id { get; init; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task GuidReaderNullable_ShouldBeGenerated_WhenPropertyIsNullableGuid()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public required Guid? Id { get; init; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task Int16Reader_ShouldBeGenerated_WhenPropertyIsShort()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public required short Count { get; init; }
                public required Int16 Count2 { get; init; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task Int16ReaderNullable_ShouldBeGenerated_WhenPropertyIsNullableShort()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public required short? Count { get; init; }
                public required Int16? Count2 { get; init; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task Int32Reader_ShouldBeGenerated_WhenPropertyIsInt()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public required int Count { get; init; }
                public required Int32 Count2 { get; init; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task Int32ReaderNullable_ShouldBeGenerated_WhenPropertyIsNullableInt()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public required int? Count { get; init; }
                public required Int32? Count2 { get; init; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task Int64Reader_ShouldBeGenerated_WhenPropertyIsLong()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public required long Count { get; init; }
                public required Int64 Count2 { get; init; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task Int64ReaderNullable_ShouldBeGenerated_WhenPropertyIsNullableLong()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public required long? Count { get; init; }
                public required Int64? Count2 { get; init; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task Namespace_ShouldBeExcluded_WhenItIsGlobalNamespace()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto;
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task NestedObject_ShouldBeGenerated_WhenItHasParameterlessConstructor(
        CancellationToken cancellationToken
    )
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader]
            public sealed partial record TestDto
            {
                public required Test2Dto Test2 { get; init; }
            }

            [GenerateDataReader]
            public sealed record Test2Dto
            {
                public required TestDto Test { get; init; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task Property_ShouldBeIncluded_WhenItIsPublic()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public required string Name { get; init; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task Property_ShouldBeSkipped_WhenItIsNotPublic()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                private string Name { get; set; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task Property_ShouldBeSkipped_WhenItIsReadOnly()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public int Value => 1;
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task PropertyIndices_ShouldBeGenerated_WhenIsNotExact()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader]
            public sealed partial record TestDto
            {
                public required string FirstName { get; init; }
                public required string? LastName { get; init; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task PropertyIndices_ShouldNotBeGenerated_WhenIsExact()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public required string Name { get; init; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task StringReader_ShouldBeGenerated_WhenPropertyIsString()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public required string Name { get; init; }
                public required String Name2 { get; init; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task StringReaderNullable_ShouldBeGenerated_WhenPropertyIsNullableString()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public required string? Name { get; init; }
                public required String? Name2 { get; init; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task Target_ShouldBeHandled_WhenItIsClass()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial class TestDto;
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task Target_ShouldBeHandled_WhenItIsNestedInInterface()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            public partial interface ITest
            {
               [GenerateDataReader(IsExact = true)]
               public readonly record struct TestDto;
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task Target_ShouldBeHandled_WhenItIsRecord()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto;
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task Target_ShouldBeHandled_WhenItIsRecordStruct()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public readonly record struct TestDto;
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task Target_ShouldBeHandled_WhenItIsStruct()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public readonly struct TestDto;
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public async Task Target_ShouldBeSkipped_WhenItHasNoParameterlessConstructor()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader]
            public sealed partial class TestDto
            {
                public TestDto(string value) { }
            }
            """;
        var generator = new DataReadersGenerator();
        var driver = CSharpGeneratorDriver.Create(generator);
        var compilation = CSharpCompilation.Create(
            typeof(DataReadersGeneratorTests).Assembly.FullName,
            [CSharpSyntaxTree.ParseText(dtoSourceText)],
            [MetadataReference.CreateFromFile(typeof(object).Assembly.Location)]
        );

        // Act.
        var runResult = driver.RunGenerators(compilation).GetRunResult();

        // Assert.
        await Assert
            .That(
                runResult.GeneratedTrees.Where(x =>
                    x.FilePath.EndsWith("TestNamespace.TestDto.g.cs")
                )
            )
            .IsEmpty();
    }

    [Test]
    public Task TimeSpanReader_ShouldBeGenerated_WhenPropertyIsTimeSpan()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public required TimeSpan Duration { get; init; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task TimeSpanReaderNullable_ShouldBeGenerated_WhenPropertyIsNullableTimeSpan()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public required TimeSpan? Duration { get; init; }
            }
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public async Task TwoReaders_ShouldBeGenerated_WhenTwoTargetsWithSameNameExistInDifferentNamespaces()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            public sealed partial class ParentA
            {
                [GenerateDataReader(IsExact = true)]
                public sealed partial record TestDto
                {
                    public required string Value { get; init; }
                }
            }

            public sealed partial class ParentB
            {
                [GenerateDataReader(IsExact = true)]
                public sealed partial record TestDto
                {
                    public required string Value { get; init; }
                }
            }

            """;
        var generator = new DataReadersGenerator();
        var driver = CSharpGeneratorDriver.Create(generator);
        var compilation = CSharpCompilation.Create(
            typeof(DataReadersGeneratorTests).Assembly.FullName,
            [CSharpSyntaxTree.ParseText(dtoSourceText)],
            [MetadataReference.CreateFromFile(typeof(object).Assembly.Location)]
        );

        // Act.
        var runResult = driver.RunGenerators(compilation).GetRunResult();

        // Assert.
        using var asserts = Assert.Multiple();
        await Assert
            .That(
                runResult.GeneratedTrees.Where(x =>
                    x.FilePath.EndsWith("TestNamespace.ParentA.TestDto.g.cs")
                )
            )
            .HasCount()
            .EqualToOne();
        await Assert
            .That(
                runResult.GeneratedTrees.Where(x =>
                    x.FilePath.EndsWith("TestNamespace.ParentB.TestDto.g.cs")
                )
            )
            .HasCount()
            .EqualToOne();
    }

    [Test]
    public Task Value_ShouldBeCast_WhenPropertyIsEnum()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public required Color Color { get; init; }
            }

            public enum Color;
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }

    [Test]
    public Task Value_ShouldBeCast_WhenPropertyIsNullableEnum()
    {
        // Arrange.
        const string dtoSourceText = """
            using Qread;

            namespace TestNamespace;

            [GenerateDataReader(IsExact = true)]
            public sealed partial record TestDto
            {
                public required Color? Color { get; init; }
            }

            public enum Color;
            """;

        // Assert.
        return DataReadersGeneratorHelper.Verify(dtoSourceText);
    }
}
