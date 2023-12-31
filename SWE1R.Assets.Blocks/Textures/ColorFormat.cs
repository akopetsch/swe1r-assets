﻿// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

namespace SWE1R.Assets.Blocks.Textures
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
}
