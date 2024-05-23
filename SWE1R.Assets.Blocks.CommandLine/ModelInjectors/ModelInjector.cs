// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;

namespace SWE1R.Assets.Blocks.CommandLine.ModelInjectors
{
    public abstract class ModelInjector(
        MeshGroup3064 meshGroup3064,
        Block<ModelBlockItem> modelBlock)
    {
        public MeshGroup3064 MeshGroup3064 { get; } = meshGroup3064;
        public Block<ModelBlockItem> ModelBlock { get; } = modelBlock;

        public void Inject()
        {
            // load block item
            ModelBlockItem modelBlockItem = GetModelBlockItem(ModelBlock);
            modelBlockItem.Load();

            // inject model
            FlaggedNode parentNode = GetParentNode(modelBlockItem.Model);
            parentNode.Children.Clear();
            parentNode.Children.Add(MeshGroup3064);
            parentNode.UpdateChildrenCount();

            // save block item
            modelBlockItem.Save();
        }

        protected abstract ModelBlockItem GetModelBlockItem(Block<ModelBlockItem> modelBlock);

        protected abstract FlaggedNode GetParentNode(Model model);
    }
}
