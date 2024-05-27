// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

namespace ByteSerialization.IO.Extensions
{
    public static class NibbleExtensions
    {
        public static byte GetHighNibble(this byte b) => 
            (byte)((b >> 4) & 0x0F);

        public static byte GetLowNibble(this byte b) => 
            (byte)(b & 0x0F);

        public static byte GetNibble(this byte[] bytes, int i) =>
            i % 2 == 0 ?
                bytes[i / 2].GetHighNibble() :
                bytes[i / 2].GetLowNibble();

        public static int GetNibblesCount(this byte[] bytes) =>
            bytes.Length * 2;
    }
}
