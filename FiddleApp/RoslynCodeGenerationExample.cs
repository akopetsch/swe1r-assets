// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace FiddleApp
{
    public class RoslynCodeGenerationExample
    {
        public RoslynCodeGenerationExample()
        {
            var className = "MyGeneratedClass";
            var propertyName = "MyProperty";
            var propertyType = "string";

            var classDeclaration = GenerateClass(className, propertyName, propertyType);
            var code = classDeclaration.NormalizeWhitespace().ToFullString();

            Console.WriteLine(code);
        }

        private ClassDeclarationSyntax GenerateClass(string className, string propertyName, string propertyType)
        {
            return ClassDeclaration(className)
                .AddModifiers(Token(SyntaxKind.PublicKeyword))
                .AddMembers(
                    PropertyDeclaration(
                        IdentifierName(propertyType),
                        Identifier(propertyName))
                    .AddModifiers(Token(SyntaxKind.PublicKeyword))
                    .AddAccessorListAccessors(
                        AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
                        AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithSemicolonToken(Token(SyntaxKind.SemicolonToken)))
                );
        }
    }
}
