// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Animations;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;

namespace SWE1R.Assets.Blocks.ModelBlock.Types
{
    public class PuppHeader : Header
    {
        #region Constructor

        public PuppHeader() : base() =>
            Type = ModelType.Pupp;

        #endregion

        #region Methods (serialization)

        public override bool HasExtraAlignment(FlaggedNode anim, Graph graph) => false;
        public override bool HasExtraAlignment(Animation anim, Graph graph) => false;

        #endregion
    }
}
