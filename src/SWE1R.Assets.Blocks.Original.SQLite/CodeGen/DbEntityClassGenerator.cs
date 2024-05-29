// SPDX-License-Identifier: MIT

using ByteSerialization.Components.Values.Composites.Records;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SWE1R.Assets.Blocks.Original.SQLite.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using ArgumentSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.ArgumentSyntax;
using AttributeListSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.AttributeListSyntax;
using AttributeSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.AttributeSyntax;
using CompilationUnitSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax;
using InvocationExpressionSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.InvocationExpressionSyntax;
using TypeSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax;

namespace SWE1R.Assets.Blocks.Original.SQLite.CodeGen
{
    public class DbEntityClassGenerator
    {
        #region Fields

        private Dictionary<Type, Type> _typeMapping = 
            new Dictionary<Type, Type>() {
                { typeof(byte), typeof(byte) },
        };

        private const string _baseKeyword = "base";
        private const string _namespaceName = "SWE1R.Assets.Blocks.Original.SQLite";

        private CompilationUnitSyntax _compilationUnit;

        private Dictionary<string, UsingDirectiveSyntax> _usingDirectives =
            new Dictionary<string, UsingDirectiveSyntax>();

        private List<PropertyHelper> _propertyHelpers;

        #endregion

        #region Properties

        public Type Type { get; }

        public string Code { get; private set; }

        #endregion

        #region Constructor

        public DbEntityClassGenerator(Type type) =>
            Type = type;

        #endregion

        #region Methods (public)

        public void Generate()
        {
            AddUsingDirective(Type.Namespace);

            _propertyHelpers = GetPropertyHelpers();

            CompilationUnitSyntax compilationUnit = CompilationUnit()
                .AddMembers(GetNamespaceDeclaration())
                .AddUsings(GetUsingDirectives())
                .NormalizeWhitespace();

            Code = compilationUnit.ToFullString();
        }

        #endregion

        #region Methods (usings)

        private UsingDirectiveSyntax[] GetUsingDirectives() =>
            _usingDirectives.OrderBy(kvp => kvp.Key).Select(kvp => kvp.Value).ToArray();

        private UsingDirectiveSyntax AddUsingDirective(string name)
        {
            if (_usingDirectives.TryGetValue(name, out UsingDirectiveSyntax existingUsingDirective))
                return existingUsingDirective;
            else
                return _usingDirectives[name] = UsingDirective(ParseName(name));
        }

        #endregion

        #region Methods (namespace)

        private NamespaceDeclarationSyntax GetNamespaceDeclaration() =>
            NamespaceDeclaration(IdentifierName(_namespaceName))
            .AddMembers(GetClassDeclaration());

        #endregion

        #region Methods (class)

        private ClassDeclarationSyntax GetClassDeclaration() =>
            ClassDeclaration($"Db{Type.Name}")
                .AddAttributeLists(GetAttribute<TableAttribute>(argument: Type.Name))
                .AddModifiers(Token(SyntaxKind.PublicKeyword))
                .WithBaseList(BaseList(SingletonSeparatedList<BaseTypeSyntax>(GetBaseType())))
                .AddMembers(GetProperties().ToArray())
                .AddMembers(GetMethods().ToArray());

        private SimpleBaseTypeSyntax GetBaseType() =>
            SimpleBaseType(GenericName(
                Identifier(typeof(DbBlockItemStructure).Name),
                TypeArgumentList(
                    SeparatedList<TypeSyntax>(
                        new SyntaxNodeOrToken[] {
                            IdentifierName(Type.Name)
                        }))));

        #endregion

        #region Methods (class attributes)

        private AttributeListSyntax GetAttribute<TAttribute>(object argument) where TAttribute : System.Attribute
        {
            Type attributeType = typeof(TAttribute);
            string attributeName = attributeType.Name.TrimEnd(nameof(System.Attribute)); // TODO: use Roslyn
            AddUsingDirective(attributeType.Namespace);

            AttributeArgumentSyntax attributeArgument = AttributeArgument(
            LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(argument.ToString())));
            AttributeSyntax attribute = Attribute(ParseName(attributeName))
                .WithArgumentList(AttributeArgumentList(SingletonSeparatedList(attributeArgument)));
            return AttributeList(SingletonSeparatedList(attribute));
        }

        #endregion

        #region Methods (methods)

        private IEnumerable<MethodDeclarationSyntax> GetMethods()
        {
            yield return GetGetHashCodeMethod();
        }

        #endregion

        #region Methods (methods - GetHashCode)

        private MethodDeclarationSyntax GetGetHashCodeMethod() =>
            MethodDeclaration(
                    returnType: PredefinedType(Token(SyntaxKind.IntKeyword)),
                    identifier: nameof(GetHashCode))
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.OverrideKeyword)))
                .WithExpressionBody(
                ArrowExpressionClause(
                    GetHashCodeCombineInvocationExpression()))
            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));

        private InvocationExpressionSyntax GetHashCodeCombineInvocationExpression() =>
            InvocationExpression(
                MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                    IdentifierName(_baseKeyword),
                    IdentifierName(nameof(DbBlockItemStructure.CombineHashCodes))))
                .AddArgumentListArguments(
                GetCombineHashCodesArguments().ToArray());

        private InvocationExpressionSyntax GetBaseGetHashCodeInvocationExpression() =>
            InvocationExpression(
                MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                    IdentifierName(_baseKeyword),
                    IdentifierName(nameof(GetHashCode))));

        private IEnumerable<ArgumentSyntax> GetCombineHashCodesArguments()
        {
            yield return Argument(GetBaseGetHashCodeInvocationExpression());
            foreach (PropertyHelper propertyHelpers in _propertyHelpers)
            {
                yield return Argument(IdentifierName(propertyHelpers.PropertyName));
            }
        }

        #endregion

        #region Methods (properties)

        private List<PropertyHelper> GetPropertyHelpers() =>
            Type.GetOrderedPropertyInfos().Select(x => new PropertyHelper(x)).ToList();

        private IEnumerable<PropertyDeclarationSyntax> GetProperties()
        {
            foreach (PropertyHelper propertyHelper in _propertyHelpers)
            {
                if (!propertyHelper.Type.IsPrimitive && propertyHelper.IsReference)
                    AddUsingDirective(propertyHelper.Type.Namespace);
                PropertyDeclarationSyntax propertyDeclaration =
                    PropertyDeclaration(
                        IdentifierName(propertyHelper.TypeName), 
                        Identifier(propertyHelper.PropertyName))
                    .AddModifiers(Token(SyntaxKind.PublicKeyword))
                    .AddAccessorListAccessors(
                        AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
                        AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)));
                yield return propertyDeclaration;
            }
        }

        #endregion
    }
}
