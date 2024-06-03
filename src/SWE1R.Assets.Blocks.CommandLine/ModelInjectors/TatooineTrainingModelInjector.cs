// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;

namespace SWE1R.Assets.Blocks.CommandLine.ModelInjectors
{
    public class TatooineTrainingModelInjector(
        MeshGroupNode meshGroupNode,
        Block<ModelBlockItem> modelBlock) : 
        ModelInjector(meshGroupNode, modelBlock)
    {
        protected override ModelBlockItem GetModelBlockItem(Block<ModelBlockItem> modelBlock) =>
            modelBlock[115];

        protected override FlaggedNode GetParentNode(Model model) =>
            (BasicNode)model.Nodes[0].FlaggedNode;
    }
}
