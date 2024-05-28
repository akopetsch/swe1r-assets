// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;
using Swe1rColorRgba32 = SWE1R.Assets.Blocks.Colors.ColorRgba32;
using UnityColor = UnityEngine.Color;
using UnityColor32 = UnityEngine.Color32;

namespace SWE1R.Assets.Blocks.Unity.Extensions
{
    public static class ColorExtensions
    {
        public static UnityColor ToUnityColor(this Swe1rColorRgba32 source)
        {
            float r = (float)source.R / byte.MaxValue;
            float g = (float)source.G / byte.MaxValue;
            float b = (float)source.B / byte.MaxValue;
            float a = (float)source.A / byte.MaxValue;
            return new UnityColor(r, g, b, a);
        }

        public static UnityColor32 ToUnityColor32(this Swe1rColorRgba32 source)
        {
            byte r = source.R;
            byte g = source.G;
            byte b = source.B;
            byte a = source.A;
            return new UnityColor32(r, g, b, a);
        }

        public static Swe1rColorRgba32 ToSwe1rColorRgba32(this UnityColor source)
        {
            byte r = (byte)Math.Round(source.r * byte.MaxValue);
            byte g = (byte)Math.Round(source.g * byte.MaxValue);
            byte b = (byte)Math.Round(source.b * byte.MaxValue);
            byte a = (byte)Math.Round(source.a * byte.MaxValue);
            return new Swe1rColorRgba32(r, g, b, a);
        }

        public static Swe1rColorRgba32 ToSwe1rColorRgba32(this UnityColor32 source)
        {
            byte r = source.r;
            byte g = source.g;
            byte b = source.b;
            byte a = source.a;
            return new Swe1rColorRgba32(r, g, b, a);
        }
    }
}
