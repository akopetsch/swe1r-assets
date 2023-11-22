// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;

namespace SWE1R.Assets.Blocks.Common.Colors
{
    public class ColorRgba5551 // TODO: struct
    {
        #region Properties (serialized)

        // TODO: use ByteSerializer

        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public bool A { get; set; }

        #endregion

        #region Properties (helper)

        public byte[] Bytes =>
            new byte[] { R, G, B, (byte)(A ? 1 : 0) };

        public static int StructureSize =>
            2;

        #endregion

        #region Constructor

        public ColorRgba5551(byte r, byte g, byte b, bool a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        #endregion

        #region Methods (helper)

        public static ColorArgbF FromRgb565(short color)
        {
            double r = ((color & (0b11111 << (6 + 5))) >> (6 + 5)) / (Math.Pow(2, 5) - 1);
            double g = ((color & (0b111111 << (6))) >> (6)) / (Math.Pow(2, 6) - 1);
            double b = (color & 0b11111) / (Math.Pow(2, 5) - 1);
            return new ColorArgbF(1, (float)r, (float)g, (float)b);
        }

        #endregion
    }
}
