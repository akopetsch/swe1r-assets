// SPDX-License-Identifier: GPL-2.0-only

using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.ModelBlock.Nodes
{
    public interface INode
    {
        List<INode> Children { get; set; }
    }
}
