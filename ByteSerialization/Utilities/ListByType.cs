// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Pooling;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace ByteSerialization.Utilities
{
    public class ListByType<T> : IList<T>, IPoolable
    {
        #region Fields

        private List<T> items =
            new List<T>();

        private ConcurrentDictionary<Type, HashSet<T>> hashSetByType =
            new ConcurrentDictionary<Type, HashSet<T>>();

        #endregion

        #region Members (: IList)

        public T this[int index]
        {
            get => items[index];
            set
            {
                if (index == Count - 1)
                {
                    RemoveAt(index);
                    Add(value);
                }
                else
                {
                    RemoveAt(index);
                    Insert(index, value);
                }
            }
        }

        public int Count => items.Count;
        public bool IsReadOnly => false;

        public void Add(T item)
        {
            items.Add(item);
            AddByType(item);
        }

        public void Clear()
        {
            items.Clear();
            hashSetByType.Clear();
        }

        public bool Contains(T item) =>
            items.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) =>
            items.CopyTo(array, arrayIndex);

        public IEnumerator<T> GetEnumerator() =>
            items.GetEnumerator();

        public int IndexOf(T item) =>
            items.IndexOf(item);

        public void Insert(int index, T item)
        {
            items.Insert(index, item);
            AddByType(item);
        }

        public bool Remove(T item)
        {
            if (items.Remove(item))
            {
                RemoveByType(item);
                return true;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            T item = this[index];
            if (item != null)
                Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            items.GetEnumerator();

        #endregion

        #region Methods (helper)

        public IEnumerable<TResult> OfType<TResult>() =>
            GetHashSetByType(typeof(TResult)).Cast<TResult>();

        private void AddByType(T item)
        {
            Type type = item.GetType();

            // class types
            foreach (Type classType in type.GetHierarchy())
                GetHashSetByType(classType).Add(item);

            // interface types
            foreach (Type interfaceType in type.GetInterfaces())
                GetHashSetByType(interfaceType).Add(item);
        }

        private void RemoveByType(T item)
        {
            Type type = item.GetType();

            // class types
            foreach (Type classType in type.GetHierarchy())
                GetHashSetByType(classType).Remove(item);

            // interface types
            foreach (Type interfaceType in type.GetInterfaces())
                GetHashSetByType(interfaceType).Remove(item);
        }

        private HashSet<T> GetHashSetByType(Type type) =>
            hashSetByType.GetOrAdd(type, x => new HashSet<T>());

        #endregion

            #region Members (: IPoolable)

        public event ReleaseEventHandler OnRelease;

        void IPoolable.Release()
        {
            items.Clear();
            hashSetByType.Clear();

            items = null;
            hashSetByType = null;

            OnRelease();
        }

        #endregion
    }
}
