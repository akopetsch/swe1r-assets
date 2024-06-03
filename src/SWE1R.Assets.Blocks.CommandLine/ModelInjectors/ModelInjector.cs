// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;

namespace SWE1R.Assets.Blocks.CommandLine.ModelInjectors
{
    public abstract class ModelInjector(
        MeshGroupNode meshGroupNode,
        Block<ModelBlockItem> modelBlock)
    {
        public MeshGroupNode MeshGroupNode { get; } = meshGroupNode;
        public Block<ModelBlockItem> ModelBlock { get; } = modelBlock;

        public void Inject()
        {
            // load block item
            ModelBlockItem modelBlockItem = GetModelBlockItem(ModelBlock);
            modelBlockItem.Load();

            // inject model
            FlaggedNode parentNode = GetParentNode(modelBlockItem.Model);
            parentNode.Children.Clear();
            parentNode.Children.Add(MeshGroupNode);
            parentNode.UpdateChildrenCount();

            // save block item
            modelBlockItem.Save();
        }

        protected abstract ModelBlockItem GetModelBlockItem(Block<ModelBlockItem> modelBlock);

        protected abstract FlaggedNode GetParentNode(Model model);
    }
}
