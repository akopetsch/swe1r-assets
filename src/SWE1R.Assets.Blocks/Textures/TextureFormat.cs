// SPDX-License-Identifier: MIT

namespace SWE1R.Assets.Blocks.Textures
{
    /// <summary>
    /// <see href="// https://github.com/Olganix/Sw_Racer/issues/9">Olganix/Sw_Racer - 'Known texture formats #9'</see>
    /// </summary>
    public enum TextureFormat : short
    {
        /// <summary>
        /// 32 bit defining all RGBA channels with 8 bit per channel.
        /// </summary>
        RGBA32 = 0x0003,
        /// <summary>
        /// 4 bit defining all RGBA channels equally.
        /// </summary>
        FourBitGrayscaleAndAlpha = 0x0400,
        /// <summary>
        /// 8 bit defining all RGB channels equally.
        /// </summary>
        EightBitGrayscale = 0x0401,
        /// <summary>
        /// 4 bits indexing a
        /// </summary>
        RGBA5551_I4 = 0x0200, // TODO: xml comment
        /// <summary>
        /// 8 bits indexing a
        /// </summary>
        RGBA5551_I8 = 0x0201, // TODO: xml comment
    }
}
