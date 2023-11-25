// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;

namespace SWE1R.Assets.Blocks.ModelBlock.Materials
{
    public enum ColorFormat
    {
        /// <summary>
        /// 32 bit defining all RGBA channels with 8 bit per R, G, B and A channel.
        /// </summary>
        RGBA32,
        /// <summary>
        /// 16 bit defining all RGBA channels with 5 bit per R, G and B channel and 1 bit for A channel.
        /// </summary>
        RGBA5551,
        /// <summary>
        /// 4 bit defining all RGB channels.
        /// </summary>
        FourBitGrayscale,
        /// <summary>
        /// 4 bit defining all RGBA channels.
        /// </summary>
        FourBitGrayscaleAndAlpha,
    }

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
