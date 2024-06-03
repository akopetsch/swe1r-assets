// SPDX-License-Identifier: MIT

using System;
using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.ModelBlock.Nodes
{
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
