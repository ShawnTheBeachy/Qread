using System.Data;
using NSubstitute;

namespace Qread.Generated.Tests.Unit;

public sealed partial class TypeReaderTests
{
    [Test]
    public async Task CustomTypeReader_ShouldBeUsed_WhenRegistered()
    {
        // Arrange.
        const string value = "Value";
        var dataReader = Substitute.For<IDataReader>();
        dataReader.GetString(0).Returns(value);

        TypeReaders.AddReader(new StringWrapperReader());

        // Act.
        var sut = Record.FromDataReader(dataReader);

        // Assert.
        await Assert.That(sut.Wrapper.Value).IsEqualTo(value);
    }

    [GenerateDataReader(IsExact = true)]
    private sealed partial record Record
    {
        public required StringWrapper Wrapper { get; init; }
    }

    private readonly record struct StringWrapper(string Value);

    private sealed class StringWrapperReader : TypeReader<StringWrapper>
    {
        public override StringWrapper Read(IDataReader reader, int index) =>
            new(reader.GetString(index));
    }
}
