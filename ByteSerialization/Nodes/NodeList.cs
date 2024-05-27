// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Pooling;
using System.Collections.Generic;

namespace ByteSerialization.Nodes
{
    public class NodeList : List<Node>, IPoolable
    {
        public event ReleaseEventHandler OnRelease;
        
        void IPoolable.Release()
        {
            Clear();
            OnRelease();
        }
    }
}
