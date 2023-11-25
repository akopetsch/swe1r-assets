// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Unity.Components.Models.Nodes;
using Swe1rGroup5064 = SWE1R.Assets.Blocks.ModelBlock.Nodes.Group5064;
using Swe1rModel = SWE1R.Assets.Blocks.ModelBlock.Model;
using Swe1rTrakModel = SWE1R.Assets.Blocks.ModelBlock.Types.TrakModel;

namespace SWE1R.Assets.Blocks.Unity.Components.Models.Types
{
    public class TrakModelComponent : ModelComponent<Swe1rTrakModel>
    {
        public Group5064Component node;

        public override void Import(Swe1rTrakModel header, ModelImporter modelImporter)
        {
            base.Import(header, modelImporter);
            node = (Group5064Component)modelImporter.CreateFlaggedNodeGameObject(header.Node, gameObject);
        }

        public override Swe1rModel Export(ModelExporter modelExporter)
        {
            var model = (Swe1rTrakModel)base.Export(modelExporter);
            model.Node = (Swe1rGroup5064)modelExporter.GetFlaggedNode(node.gameObject);
            return model;
        }
    }
}
