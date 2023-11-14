// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Pooling;
using System.Collections.Generic;

namespace ByteSerialization.Components
{
    public class ComponentList : List<Component>, IPoolable
    {
        public event ReleaseEventHandler OnRelease;

        void IPoolable.Release()
        {
            Clear();
            OnRelease();
        }
    }

    //public class ComponentList : ListByType<Component>
    //{

    //}
}
