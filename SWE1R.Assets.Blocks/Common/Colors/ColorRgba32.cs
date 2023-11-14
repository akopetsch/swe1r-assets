// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

namespace SWE1R.Assets.Blocks.Common.Colors
{
    public struct ColorRgba32
    {
        #region Properties

        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public byte A { get; set; }

        public static readonly ColorRgba32 Pink =
            new ColorRgba32(byte.MaxValue, byte.MaxValue, 0, byte.MaxValue);

        #endregion

        #region Constructor

        public ColorRgba32(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public ColorRgba32(ColorArgbF colorArgbF)
        {
            R = (byte)(colorArgbF.R * byte.MaxValue);
            G = (byte)(colorArgbF.G * byte.MaxValue);
            B = (byte)(colorArgbF.B * byte.MaxValue);
            A = (byte)(colorArgbF.A * byte.MaxValue);
        }

        #endregion
    }
}
