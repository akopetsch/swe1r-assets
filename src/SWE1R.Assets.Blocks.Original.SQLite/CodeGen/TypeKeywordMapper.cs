// SPDX-License-Identifier: GPL-2.0-only

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SWE1R.Assets.Blocks.Original.SQLite.CodeGen
{
    public static class TypeKeywordMapper
    {
        public static string GetKeywordFromType(Type type)
        {
            // Create a small piece of code
            var code = $"var exampleVar = default({type.FullName});";
            var syntaxTree = CSharpSyntaxTree.ParseText(code);

            // Set up a compilation
            var compilation = CSharpCompilation.Create("TypeKeywordMapping")
                .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
                .AddSyntaxTrees(syntaxTree);

            // Get the semantic model
            var semanticModel = compilation.GetSemanticModel(syntaxTree);

            // Find the variable declaration
            var variableDeclaration = syntaxTree.GetRoot().DescendantNodes()
                .OfType<VariableDeclarationSyntax>()
                .FirstOrDefault();

            // Get the type info
            var typeInfo = semanticModel.GetTypeInfo(variableDeclaration.Type);

            // Get the special type and map it to the keyword
            return typeInfo.Type.ToDisplayString();
        }
    }
}
