// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using SWE1R.Assets.Blocks.Vectors;

namespace SWE1R.Assets.Blocks.ModelBlock.Nodes
{
    public abstract class AbstractTransformedNode : FlaggedNode
    {
        #region Properties (serialized)

        [Order(0)]
        public Matrix3x4Single Transform { get; set; }

        #endregion

        #region Constructor

        public AbstractTransformedNode(NodeFlags flags) :
            base(flags)
        { }

        #endregion
    }
}
