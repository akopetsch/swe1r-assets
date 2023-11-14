// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Types;
using ByteSerializationGraph = ByteSerialization.Nodes.Graph;

namespace SWE1R.Assets.Blocks.Utils.Graphviz
{
    public class ModelGraphvizExporterFactory
    {
        public ModelGraphvizExporter Get(
            Model model, ByteSerializationGraph byteSerializationGraph, string suffix)
        {
            if (model.Header is TrakHeader trakHeader)
                return new TrakModelGraphvizExporter(model, byteSerializationGraph, suffix);
            else
                return new ModelGraphvizExporter(model, byteSerializationGraph, suffix);
        }
    }
}
