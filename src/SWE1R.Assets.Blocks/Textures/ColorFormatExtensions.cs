// SPDX-License-Identifier: MIT

using System;

namespace SWE1R.Assets.Blocks.Textures
{
    public static class ColorFormatExtensions
    {
        public static int GetBpp(this ColorFormat colorFormat)
        {
            switch (colorFormat)
            {
                case ColorFormat.RGBA32: return 32;
                case ColorFormat.RGBA5551: return 16;
                case ColorFormat.FourBitGrayscale: return 4;
                case ColorFormat.FourBitGrayscaleAndAlpha: return 4;
                default: throw new InvalidOperationException();
            }
        }
    }
}
