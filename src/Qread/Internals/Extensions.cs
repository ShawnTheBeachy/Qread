using System.CodeDom.Compiler;
using Microsoft.CodeAnalysis;
using Qread.Sources;

namespace Qread.Internals;

internal static class Extensions
{
    public static void EndBlock(this IndentedTextWriter writer)
    {
        writer.Indent--;
        writer.WriteLine("}");
    }

    public static AttributeData? GetGenerateDataReaderAttribute(
        this GeneratorAttributeSyntaxContext context
    )
    {
        foreach (var attribute in context.Attributes)
        {
            if (
                attribute.AttributeClass?.Name == GenerateDataReaderAttribute.Name
                && attribute.AttributeClass?.ContainingNamespace.ToDisplayString()
                    == Constants.Namespace
            )
                return attribute;
        }

        return null;
    }

    public static TypedConstant? GetNamedArg(this AttributeData attribute, string argName)
    {
        foreach (var arg in attribute.NamedArguments)
        {
            if (arg.Key == argName)
                return arg.Value;
        }

        return null;
    }

    public static void StartBlock(this IndentedTextWriter writer)
    {
        writer.WriteLine("{");
        writer.Indent++;
    }
}
