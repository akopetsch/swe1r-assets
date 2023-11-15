// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes
{
    public enum TextureDataFormat
    {
        /// <summary>
        /// 32 bit defining all RGBA channels with 8 bit per channel.
        /// </summary>
        RGBA32,
        /// <summary>
        /// 4 bit defining all RGBA channels.
        /// </summary>
        FourBitGrayscaleAndAlpha,
        /// <summary>
        /// 8 bit defining all RGB channels
        /// </summary>
        EightBitGrayscale,
        /// <summary>
        /// 4 bits indexing a 
        /// </summary>
        I4_RGBA5551,
        /// <summary>
        /// 8 bits indexing a 
        /// </summary>
        I8_RGBA5551,
    }

    public static class TextureDataFormatExtensions
    {
        public static int GetBpp(this TextureDataFormat textureDataFormat)
        {
            switch (textureDataFormat)
            {
                case TextureDataFormat.RGBA32: return 32;
                case TextureDataFormat.FourBitGrayscaleAndAlpha: return 4;
                case TextureDataFormat.EightBitGrayscale: return 8;
                case TextureDataFormat.I4_RGBA5551: return 4;
                case TextureDataFormat.I8_RGBA5551: return 8;
                default: throw new InvalidOperationException();
            }
        }

        public static bool IsIndexFormat(this TextureDataFormat textureDataFormat) =>
            textureDataFormat == TextureDataFormat.I4_RGBA5551 ||
            textureDataFormat == TextureDataFormat.I8_RGBA5551;
    }
}
