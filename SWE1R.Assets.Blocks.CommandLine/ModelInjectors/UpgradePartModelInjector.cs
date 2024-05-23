﻿// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;

namespace SWE1R.Assets.Blocks.CommandLine.ModelInjectors
{
    public class UpgradePartModelInjector(
        MeshGroup3064 meshGroup3064,
        Block<ModelBlockItem> modelBlock) : 
        ModelInjector(meshGroup3064, modelBlock)
    {
        protected override ModelBlockItem GetModelBlockItem(Block<ModelBlockItem> modelBlock) =>
            modelBlock[170]; // 170 = Part_Upgrade_TopSpeed_Plug3ThrustCoil

        protected override FlaggedNode GetParentNode(Model model) =>
            model.GetAllNodes().OfType<TransformableD065>().Single(); // TODO: Children.Clear()?
    }
}