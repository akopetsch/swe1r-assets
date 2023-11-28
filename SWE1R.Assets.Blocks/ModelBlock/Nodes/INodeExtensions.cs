// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System.Collections.Generic;
using System.Linq;

namespace SWE1R.Assets.Blocks.ModelBlock.Nodes
{
    public static class INodeExtensions
    {
        public static IEnumerable<INode> GetDescendants(this INode node)
        {
            var d = Enumerable.Empty<INode>();

            // breadth-first
            var c = node.Children?.Distinct() ?? Enumerable.Empty<INode>();
            d = d.Concat(c);
            foreach (INode child in c)
                d = d.Concat(child?.GetDescendants() ?? Enumerable.Empty<INode>());

            return d.Distinct();
        }

        public static IEnumerable<INode> GetSelfAndDescendants(this INode node) =>
            new INode[] { node }.Concat(node.GetDescendants());

        public static IEnumerable<INode> GetLeaves(this INode node) =>
            node.GetLeaves().Where(n => n.Children?.Count > 0);
    }
}
