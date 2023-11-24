// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using System;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes
{
    public class TextureId
    {
        #region Constants

        private const int _signature = 0x0a000000;
        private const int _mask = 0xFFFFFF;

        #endregion

        #region Properties (serialized)

        [Order(0)]
        internal int IdField { get; set; }

        #endregion

        #region Properties (helper)

        public int Id
        {
            get => GetId();
            set => SetId(value);
        }

        #endregion

        #region Methods (helper)

        private int GetId()
        {
            int masked = IdField & _mask;
            if (masked == _mask)
                return -1;
            else
                return masked;
        }

        private void SetId(int value)
        {
            if ((value & ~_mask) != 0)
                throw new ArgumentException(
                    $"The value for {nameof(Id)} must be a three-byte, two's complement integer number.", nameof(value));
            if (value == -1)
                IdField = _signature | _mask;
            else
                IdField = _signature | value;
        }

        #endregion

        #region Methods (operators - conversion)

        public static implicit operator int(TextureId value) =>
            value.Id;

        public static implicit operator TextureId(int value) =>
            new TextureId() { Id = value };

        #endregion

        #region Methods (: object

        public override string ToString() => 
            Id.ToString();

        #endregion
    }
}
