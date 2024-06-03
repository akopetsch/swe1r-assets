// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using SWE1R.Assets.Blocks.Vectors;

namespace SWE1R.Assets.Blocks.ModelBlock.Nodes
{
    public class TransformedWithPivotNode : FlaggedNode
    {
        [Order(0)] public Matrix3x4Single Matrix { get; set; }
        [Order(1)] public Vector3Single Vector { get; set; }

        public TransformedWithPivotNode() : base() =>
            Flags = NodeFlags.TransformedWithPivotNode;
    }
}
