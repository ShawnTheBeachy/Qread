# Qread
Qread is a source-generated data reader implementation rather than using reflection to set properties on your DTOs.

Qread expects that your DTOs are used *only* for reading data from an `IDataReader`. Your DTO may be a `class`, a `record`, or a `struct`.

Using Qread is dead simple:

```c#
using Qread;

...

public sealed partial class MyService
{
  public async Task<IReadOnlyList<Record>> GetRecords()
  {
    // Get an IDataReader...
    var dtos = MyDto.ListFromDataReader(dataReader);

    // Alternatively, use MyDto.FromDataReader(dataReader) to get a single MyDto.

    // Map MyDto to Record and return.
  }

  [GenerateDataReader]
  private sealed partial record MyDto
  {
    public required Guid Id { get; init; }
    public required string Name { get; init; }
  }
}
```

If your DTO properties are in exactly the same order as your SQL select columns you can make Qread even faster by specifying the `IsExact` property:
```c#
[GenerateDataReader(IsExact = true)]
private sealed partial record MyDto
{
  ...
}
```
