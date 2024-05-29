// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Animations;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;

namespace SWE1R.Assets.Blocks.ModelBlock.Types
{
    public class ScenModel : Model
    {
        #region Constructor

        public ScenModel() : base() =>
            Type = ModelType.Scen;

        #endregion

        #region Methods (serialization)

        public override bool HasExtraAlignment(FlaggedNode anim, ByteSerializerGraph graph) => false;
        public override bool HasExtraAlignment(Animation anim, ByteSerializerGraph graph) => false;

        #endregion
    }
}
