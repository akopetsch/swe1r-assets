// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using ByteSerialization.Nodes;
using SWE1R.Assets.Blocks.ModelBlock.Animations;
using SWE1R.Assets.Blocks.ModelBlock.Nodes;

namespace SWE1R.Assets.Blocks.ModelBlock.Types
{
    public class TrakModel : Model
    {
        #region Properties (serialized)

        [Order(0)]
        public Group5064 Node { get; set; }

        #endregion

        #region Properties (helper)

        public TransformableD065 Skybox => (TransformableD065)Nodes[2].FlaggedNode;

        #endregion

        #region Constructor

        public TrakModel() : base() =>
            Type = ModelType.Trak;

        #endregion

        #region Methods (serialization)

        public override bool HasExtraAlignment(FlaggedNode anim, ByteSerializerGraph graph) => false;
        public override bool HasExtraAlignment(Animation anim, ByteSerializerGraph graph) => false;

        #endregion
    }
}
