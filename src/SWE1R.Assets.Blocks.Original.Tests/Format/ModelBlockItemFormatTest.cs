// SPDX-License-Identifier: MIT

using ByteSerialization;
using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Materials;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.Original.Tests.Format.Testers.ModelBlock.Materials;
using SWE1R.Assets.Blocks.Original.Tests.Format.Testers.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.Original.Tests.Format.Testers.ModelBlock.Models;
using SWE1R.Assets.Blocks.Original.Tests.Format.Testers.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.Utils.Graphviz;
using SWE1R.Assets.Blocks.Vectors;
using Xunit.Abstractions;

namespace SWE1R.Assets.Blocks.Original.Tests.Format
{
    public partial class ModelBlockItemFormatTest : BlockItemsFormatTestBase<ModelBlockItem>
    {
        #region Constructor

        public ModelBlockItemFormatTest(
            AnalyticsFixture analyticsFixture, 
            OriginalBlocksProviderFixture originalBlocksProviderFixture, 
            ITestOutputHelper output) :
            base(
                analyticsFixture, 
                originalBlocksProviderFixture, 
                output)
        { }

        #endregion

        #region Methods (: BlockItemsTestBase)

        protected override void CompareItemInternal(int index)
        {
            ModelBlockItem modelBlockItem = DeserializeItem(index, out ByteSerializerContext context);
            
            //ExportGraphviz(modelBlockItem, context);
            
            new ModelFormatTesterFactory().Get(modelBlockItem.Model, context.Graph, AnalyticsFixture).Test();
            RunTesters<Mesh, MeshTester>(context);
            RunTesters<MaterialTexture, MaterialTextureTester>(context);
            RunTesters<MeshGroup3064, MeshGroup3064Tester>(context);
            AssertReferenceCounts(context);
            AssertVerticesCount(modelBlockItem);
        }

        #endregion

        #region Methods (helper)

        private void ExportGraphviz(ModelBlockItem modelBlockItem, ByteSerializerContext context)
        {
            new ModelGraphvizExporterFactory().Get(modelBlockItem, context.Graph, "in").Export();
        }

        private void AssertVerticesCount(ModelBlockItem modelBlockItem)
        {
            var meshes = modelBlockItem.Model.GetAllNodes().OfType<Mesh>().ToList();
            int verticesCount = meshes.Sum(m => m.VerticesCount);
        }

        private void AssertReferenceCounts(ByteSerializerContext context)
        {
            // Mesh instances are referenced only once
            Assert.True(GetReferenceCountsToValues<Mesh>(context.Graph).SingleOrDefault() == 1);

            // Material instances can be re-referenced
            Assert.True(GetReferenceCountsToValues<Material>(context.Graph).Count >= 1); // TODO: only references from Mesh (not from e.g. Animation)

            // Mapping instances can be re-referenced
            Assert.True(GetReferenceCountsToValues<Mapping>(context.Graph).Count >= 1);

            // MeshGroup3064 instances do not contain null in Children
            Assert.True(!context.Graph.GetValues<MeshGroup3064>()
                .Where(mg => mg.Children != null)
                .SelectMany(mg => mg.Children)
                .Contains(null));
        }

        private List<int> GetReferenceCountsToValues<TValue>(ByteSerializerGraph graph)
        {
            var references = graph.References
                .Where(r => r.Type == typeof(TValue)).ToList();
            var referenceCountsPerValue = references
                .GroupBy(r => r.Value).Select(g => g.Count()).Distinct().ToList();
            return referenceCountsPerValue;
        }

        #endregion
    }
}
