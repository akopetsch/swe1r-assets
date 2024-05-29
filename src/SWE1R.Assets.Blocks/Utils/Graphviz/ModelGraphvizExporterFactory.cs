// SPDX-License-Identifier: GPL-2.0-only

using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Types;
using ByteSerializationGraph = ByteSerialization.Nodes.ByteSerializerGraph;

namespace SWE1R.Assets.Blocks.Utils.Graphviz
{
    public class ModelGraphvizExporterFactory
    {
        public ModelGraphvizExporter Get(
            ModelBlockItem modelBlockItem, ByteSerializationGraph byteSerializationGraph, string suffix)
        {
            if (modelBlockItem.Model is TrakModel trakHeader)
                return new TrakModelGraphvizExporter(modelBlockItem, byteSerializationGraph, suffix);
            else
                return new ModelGraphvizExporter(modelBlockItem, byteSerializationGraph, suffix);
        }
    }
}
