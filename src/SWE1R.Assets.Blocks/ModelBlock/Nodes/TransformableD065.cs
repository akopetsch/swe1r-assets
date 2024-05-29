// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Attributes;
using SWE1R.Assets.Blocks.Vectors;

namespace SWE1R.Assets.Blocks.ModelBlock.Nodes
{
    public class TransformableD065 : FlaggedNode
    {
        [Order(0)] public Matrix3x4Single Matrix { get; set; }
        [Order(1)] public Vector3Single Vector { get; set; }

        public TransformableD065() : base() =>
            Flags = NodeFlags.TransformableD065;
    }
}
