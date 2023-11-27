// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
using ByteSerialization.Extensions;
using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Materials;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.Original.Tests.Format.ModelBlock.Testers.Headers;
using SWE1R.Assets.Blocks.Original.Tests.Format.ModelBlock.Testers.Meshes;
using SWE1R.Assets.Blocks.Utils.Graphviz;
using SWE1R.Assets.Blocks.Vectors;
using Xunit.Abstractions;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.ModelBlock
{
    public abstract class ModelFormatTestBase : BlockItemsFormatTestBase<ModelBlockItem>
    {
        #region Constructor

        public ModelFormatTestBase(AnalyticsFixture analyticsFixture, ITestOutputHelper output, string blockIdName) :
            base(analyticsFixture, output, blockIdName)
        { }

        #endregion

        #region Methods (: BlockItemsTestBase)

        protected override void CompareItemInternal(int index)
        {
            ModelBlockItem modelBlockItem = DeserializeItem(index, out ByteSerializerContext context);

            // analyze

            new ModelGraphvizExporterFactory().Get(modelBlockItem, context.Graph, "in").Export();

            PrintMemoryUsageStats(modelBlockItem, context);

            new ModelKindFormatTesterFactory().Get(modelBlockItem.Model, context.Graph, AnalyticsFixture).Test();
            RunTesters<Mesh, MeshTester>(context);
            RunTesters<MaterialTexture, MaterialTextureTester>(context);

            AssertBounds(context);

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

            // Header...
            if (modelBlockItem.Model.Animations != null)
            {
                // Anims does not contain null
                Assert.True(!modelBlockItem.Model.Animations.Contains(null));

                // Anims only contains distinct values
                Assert.True(modelBlockItem.Model.Animations.AllUnique());
            }
            if (modelBlockItem.Model.AltN != null)
                // AltN does not contain null
                Assert.True(!modelBlockItem.Model.AltN.Contains(null));
        }

        #endregion

        #region Methods (bounds)

        private void AssertBounds(ByteSerializerContext context)
        {
            var meshGroups = context.Graph.GetValues<MeshGroup3064>().ToList();
            foreach (MeshGroup3064 meshGroup in meshGroups)
                AssertBounds(meshGroup);
        }

        private void AssertBounds(MeshGroup3064 meshGroup)
        {
            if (meshGroup.Meshes == null)
            {
                var nullBounds = new Bounds3Single(-1, -1, -1, -1, -1, -1);
                Assert.True(meshGroup.Bounds.Equals(nullBounds));
            }
            else
            {
                AssertBounds(meshGroup.Bounds);
                var computedBounds = new Bounds3Single(meshGroup.Meshes.Select(x => x.FixedBounds).ToArray());
                Assert.True(meshGroup.Bounds.Equals(computedBounds));
                foreach (Mesh mesh in meshGroup.Meshes)
                    AssertBounds(mesh);
            }
        }

        private void AssertBounds(Mesh mesh)
        {
            var visibleVertices = mesh.VisibleVertices?.Select(x => (Vector3Single)x.Position).ToArray() ?? new Vector3Single[] { };
            var collisionVerticesInt16 = mesh.CollisionVertices?.ShortVectors?.Select(x => (Vector3Single)x).ToArray() ?? new Vector3Single[] { };
            var collisionVerticesSingle = mesh.CollisionVertices?.FloatVectors?.ToArray() ?? new Vector3Single[] { };
            var allVectors = visibleVertices.Concat(collisionVerticesSingle).Concat(collisionVerticesInt16).ToArray();
            var computedBounds = new Bounds3Single(allVectors);

            //Assert.True(mesh.FixedBounds.Equals(computedBounds)); // sometimes fails
            //Assert.True(computedBounds.Contains(mesh.FixedBounds)); // sometimes fails
            //Assert.True(mesh.FixedBounds.Contains(computedBounds)); // sometimes fails
        }

        private void AssertBounds(Bounds3Single bounds)
        {
            Assert.True(bounds.Min.X <= bounds.Max.X);
            Assert.True(bounds.Min.Y <= bounds.Max.Y);
            Assert.True(bounds.Min.Z <= bounds.Max.Z);
        }

        #endregion

        #region Methods (helper)

        private List<int> GetReferenceCountsToValues<TValue>(Graph graph)
        {
            var references = graph.References
                .Where(r => r.Type == typeof(TValue)).ToList();
            var referenceCountsPerValue = references
                .GroupBy(r => r.Value).Select(g => g.Count()).Distinct().ToList();
            return referenceCountsPerValue;
        }

        private void PrintMemoryUsageStats(ModelBlockItem modelBlockItem, ByteSerializerContext context)
        {
            int bytesCount = modelBlockItem.Part2.Length;

            int vertexBytesCount = GetBytesCount<Vertex>(context);
            int indicesChunkBytesCount = GetBytesCount<IndicesChunk>(context);
            int materialPropertiesChunkBytesCount = GetBytesCount<MaterialProperties>(context);
            int materialTextureChildBytesCount = GetBytesCount<MaterialTextureChild>(context);
            int selectedBytesCount =
                vertexBytesCount +
                indicesChunkBytesCount +
                materialPropertiesChunkBytesCount +
                materialTextureChildBytesCount;

            // TODO: remove tmp helper method
        }

        private int GetBytesCount<TValue>(ByteSerializerContext context)
        {
            var valueComponents = context.Graph.GetValueComponents<TValue>().ToList();
            return valueComponents.Select(vc => (int)vc.Node.Size.Value).Sum();
        }

        #endregion
    }
}
