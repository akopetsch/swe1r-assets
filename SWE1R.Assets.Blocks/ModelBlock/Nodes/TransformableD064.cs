// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

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
