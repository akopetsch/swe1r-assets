// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System.Collections.Generic;
using System.Linq;

namespace SWE1R.Assets.Blocks.ModelBlock.Nodes
{
    public static class NodeExtensions
    {
        public static IEnumerable<INode> GetDescendants(this INode node)
        {
            var descendants = new List<INode>();

            // TODO: do not create any lists and use only LINQ
            // NOTE: but lists are useful for debugging
            // ... but this method is so short and definetely correct

            // breadth-first
            var children = node.Children?.Distinct() ?? Enumerable.Empty<INode>();
            descendants.AddRange(children);
            foreach (INode child in children)
                descendants.AddRange(child?.GetDescendants() ?? Enumerable.Empty<INode>());

            return descendants.Distinct();
        }

        public static IEnumerable<INode> GetSelfAndDescendants(this INode node) =>
            new INode[] { node }.Concat(node.GetDescendants());

        public static IEnumerable<INode> GetLeaves(this INode node) =>
            node.GetDescendants().Where(n => n.Children?.Count > 0);
    }
}
