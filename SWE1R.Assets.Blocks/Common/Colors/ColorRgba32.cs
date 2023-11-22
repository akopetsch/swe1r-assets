// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Common.Vectors;
using System;

namespace SWE1R.Assets.Blocks.Common.Colors
{
    public class ColorRgba32 // TODO: struct
    {
        #region Properties (serialized)

        // TODO: use ByteSerializer

        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public byte A { get; set; }

        #endregion

        #region Properties (helper)

        public byte[] Bytes =>
            new byte[] { R, G, B, A };

        public static int StructureSize => 
            4;

        public static ColorRgba32 Pink { get; } =
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

        #region Methods (operators - conversion)

        public static explicit operator ColorRgba5551(ColorRgba32 c) =>
            new ColorRgba5551((byte)v.X, (byte)v.Y, (byte)v.Z);

        #endregion

        #region Methods (: object)

        public override int GetHashCode() => 
            HashCode.Combine(R, G, B, A);

        #endregion
    }
}
