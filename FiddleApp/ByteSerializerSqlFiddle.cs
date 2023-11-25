// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using Attribute = ByteSerialization.Attributes.Attribute;
using ByteSerialization.Components.Values.Composites.Records;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Reflection;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace FiddleApp
{
    public class ByteSerializerSqlFiddle
    {
        private Dictionary<Type, Type> _typeMapping = 
            new Dictionary<Type, Type>() {
                { typeof(byte), typeof(byte) },
        };

        public ByteSerializerSqlFiddle(Type type)
        {

            string namespaceName = "SWE1R.Assets.Blocks.SQLite";
            string className = $"Db{type.Name}";

            ClassDeclarationSyntax classDeclaration = ClassDeclaration(className)
                .AddModifiers(Token(SyntaxKind.PublicKeyword));
            OrderedPropertyInfos propertyInfos = type.GetOrderedPropertyInfos();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                (string typeName, string propertyName) = GetPropertyCSharpInfos(propertyInfo);
                classDeclaration = classDeclaration.AddMembers(
                    PropertyDeclaration(
                        IdentifierName(typeName),
                        Identifier(propertyName))
                    .AddModifiers(Token(SyntaxKind.PublicKeyword))
                    .AddAccessorListAccessors(
                        AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
                        AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithSemicolonToken(Token(SyntaxKind.SemicolonToken)))
                );
            }

            // methods



            var namespaceDeclaration = NamespaceDeclaration(IdentifierName(namespaceName))
                                        .AddMembers(classDeclaration);

            var code = namespaceDeclaration.NormalizeWhitespace().ToFullString();
            Console.WriteLine(code);
        }

        private (string typeName, string propertyName) GetPropertyCSharpInfos(PropertyInfo propertyInfo)
        {
            string typeName = null;
            string propertyName = null;

            Type type = propertyInfo.PropertyType;
            typeName = type.Name;
            propertyName = propertyInfo.Name;

            if (type.IsEnum)
            {

            }

            if (type.IsPrimitive)
                typeName = TypeKeywordMapper.GetKeywordFromType(type);
            else
            {
                List<Attribute> byteSerializerAttributes = propertyInfo.GetAttributes();
                
                // reference?
                var referenceAttribute = byteSerializerAttributes.OfType<ReferenceAttribute>().SingleOrDefault();
                if (referenceAttribute != null)
                {
                    typeName = TypeKeywordMapper.GetKeywordFromType(typeof(int));
                    propertyName = $"P_{propertyName}";
                }
            }

            // see ValueComponentFactory

            return (typeName, propertyName);
        }
    }
}
