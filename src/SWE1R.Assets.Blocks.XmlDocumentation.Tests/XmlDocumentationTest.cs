// SPDX-License-Identifier: MIT

using CSharpXmlDocumentation;
using SWE1R.Assets.Blocks.ModelBlock.Materials;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.XmlDocumentation.Tests.ElementValidation;
using System.Xml.Linq;
using System.Xml.XPath;
using Xunit.Abstractions;

namespace SWE1R.Assets.Blocks.XmlDocumentation.Tests
{
    public partial class XmlDocumentationTest(ITestOutputHelper outputHelper)
    {
        #region Properties

        private ITestOutputHelper OutputHelper { get; } = outputHelper;

        #endregion

        #region Methods (SWE1R.Assets.Blocks.ModelBlock.Materials)

        [Fact]
        public void Test_Materials_Material() => 
            AssertType<Material>();

        [Fact]
        public void Test_Materials_MaterialProperties() => 
            AssertType<MaterialProperties>();

        [Fact]
        public void Test_Materials_MaterialTexture() => 
            AssertType<MaterialTexture>();

        [Fact]
        public void Test_Materials_MaterialTextureChild() => 
            AssertType<MaterialTextureChild>();

        #endregion

        #region Methods (SWE1R.Assets.Blocks.ModelBlock.Nodes)

        [Fact]
        public void Test_Nodes_FlaggedNode() =>
            AssertType<FlaggedNode>();

        [Fact(Skip = $"Disabled because '{nameof(BasicNode)}' is the same as '{nameof(FlaggedNode)}' in linked GitHub repositories.")]
        public void Test_Nodes_BasicNode() =>
            AssertType<BasicNode>();

        [Fact]
        public void Test_Nodes_SelectorNode() =>
            AssertType<SelectorNode>();

        [Fact]
        public void Test_Nodes_LodSelectorNode() =>
            AssertType<LodSelectorNode>();

        [Fact]
        public void Test_Nodes_TransformedNode() =>
            AssertType<TransformedNode>();

        [Fact]
        public void Test_Nodes_TransformedWithPivotNode() =>
            AssertType<TransformedWithPivotNode>();

        [Fact]
        public void Test_Nodes_TransformedComputedNode() =>
            AssertType<TransformedComputedNode>();

        [Fact]
        public void Test_Nodes_NodeFlags() =>
            AssertType<NodeFlags>();

        #endregion

        #region Methods (helper)

        private void AssertType<T>()
        {
            Type type = typeof(T);
            OutputHelper.WriteLine(type.Name);

            XElement summary = CSharpXmlDocumentationHelper.GetSummary(type);

            var seeAlsoElementValidator = new SeeAlsoElementValidator(
                summary.XPathSelectElement(SeeAlsoElementValidator.XPath));
            seeAlsoElementValidator.Validate();

            OutputHelper.WriteLine(string.Empty);
        }

        #endregion
    }
}
