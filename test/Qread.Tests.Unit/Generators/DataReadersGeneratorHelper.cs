using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Qread.Generators;

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
        GeneratorDriver driver = CSharpGeneratorDriver.Create(
            [generator.AsSourceGenerator()],
            optionsProvider: new CustomConfigOptionsProvider()
        );
        driver = driver.RunGenerators(compilation);

        var verifySettings = new VerifySettings();
        verifySettings.UseDirectory("Verify/DataReadersGenerator");
        return Verifier
            .Verify(driver, verifySettings)
            .IgnoreGeneratedResult(result =>
                result.HintName
                    is "GenerateDataReaderAttribute.g.cs"
                        or "IgnoreAttribute.g.cs"
                        or "ITypeReader.g.cs"
                        or "TypeReader.g.cs"
                        or "TypeReaders.g.cs"
            );
    }

    private sealed class CustomConfigOptionsProvider : AnalyzerConfigOptionsProvider
    {
        public override AnalyzerConfigOptions GlobalOptions { get; } = new CustomConfigOptions();

        public override AnalyzerConfigOptions GetOptions(SyntaxTree tree) => GlobalOptions;

        public override AnalyzerConfigOptions GetOptions(AdditionalText textFile) => GlobalOptions;
    }

    private sealed class CustomConfigOptions : AnalyzerConfigOptions
    {
        public override bool TryGetValue(string key, [NotNullWhen(true)] out string? value)
        {
            value = null;

            if (key != "build_property.RootNamespace")
                return false;

            value = "Test";
            return true;
        }
    }
}
