// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Attributes;
using SWE1R.Assets.Blocks.Vectors;

namespace SWE1R.Assets.Blocks.ModelBlock.Nodes
{
    public class TransformableD064 : FlaggedNode
    {
        [Order(0)] public Matrix3x4Single Matrix { get; set; }

        public TransformableD064() : base() =>
            Flags = NodeFlags.TransformableD064;
    }
}
