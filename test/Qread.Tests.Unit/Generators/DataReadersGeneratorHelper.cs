using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Qread.Generators;
using VerifyTUnit;

namespace Qread.Tests.Unit.Generators;

internal static class DataReadersGeneratorHelper
{
    public static Task Verify(string source)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(source);
        var compilation = CSharpCompilation.Create(
            assemblyName: "Tests",
            syntaxTrees: [syntaxTree],
            references: [MetadataReference.CreateFromFile(typeof(object).Assembly.Location)]
        );

        var generator = new DataReadersGenerator();
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);
        driver = driver.RunGenerators(compilation);

        var verifySettings = new VerifySettings();
        verifySettings.UseDirectory("Verify/DataReadersGenerator");
        return Verifier
            .Verify(driver, verifySettings)
            .IgnoreGeneratedResult(result =>
                result.HintName is "GenerateDataReaderAttribute.g.cs" or "IgnoreAttribute.g.cs"
            );
    }
}
