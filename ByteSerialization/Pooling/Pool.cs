// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;
using System.Collections.Generic;
using System.Linq;

namespace ByteSerialization.Pooling
{
    public delegate void ReleaseEventHandler();

    public interface IPoolable
    {
        event ReleaseEventHandler OnRelease;
        void Release();
    }
    
    public class Pool
    {
        public Type Type { get; set; }

        private List<IPoolable> Unused { get; } = new List<IPoolable>();
        private List<IPoolable> Used { get; } = new List<IPoolable>();

        public Pool(Type type)
        {
            Type = type;
        }

        public IPoolable Get() => UseUnused() ?? UseNew();

        private IPoolable UseUnused()
        {
            IPoolable o = Unused.FirstOrDefault();
            if (o != null)
            {
                Unused.Remove(o);
                Used.Remove(o);
            }
            return o;
        }

        private IPoolable UseNew()
        {
            var o = (IPoolable)Activator.CreateInstance(Type);
            //Used.Add(o);
            o.OnRelease += () =>
            {
                Used.Remove(o);
                Unused.Add(o);
            };
            return o;
        }
    }
}
