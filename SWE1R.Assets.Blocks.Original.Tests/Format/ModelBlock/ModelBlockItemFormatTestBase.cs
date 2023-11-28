﻿// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Materials;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;
using SWE1R.Assets.Blocks.Original.Tests.Format.ModelBlock.Testers.Models;
using SWE1R.Assets.Blocks.Original.Tests.Format.ModelBlock.Testers.Materials;
using SWE1R.Assets.Blocks.Original.Tests.Format.ModelBlock.Testers.Meshes;
using SWE1R.Assets.Blocks.Original.Tests.Format.ModelBlock.Testers.Nodes;
using SWE1R.Assets.Blocks.Utils.Graphviz;
using SWE1R.Assets.Blocks.Vectors;
using Xunit.Abstractions;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.ModelBlock
{
    public abstract class ModelBlockItemFormatTestBase : BlockItemsFormatTestBase<ModelBlockItem>
    {
        #region Constructor

        public ModelBlockItemFormatTestBase(AnalyticsFixture analyticsFixture, ITestOutputHelper output, string blockIdName) :
            base(analyticsFixture, output, blockIdName)
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
        }

        #endregion

        #region Methods (helper)

        private void ExportGraphviz(ModelBlockItem modelBlockItem, ByteSerializerContext context)
        {
            new ModelGraphvizExporterFactory().Get(modelBlockItem, context.Graph, "in").Export();
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
