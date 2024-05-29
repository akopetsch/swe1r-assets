// SPDX-License-Identifier: GPL-2.0-only

using System;
using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.ModelBlock.Nodes
{
    public enum NodeFlags : uint
    {
        MeshGroup3064 =
            0b0_0011_0000_0110_0100, // 0x3064
        Group5064 =
            0b0_0101_0000_0110_0100, // 0x5064
        Group5065 =
            0b0_0101_0000_0110_0101, // 0x5065
        Group5066 =
            0b0_0101_0000_0110_0110, // 0x5066
        TransformableD064 =
            0b0_1101_0000_0110_0100, // 0xD064
        TransformableD065 =
            0b0_1101_0000_0110_0101, // 0xD065
        UnknownD066 =
            0b0_1101_0000_0110_0110, // 0xD066
    }

    public static class NodeFlagsExtensions
    {
        private static Dictionary<NodeFlags, Type> flaggedNodeType = new Dictionary<NodeFlags, Type>()
        {
            { NodeFlags.MeshGroup3064, typeof(MeshGroup3064) },
            { NodeFlags.Group5064, typeof(Group5064) },
            { NodeFlags.Group5065, typeof(Group5065) },
            { NodeFlags.Group5066, typeof(Group5066) },
            { NodeFlags.TransformableD064, typeof(TransformableD064) },
            { NodeFlags.TransformableD065, typeof(TransformableD065) },
            { NodeFlags.UnknownD066, typeof(UnknownD066) },
        };

        public static Type GetFlaggedNodeType(this NodeFlags flags) => flaggedNodeType[flags];

        public static string ToHexString(this NodeFlags flags) => ((uint)flags).ToString("X4");
    }
}
