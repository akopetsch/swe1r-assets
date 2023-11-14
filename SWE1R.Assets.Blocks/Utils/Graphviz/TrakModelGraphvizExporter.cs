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
        public TrakModelGraphvizExporter(Model model, Graph byteSerializationGraph, string suffix) : 
            base(model, byteSerializationGraph, suffix)
        { }

        protected override void WriteDigraph()
        {
            base.WriteDigraph();

            var trakHeader = (TrakHeader)Model.Header;
            DotFile.AppendLine($"{tab}{headerNodeName} -> {nodeNodeNamesByNode[trakHeader.Node]};");
        }
    }
}
