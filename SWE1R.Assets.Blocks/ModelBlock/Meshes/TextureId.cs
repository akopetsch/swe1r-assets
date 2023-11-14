// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using System;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes
{
    public class TextureId
    {
        private const int signature = 0x0a000000;
        private const int mask = 0xFFFFFF;
        
        [Order(0)] internal int IdField { get; set; }

        public int Id
        {
            get => GetId();
            set => SetId(value);
        }

        private int GetId()
        {
            int masked = IdField & mask;
            if (masked == mask)
                return -1;
            else
                return masked;
        }

        private void SetId(int value)
        {
            if ((IdField & ~mask) != 0)
                throw new ArgumentException(
                    $"The value for {nameof(Id)} must be a three-byte, two's complement integer number.", nameof(value));
            if (value == -1)
                IdField = signature | mask;
            else
                IdField = signature | value;
        }
    }
}
