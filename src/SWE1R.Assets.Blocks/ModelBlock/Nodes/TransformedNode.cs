// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using SWE1R.Assets.Blocks.Vectors;

namespace SWE1R.Assets.Blocks.ModelBlock.Nodes
{
    public class TransformedNode : FlaggedNode
    {
        [Order(0)] public Matrix3x4Single Matrix { get; set; }

        public TransformedNode() : base() =>
            Flags = NodeFlags.TransformedNode;
    }
}
