// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Animations;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;

namespace SWE1R.Assets.Blocks.ModelBlock.Types
{
    public class ScenHeader : Header
    {
        public ScenHeader() : base() =>
            Type = ModelType.Scen;

        public override bool HasExtraAlignment(FlaggedNode anim, Graph graph) => false;
        public override bool HasExtraAlignment(Animation anim, Graph graph) => false;
    }
}
