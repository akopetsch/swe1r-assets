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
            { NodeFlags.MeshGroupNode, typeof(MeshGroupNode) },
            { NodeFlags.BasicNode, typeof(BasicNode) },
            { NodeFlags.SelectorNode, typeof(SelectorNode) },
            { NodeFlags.LodSelectorNode, typeof(LodSelectorNode) },
            { NodeFlags.TransformedNode, typeof(TransformedNode) },
            { NodeFlags.TransformedWithPivotNode, typeof(TransformedWithPivotNode) },
            { NodeFlags.TransformedComputedNode, typeof(TransformedComputedNode) },
        };

        public static Type GetFlaggedNodeType(this NodeFlags flags) => flaggedNodeType[flags];

        public static string ToHexString(this NodeFlags flags) => ((uint)flags).ToString("X4");
    }
}
