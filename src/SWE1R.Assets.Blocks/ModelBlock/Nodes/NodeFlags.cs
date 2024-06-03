// SPDX-License-Identifier: MIT

using System;
using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.ModelBlock.Nodes
{
    public enum NodeFlags : uint
    {
        MeshGroupNode =
            0b0_0011_0000_0110_0100, // 0x3064
        BasicNode =
            0b0_0101_0000_0110_0100, // 0x5064
        SelectorNode =
            0b0_0101_0000_0110_0101, // 0x5065
        LodSelectorNode =
            0b0_0101_0000_0110_0110, // 0x5066
        TransformedNode =
            0b0_1101_0000_0110_0100, // 0xD064
        TransformedWithPivotNode =
            0b0_1101_0000_0110_0101, // 0xD065
        TransformedComputedNode =
            0b0_1101_0000_0110_0110, // 0xD066
    }

    public static class NodeFlagsExtensions
    {
        private static Dictionary<NodeFlags, Type> flaggedNodeType = new Dictionary<NodeFlags, Type>()
        {
            { NodeFlags.MeshGroupNode, typeof(MeshGroup3064) },
            { NodeFlags.BasicNode, typeof(Group5064) },
            { NodeFlags.SelectorNode, typeof(Group5065) },
            { NodeFlags.LodSelectorNode, typeof(Group5066) },
            { NodeFlags.TransformedNode, typeof(TransformableD064) },
            { NodeFlags.TransformedWithPivotNode, typeof(TransformableD065) },
            { NodeFlags.TransformedComputedNode, typeof(UnknownD066) },
        };

        public static Type GetFlaggedNodeType(this NodeFlags flags) => flaggedNodeType[flags];

        public static string ToHexString(this NodeFlags flags) => ((uint)flags).ToString("X4");
    }
}
