// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;
using System.Collections.Generic;

namespace ByteSerialization.IO.Extensions
{
    public static class DecimalExtensions
    {
        public static decimal ToDecimal(this byte[] bytes, int startIndex)
        {
            var i1 = BitConverter.ToInt32(bytes, startIndex);
            var i2 = BitConverter.ToInt32(bytes, startIndex + 4);
            var i3 = BitConverter.ToInt32(bytes, startIndex + 8);
            var i4 = BitConverter.ToInt32(bytes, startIndex + 12);

            return new decimal(new int[] { i1, i2, i3, i4 });
        }

        internal static byte[] GetBytes(this decimal value)
        {
            var bytes = new List<byte>(sizeof(decimal));
            foreach (int part in decimal.GetBits(value))
                bytes.AddRange(BitConverter.GetBytes(part));
            return bytes.ToArray();
        }
    }
}
