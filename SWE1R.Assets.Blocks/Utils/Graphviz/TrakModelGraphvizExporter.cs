// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Types;

namespace SWE1R.Assets.Blocks.Utils.Graphviz
{
    public class TrakModelGraphvizExporter : ModelGraphvizExporter
    {
        public TrakModelGraphvizExporter(ModelBlockItem modelBlockItem, ByteSerializerGraph byteSerializationGraph, string suffix) : 
            base(modelBlockItem, byteSerializationGraph, suffix)
        { }

        protected override void WriteDigraph()
        {
            base.WriteDigraph();

            var trakHeader = (TrakModel)ModelBlockItem.Model;
            DotFile.AppendLine($"{tab}{modelNodeName} -> {nodeNodeNamesByNode[trakHeader.Node]};");
        }
    }
}
