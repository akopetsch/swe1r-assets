// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.Unity.Components.Models.Nodes;
using Swe1rTrakHeader = SWE1R.Assets.Blocks.ModelBlock.Types.TrakHeader;
using Swe1rGroup5064 = SWE1R.Assets.Blocks.ModelBlock.Nodes.Group5064;

namespace SWE1R.Assets.Blocks.Unity.Components.Models.Types
{
    public class TrakHeaderComponent : HeaderComponent<Swe1rTrakHeader>
    {
        public Group5064Component node;

        public override void Import(Swe1rTrakHeader header, ModelImporter modelImporter)
        {
            base.Import(header, modelImporter);
            node = (Group5064Component)modelImporter.CreateFlaggedNodeGameObject(header.Node, gameObject);
        }

        public override Header Export(ModelExporter modelExporter)
        {
            var header = (Swe1rTrakHeader)base.Export(modelExporter);
            header.Node = (Swe1rGroup5064)modelExporter.GetFlaggedNode(node.gameObject);
            return header;
        }
    }
}
