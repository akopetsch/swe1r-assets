// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;
using UnityEngine;

namespace SWE1R.Assets.Blocks.Unity
{
    /// <summary>
    /// Serializable Nullable (SN) Does the same as C# System.Nullable, except it's an ordinary
    /// serializable struct, allowing unity to serialize it and show it in the inspector.
    /// <para>
    /// The code is a refactored version of the code from:
    /// <see href="https://discussions.unity.com/t/why-doesnt-unity-property-editor-show-a-nullable-variable/226218/3"/>
    /// </para>
    /// </summary>
    [Serializable]
    public struct SerializableNullable<T> where T : struct
    {
        #region Properties

        public T Value
        {
            get
            {
                if (!HasValue)
                    throw new InvalidOperationException("Serializable nullable object must have a value.");
                return v;
            }
        }

        public bool HasValue =>
            hasValue;

        #endregion

        #region Fields

        [SerializeField]
        private T v;

        [SerializeField]
        private bool hasValue;

        #endregion

        #region Constructors

        public SerializableNullable(bool hasValue, T v)
        {
            this.v = v;
            this.hasValue = hasValue;
        }

        private SerializableNullable(T v)
        {
            this.v = v;
            hasValue = true;
        }

        #endregion

        #region Operators

        public static implicit operator SerializableNullable<T>(T value) =>
            new SerializableNullable<T>(value);

        public static implicit operator SerializableNullable<T>(T? value) =>
            value.HasValue ? new SerializableNullable<T>(value.Value) : new SerializableNullable<T>();

        public static implicit operator T?(SerializableNullable<T> value) =>
            value.HasValue ? value.Value : null;

        #endregion
    }
}
