// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;

namespace SWE1R.Assets.Blocks.CommandLine.ModelInjectors
{
    public class TatooineTrainingModelInjector(
        MeshGroup3064 meshGroup3064,
        Block<ModelBlockItem> modelBlock) : 
        ModelInjector(meshGroup3064, modelBlock)
    {
        protected override ModelBlockItem GetModelBlockItem(Block<ModelBlockItem> modelBlock) =>
            modelBlock[115];

        protected override FlaggedNode GetParentNode(Model model) =>
            (Group5064)model.Nodes[0].FlaggedNode;
    }
}
