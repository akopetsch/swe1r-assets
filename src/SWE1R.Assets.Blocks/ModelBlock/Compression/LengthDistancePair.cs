// SPDX-License-Identifier: GPL-2.0-only

namespace SWE1R.Assets.Blocks.Common.Compression
{
    internal struct LengthDistancePair
    {
        internal int Length;
        internal int Distance;

        private int LengthBits => (Length - 2) & 0x000F << 12;
        private int DistanceBits => Distance & 0x0FFF;
        private ushort Bits => (ushort)(LengthBits | DistanceBits);

        internal static int MaxLength { get; } = new LengthDistancePair(ushort.MaxValue).Length + 1;
        internal static int MaxDistance { get; } = new LengthDistancePair(ushort.MaxValue).Distance + 1;

        public LengthDistancePair(ushort bits)
        {
            Length = ((bits & 0xF000) >> 12) + 2;
            Distance = bits & 0x0FFF;
        }

        public override string ToString() => $"{Length}, {Distance}";

        public static explicit operator ushort(LengthDistancePair pair) => pair.Bits;
        public static explicit operator LengthDistancePair(ushort bits) => new LengthDistancePair(bits);
    }
}
