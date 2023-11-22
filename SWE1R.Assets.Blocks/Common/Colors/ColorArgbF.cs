// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;

namespace SWE1R.Assets.Blocks.Common.Colors
{
    public struct ColorArgbF
    {
        public float A;
        public float R;
        public float G;
        public float B;

        public static readonly ColorArgbF Pink = 
            new ColorArgbF(a: 1, r: 1, g: 0, b: 1);

        public ColorArgbF(float a, float r, float g, float b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        public static ColorArgbF FromRgba5551(short color)
        {
            float r = ((color & 0xf800) >> 11) / 31f;
            float g = ((color & 0x07c0) >> 6) / 31f;
            float b = ((color & 0x003e) >> 1) / 31f;
            float a = color & 1;
            return new ColorArgbF(a, r, g, b);
        }

        public short ToRgba5551()
        {
            byte r = (byte)Math.Round(R * 31);
            byte g = (byte)Math.Round(G * 31);
            byte b = (byte)Math.Round(B * 31);
            byte a = (byte)Math.Round(A * 31);
            return (short)((r << 11) | (g << 6) | (b << 1) | a);
        }
    }
}
