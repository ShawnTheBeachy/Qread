using System;
using System.CodeDom.Compiler;
using System.Collections.Immutable;
using System.IO;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Qread.Internals;
using Qread.Models;
using Qread.Sources;

namespace Qread.Generators;

[Generator]
public sealed class DataReadersGenerator : IIncrementalGenerator
{
    private static void EndContainers(TypeInternal type, IndentedTextWriter writer)
    {
        foreach (var _ in type.Containers)
            writer.EndBlock();
    }

    private static void GenerateCode(
        SourceProductionContext context,
        ImmutableArray<DataReaderGenerationTarget> targets
    )
    {
        foreach (var target in targets)
        {
            if (context.CancellationToken.IsCancellationRequested)
                return;

            var baseWriter = new StringWriter();
            var indentWriter = new IndentedTextWriter(baseWriter);

            indentWriter.Write(
                $"""
                // <auto-generated />
                #nullable enable
                {(!target.IsExact ? "using System.Collections.Frozen;" : "")}
                using System.Data;

                namespace {target.Namespace};

                
                """
            );
            StartContainers(target.Type, indentWriter);

            if (!target.IsExact)
            {
                indentWriter.WriteLine(
                    "private static FrozenDictionary<string, int>? _propIndices;"
                );
                indentWriter.WriteLineNoTabs("");
            }

            GenerateFromDataReaderMethod(target, indentWriter);
            indentWriter.WriteLineNoTabs("");
            GenerateListFromDataReaderMethod(target, indentWriter);
            EndContainers(target.Type, indentWriter);

            foreach (var prop in target.Type.Properties)
                GenerateNestedObject(prop, indentWriter);

            var hintName = $"{target.Type.FullName}.g.cs";
            context.AddSource(hintName, SourceText.From(baseWriter.ToString(), Encoding.UTF8));
        }
    }

    private static void GenerateFromDataReaderMethod(
        DataReaderGenerationTarget target,
        IndentedTextWriter writer
    )
    {
        writer.WriteLine($"public static {target.Type.Name} FromDataReader(IDataReader reader)");
        writer.StartBlock();
        GeneratePropertyIndices(target, writer);
        writer.WriteLine($"var instance = new {target.Type.Name}");
        writer.StartBlock();

        for (var i = 0; i < target.Type.Properties.Length; i++)
        {
            var prop = target.Type.Properties[i];
            var index = target.IsExact ? i.ToString() : $"_propIndices[\"{prop.Name}\"]";
            var orNull = prop.IsNullable ? $"reader.IsDBNull({index}) ? null : " : "";
            var setter = prop.Type.IsEnum
                ? $"{orNull}(global::{prop.Type.FullName})reader.GetInt32({index})"
                : prop.Type.Name switch
                {
                    nameof(Boolean) => $"{orNull}reader.GetBoolean({index})",
                    nameof(Byte) => $"{orNull}reader.GetByte({index})",
                    nameof(Char) => $"{orNull}reader.GetChar({index})",
                    "DateOnly" => $"{orNull}DateOnly.FromDateTime(reader.GetDateTime({index}))",
                    nameof(DateTime) => $"{orNull}reader.GetDateTime({index})",
                    nameof(DateTimeOffset) => $"{orNull}(DateTimeOffset)reader.GetValue({index})",
                    nameof(Decimal) => $"{orNull}reader.GetDecimal({index})",
                    nameof(Double) => $"{orNull}reader.GetDouble({index})",
                    nameof(Single) => $"{orNull}reader.GetFloat({index})",
                    nameof(Guid) => $"{orNull}reader.GetGuid({index})",
                    nameof(Int16) => $"{orNull}reader.GetInt16({index})",
                    nameof(Int32) => $"{orNull}reader.GetInt32({index})",
                    nameof(Int64) => $"{orNull}reader.GetInt64({index})",
                    nameof(String) => $"{orNull}reader.GetString({index})",
                    _ => $"throw new Exception(\"Unknown type {prop.Type.FullName}.\")",
                };
            writer.WriteLine(
                $"{prop.Name} = {setter}{(i < target.Type.Properties.Length - 1 ? "," : "")}"
            );
        }

        writer.Indent--;
        writer.WriteLine("};");
        writer.WriteLine("return instance;");
        writer.EndBlock();
    }

    private static void GenerateListFromDataReaderMethod(
        DataReaderGenerationTarget target,
        IndentedTextWriter writer
    )
    {
        writer.WriteLine(
            $"public static IReadOnlyList<{target.Type.Name}> ListFromDataReader(IDataReader reader)"
        );
        writer.StartBlock();
        writer.WriteLine($"var results = new List<{target.Type.Name}>();");
        writer.WriteLineNoTabs("");
        writer.WriteLine("while (reader.Read())");
        writer.StartBlock();
        writer.WriteLine("var instance = FromDataReader(reader);");
        writer.WriteLine("results.Add(instance);");
        writer.EndBlock();
        writer.WriteLineNoTabs("");
        writer.WriteLine("return results;");
        writer.EndBlock();
    }

    private static void GenerateNestedObject(Property property, IndentedTextWriter writer)
    {
        if (property.Type.Properties.Length < 1)
            return;

        writer.WriteLineNoTabs("");
        StartContainers(property.Type, writer);

        EndContainers(property.Type, writer);
    }

    private static void GeneratePropertyIndices(
        DataReaderGenerationTarget target,
        IndentedTextWriter writer
    )
    {
        if (target.IsExact)
            return;

        writer.WriteLine("if (_propIndices is null)");
        writer.StartBlock();
        writer.WriteLine("var unfrozenPropIndices = new Dictionary<string, int>();");
        writer.WriteLineNoTabs("");
        writer.WriteLine("for (var i = reader.FieldCount - 1; i >= 0; i--)");
        writer.StartBlock();
        writer.WriteLine("var columnName = reader.GetName(i);");
        writer.WriteLine("unfrozenPropIndices[columnName] = i;");
        writer.EndBlock();
        writer.WriteLineNoTabs("");
        writer.WriteLine("_propIndices = unfrozenPropIndices.ToFrozenDictionary();");
        writer.EndBlock();
        writer.WriteLineNoTabs("");
    }

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx => ctx.AddGenerateDataReaderAttributeSource());
        var provider = context.SyntaxProvider.ForAttributeWithMetadataName(
            $"{Constants.Namespace}.{GenerateDataReaderAttribute.Name}",
            (node, _) =>
                node
                    is ClassDeclarationSyntax
                        or RecordDeclarationSyntax
                        or StructDeclarationSyntax,
            static (ctx, _) =>
                DataReaderGenerationTarget.TryCreate(ctx, out var target) ? target : null
        );
        context.RegisterSourceOutput(
            context.CompilationProvider.Combine(provider.Collect()),
            (ctx, targets) =>
                GenerateCode(
                    ctx,
                    targets.Right.OfType<DataReaderGenerationTarget>().ToImmutableArray()
                )
        );
    }

    private static void StartContainers(TypeInternal type, IndentedTextWriter writer)
    {
        foreach (var parent in type.Containers)
        {
            writer.WriteLine($"partial {parent.TypeKind.ToDeclaration()} {parent.Name}");
            writer.StartBlock();
        }
    }
}
