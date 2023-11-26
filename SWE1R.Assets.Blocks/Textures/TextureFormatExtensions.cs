// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;

namespace SWE1R.Assets.Blocks.Textures
{
    public static class TextureFormatExtensions
    {
        public static byte GetByte1(this TextureFormat textureFormat) =>
            (byte)((short)textureFormat >> 8);

        public static byte GetByte2(this TextureFormat textureFormat) =>
            (byte)(short)textureFormat;

        public static int GetBpp(this TextureFormat textureFormat)
        {
            switch (textureFormat)
            {
                case TextureFormat.RGBA32: return 32;
                case TextureFormat.FourBitGrayscaleAndAlpha: return 4;
                case TextureFormat.EightBitGrayscale: return 8;
                case TextureFormat.I4_RGBA5551: return 4;
                case TextureFormat.I8_RGBA5551: return 8;
                default: throw new InvalidOperationException();
            }
        }

        public static bool HasPalette(this TextureFormat textureFormat) =>
            textureFormat == TextureFormat.I4_RGBA5551 ||
            textureFormat == TextureFormat.I8_RGBA5551;

        public static int GetPaletteSize(this TextureFormat textureFormat)
        {
            switch (textureFormat)
            {
                case TextureFormat.I4_RGBA5551: return 1 << 4;
                case TextureFormat.I8_RGBA5551: return 1 << 8;
                default: return 0;
            }
        }
    }
}
