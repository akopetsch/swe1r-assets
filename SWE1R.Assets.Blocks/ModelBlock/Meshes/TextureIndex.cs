// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using System;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes
{
    public class TextureIndex 
    {
        // TODO: make this class behavor more like type 'int?', so e.g. have property 'HasValue'

        #region Constants

        private const int _signature = 0x0a000000;
        private const int _mask = 0xFFFFFF;

        #endregion

        #region Properties (serialized)

        [Order(0)]
        internal int SerializedValue { get; set; }

        #endregion

        #region Properties (helper)

        public int Value
        {
            get => GetValue();
            set => SetValue(value);
        }

        #endregion

        #region Methods (helper)

        private int GetValue()
        {
            int masked = SerializedValue & _mask;
            if (masked == _mask)
                return -1;
            else
                return masked;
        }

        private void SetValue(int value)
        {
            if ((value & ~_mask) != 0)
                throw new ArgumentException(
                    $"The value for {nameof(Value)} must be a three-byte, two's complement integer number.", nameof(value));
            if (value == -1)
                SerializedValue = _signature | _mask;
            else
                SerializedValue = _signature | value;
        }

        #endregion

        #region Methods (operators - conversion)

        public static implicit operator int?(TextureIndex value) =>
            value?.Value;

        public static implicit operator TextureIndex(int value) =>
            new TextureIndex() { Value = value };

        #endregion

        #region Methods (: object

        public override string ToString() => 
            Value.ToString();

        #endregion
    }
}
