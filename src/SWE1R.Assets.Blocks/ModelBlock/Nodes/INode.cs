// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.ModelBlock.Nodes
{
    public interface INode
    {
        List<INode> Children { get; set; }
    }
}
