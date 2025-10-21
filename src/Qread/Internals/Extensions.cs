using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
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

    public static IEnumerable<Property> GetProperties(
        this ITypeSymbol typeSymbol,
        TypeCache typeCache
    )
    {
        if (typeSymbol.IsAbstract)
            yield break;

        while (true)
        {
            foreach (var prop in typeSymbol.GetMembers())
            {
                if (prop is not IPropertySymbol propSymbol)
                    continue;

                if (propSymbol.IsReadOnly)
                    continue;

                if (typeSymbol.IsRecord && propSymbol.Name == "EqualityContract")
                    continue;

                if (
                    prop.GetAttributes()
                        .Any(x =>
                            x.AttributeClass?.ContainingNamespace.Name == "Qread"
                            && x.AttributeClass?.Name == IgnoreAttribute.Name
                        )
                )
                    continue;

                yield return new Property(propSymbol, typeCache);
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

    public static string ToDeclaration(this TypeKindInternal typeKind) =>
        typeKind switch
        {
            TypeKindInternal.Class => "class",
            TypeKindInternal.Interface => "interface",
            TypeKindInternal.Record => "record",
            TypeKindInternal.RecordStruct => "record struct",
            TypeKindInternal.Struct => "struct",
            _ => "",
        };

    public static TypeKindInternal TypeKind(this ITypeSymbol symbol) =>
        symbol.TypeKind == Microsoft.CodeAnalysis.TypeKind.Struct
            ? symbol.IsRecord
                ? TypeKindInternal.RecordStruct
                : TypeKindInternal.Struct
            : symbol.IsRecord
                ? TypeKindInternal.Record
                : symbol.TypeKind == Microsoft.CodeAnalysis.TypeKind.Interface
                    ? TypeKindInternal.Interface
                    : TypeKindInternal.Class;

    public static void WriteLineIndented(this IndentedTextWriter writer, string value)
    {
        writer.Indent++;
        writer.WriteLine(value);
        writer.Indent--;
    }
}
