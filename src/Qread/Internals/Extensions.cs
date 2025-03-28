using System.CodeDom.Compiler;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Qread.Models;
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

    public static IEnumerable<Property> GetProperties(this INamedTypeSymbol typeSymbol)
    {
        if (typeSymbol.IsAbstract)
            yield break;

        if (typeSymbol.IsValueType)
            yield break;

        while (true)
        {
            foreach (var prop in typeSymbol.GetMembers())
            {
                if (prop is not IPropertySymbol propSymbol)
                    continue;

                if (
                    propSymbol.DeclaredAccessibility != Accessibility.Public
                    && propSymbol.DeclaredAccessibility != Accessibility.Internal
                )
                    continue;

                if (propSymbol.IsReadOnly)
                    continue;

                if (typeSymbol.IsRecord && propSymbol.Name == "EqualityContract")
                    continue;

                yield return new Property(propSymbol);
            }

            if (typeSymbol.BaseType is null)
                yield break;

            typeSymbol = typeSymbol.BaseType;
        }
    }

    public static void StartBlock(this IndentedTextWriter writer)
    {
        writer.WriteLine("{");
        writer.Indent++;
    }
}
