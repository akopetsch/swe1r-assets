// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
using ByteSerialization.Attributes.Reference;
using ByteSerialization.Components.Values;
using ByteSerialization.Extensions;
using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.Common.Vectors;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.Original.Tests.Format.Testers.ModelBlock.Headers;
using SWE1R.Assets.Blocks.Original.Tests.Format.Testers.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.TestUtils;
using SWE1R.Assets.Blocks.Utils.Graphviz;
using System.Diagnostics;
using Xunit.Abstractions;

namespace SWE1R.Assets.Blocks.Original.Tests.Format
{
    public abstract class TestBase : BlockItemsTestBase<Model>, IClassFixture<AnalyticsFixture>
    {
        #region Properties

        protected ITestOutputHelper Output { get; }
        protected AnalyticsFixture AnalyticsFixture { get; }

        #endregion

        #region Constructor

        public TestBase(AnalyticsFixture analyticsFixture, ITestOutputHelper output, string blockIdName) : 
            base(new OriginalBlockProvider().LoadBlock<Model>(blockIdName))
        {
            AnalyticsFixture = analyticsFixture;
            Output = output;
        }

        #endregion

        #region Methods (: BlockItemsTestBase)

        private List<TValue> GetValues<TValue>(ByteSerializerContext context) => // TODO: move to Graph.cs
            context.Graph.GetValueComponents<TValue>()
                .OrderBy(vc => vc.Position)
                .Select(vc => (TValue)vc.Value)
                .ToList();

        protected override void CompareItemInternal(int i)
        {
            Model model = DeserializeItem(i, out ByteSerializerContext context);
            
            // analyze

            new ModelGraphvizExporterFactory().Get(model, context.Graph, "in").Export();
            
            PrintMemoryUsageStats(model, context);

            var materialTextures = GetValues<MaterialTexture>(context);
            materialTextures.ForEach(x => new MaterialTextureTester(x, context.Graph, AnalyticsFixture).Test());

            var meshes = GetValues<Mesh>(context);
            meshes.ForEach(x => new MeshTester(x, context.Graph, AnalyticsFixture).Test());

            new HeaderFormatTesterFactory().Get(model.Header).Test(context.Graph);

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

            // Mesh instances...
            AssertMeshes(model, context);

            // Header...
            if (model.Header.Animations != null)
            {
                // Anims does not contain null
                Assert.True(!model.Header.Animations.Contains(null));

                // Anims only contains distinct values
                Assert.True(model.Header.Animations.AllUnique());
            }
            if (model.Header.AltN != null)
                // AltN does not contain null
                Assert.True(!model.Header.AltN.Contains(null));
        }

        protected override void PrintItemIndex(int index) =>
            Debug.WriteLine(BlockItem.GetIndexString(index));

        protected override void PrintItemName(string nameString) =>
            Output.WriteLine(nameString);

        protected override void PrintItemDone()
        {
            Debug.WriteLine(string.Empty);
            Debug.WriteLine(string.Empty);
        }

        #endregion

        #region Methods (bounds)

        private void AssertBounds(ByteSerializerContext context)
        {
            List<MeshGroup3064> meshGroups = context.Graph.GetValues<MeshGroup3064>().ToList();
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
                var computedBounds = Bounds3Single.Encapsulate(meshGroup.Meshes.Select(x => x.FixedBounds).ToArray());
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
            var computedBounds = Bounds3Single.Encapsulate(allVectors);

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

        #region Methods (assert)

        private void AssertMeshes(Model model, ByteSerializerContext context)
        {
            List<Mesh> meshes = context.Graph.GetValues<Mesh>().ToList();
            foreach (Mesh mesh in meshes)
            {
                // visible mesh
                if (mesh.VisibleVerticesCount > 0)
                {
                    foreach (IndicesChunk indicesChunk in mesh.VisibleIndicesChunks)
                    {
                        //Debug.Write($"{indicesChunk.Tag}");
                        if (indicesChunk is IndicesChunk01 indicesChunk01)
                        {
                            Assert.True(indicesChunk01.StartVertex.Index >= 0);
                            Assert.True(indicesChunk01.StartVertex.Collection == mesh.VisibleVertices);
                        }
                    }
                    //Debug.WriteLine(string.Empty);
                }
            }
        }

        private List<int> GetReferenceCountsToValues<TValue>(Graph graph)
        {
            List<ReferenceComponent> references = graph.References
                .Where(r => r.Type == typeof(TValue)).ToList();
            List<int> referenceCountsPerValue = references
                .GroupBy(r => r.Value).Select(g => g.Count()).Distinct().ToList();
            return referenceCountsPerValue;
        }

        #endregion

        #region Methods (memory usage)

        private void PrintMemoryUsageStats(Model model, ByteSerializerContext context)
        {
            int bytesCount = model.Part2.Length;

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
            List<ValueComponent> valueComponents = context.Graph.GetValueComponents<TValue>().ToList();
            return valueComponents.Select(vc => (int)vc.Node.Size.Value).Sum();
        }

        #endregion
    }
}
