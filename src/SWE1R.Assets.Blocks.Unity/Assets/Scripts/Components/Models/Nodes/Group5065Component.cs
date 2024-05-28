// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using Swe1rGroup5065 = SWE1R.Assets.Blocks.ModelBlock.Nodes.Group5065;
using Swe1rFlaggedNode = SWE1R.Assets.Blocks.ModelBlock.Nodes.FlaggedNode;

namespace SWE1R.Assets.Blocks.Unity.Components.Models.Nodes
{
    public class Group5065Component : FlaggedNodeComponent<Swe1rGroup5065>
    {
        public int integer;

        public override void Import(Swe1rGroup5065 source)
        {
            base.Import(source);
            integer = source.Int;
        }

        public override Swe1rFlaggedNode Export(ModelExporter modelExporter)
        {
            var result = (Swe1rGroup5065)base.Export(modelExporter);
            result.Int = integer;
            return result;
        }
    }
}
