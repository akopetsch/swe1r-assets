// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Pooling;
using System.Collections.Generic;
using System.Linq;

namespace ByteSerialization.Components.Values.Composites.Records
{
    public class PropertyComponentList : List<PropertyComponent>, IPoolable
    {
        #region Members (: IPoolable)

        public event ReleaseEventHandler OnRelease;

        void IPoolable.Release()
        {
            Clear();
            OnRelease();
        }

        #endregion

        public PropertyComponent this[string name] => 
            this.FirstOrDefault(p => p.Name.Equals(name));
    }
}
