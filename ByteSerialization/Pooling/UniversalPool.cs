// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;
using System.Collections.Concurrent;

namespace ByteSerialization.Pooling
{
    // TODO: pooling necessary at all?

    public class UniversalPool
    {
        #region Singleton

        public static UniversalPool Instance { get; } = new UniversalPool();
        private UniversalPool() { }

        #endregion

        #region Fields

        private readonly ConcurrentDictionary<Type, Pool> dictionary =
            new ConcurrentDictionary<Type, Pool>();

        #endregion

        #region Methods

        public T Get<T>() where T : class, new() =>
            (T)GetPool(typeof(T)).Get();

        public object Get(Type type) =>
            GetPool(type).Get();

        private Pool GetPool(Type type) =>
            dictionary.GetOrAdd(type, x => new Pool(x));

        #endregion
    }
}
