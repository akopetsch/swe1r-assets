// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;

namespace SWE1R.Assets.Blocks.CommandLine.ModelInjectors
{
    public class UpgradePartModelInjector(
        MeshGroupNode meshGroupNode,
        Block<ModelBlockItem> modelBlock) : 
        ModelInjector(meshGroupNode, modelBlock)
    {
        protected override ModelBlockItem GetModelBlockItem(Block<ModelBlockItem> modelBlock) =>
            modelBlock[170]; // 170 = Part_Upgrade_TopSpeed_Plug3ThrustCoil

        protected override FlaggedNode GetParentNode(Model model) =>
            model.GetAllNodes().OfType<TransformedWithPivotNode>().Single(); // TODO: Children.Clear()?
    }
}
