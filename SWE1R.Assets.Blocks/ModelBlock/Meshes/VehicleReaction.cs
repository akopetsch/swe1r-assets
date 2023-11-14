// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes
{
    [Flags]
    public enum VehicleReaction : int
    {
        /// <summary>
        /// zero-gravity on (Oovo IV)
        /// </summary>
        ZOn = 1,
        /// <summary>
        /// zero-gravity off (Oovo IV)
        /// </summary>
        ZOff = 2,
        /// <summary>
        /// ? (several)
        /// </summary>
        Fast = 4,
        /// <summary>
        /// slow (several)
        /// </summary>
        Slow = 8,

        /// <summary>
        /// ? (Baroo Coast)
        /// </summary>
        Swst = 0x10,
        /// <summary>
        /// ? (Ando Prime)
        /// </summary>
        Slip = 0x20,
        /// <summary>
        /// ? (several)
        /// </summary>
        Dust = 0x40,
        /// <summary>
        /// ? (Ando Prime)
        /// </summary>
        Snow = 0x80,

        /// <summary>
        /// ? (Aquilaris, Baroonda, Malastare)
        /// </summary>
        Wet = 0x100,
        /// <summary>
        /// ? (several)
        /// </summary>
        Ruff = 0x200,
        /// <summary>
        /// ? (Ando Prime, Baroonda, Tatooine)
        /// </summary>
        Swmp = 0x400,
        /// <summary>
        /// ? (Baroonda, Ando Prime)
        /// </summary>
        NSnw = 0x800,

        /// <summary>
        /// mirror (Ando Prime)
        /// </summary>
        Mirr = 0x1000,
        /// <summary>
        /// Lava (Baroonda)
        /// </summary>
        Lava = 0x2000,
        /// <summary>
        /// ? (several)
        /// </summary>
        Fall = 0x4000,
        /// <summary>
        /// ? (Ando Prime Centrum, Baroonda)
        /// </summary>
        Soft = 0x8000,

        /// <summary>
        /// ? (several)
        /// </summary>
        NRSp = 0x10000,
        /// <summary>
        /// ? (several)
        /// </summary>
        Flat = 0x20000,

        /// <summary>
        /// ? (Tatooine, Ord Ibanna)
        /// </summary>
        Side = 0x20000000,
    }
}
