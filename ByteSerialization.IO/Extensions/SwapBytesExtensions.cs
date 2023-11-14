// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System.Runtime.CompilerServices;

namespace ByteSerialization.IO.Extensions
{
    public static class SwapBytesExtensions
    {
        #region Constants

        private const uint mask0 = unchecked((uint)0xFF << (0 * 8));
        private const uint mask1 = unchecked((uint)0xFF << (1 * 8));
        private const uint mask2 = unchecked((uint)0xFF << (2 * 8));
        private const uint mask3 = unchecked((uint)0xFF << (3 * 8));
        private const ulong mask4 = unchecked((ulong)0xFF << (4 * 8));
        private const ulong mask5 = unchecked((ulong)0xFF << (5 * 8));
        private const ulong mask6 = unchecked((ulong)0xFF << (6 * 8));
        private const ulong mask7 = unchecked((ulong)0xFF << (7 * 8));

        #endregion

        #region Methods (short/ushort)

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short SwapBytes(this short x) => (short)SwapBytes((ushort)x);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort SwapBytes(this ushort x) =>
            (ushort)(x << 8 | (byte)(x >> 8));

        #endregion

        #region Methods (int/uint)

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int SwapBytes(this int x) => (int)SwapBytes((uint)x);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint SwapBytes(this uint x) =>
            /* 3 */ (x << 24) |
            /* 2 */ ((x << 8) & mask2) |
            /* 1 */ ((x >> 8) & mask1) |
            /* 0 */ (x >> 24);

        #endregion

        #region Methods (long/ulong)

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long SwapBytes(this long x) => (long)SwapBytes((ulong)x);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong SwapBytes(this ulong x) => (ulong)(
            /* 7 */ ((x << 56) & mask7) |
            /* 6 */ ((x << 40) & mask6) |
            /* 5 */ ((x << 24) & mask5) |
            /* 4 */ ((x << 8) & mask4) |
            /* 3 */ ((x >> 8) & mask3) |
            /* 2 */ ((x >> 24) & mask2) |
            /* 1 */ ((x >> 40) & mask1) |
            /* 0 */ (x >> 56));

        #endregion
    }
}
