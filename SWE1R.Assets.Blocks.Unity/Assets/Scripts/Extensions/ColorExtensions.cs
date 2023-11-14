// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using Swe1rColorArgbF = SWE1R.Assets.Blocks.Common.Colors.ColorArgbF;
using Swe1rColorRgba32 = SWE1R.Assets.Blocks.Common.Colors.ColorRgba32;
using Swe1rColorRgbB = SWE1R.Assets.Blocks.Common.Colors.ColorRgb<byte>;
using UnityColor = UnityEngine.Color;

namespace SWE1R.Assets.Blocks.Unity.Extensions
{
    public static class ColorExtensions
    {
        public static UnityColor ToUnityColor(this Swe1rColorArgbF source) =>
            new UnityColor(source.R, source.G, source.B, source.A);

        public static UnityColor ToUnityColor(this Swe1rColorRgba32 source)
        {
            float r = (float)source.R / byte.MaxValue;
            float g = (float)source.G / byte.MaxValue;
            float b = (float)source.B / byte.MaxValue;
            float a = (float)source.A / byte.MaxValue;
            return new UnityColor(r, g, b, a);
        }

        public static UnityColor ToUnityColor(this Swe1rColorRgbB source)
        {
            float r = (float)source.R / byte.MaxValue;
            float g = (float)source.G / byte.MaxValue;
            float b = (float)source.B / byte.MaxValue;
            return new UnityColor(r, g, b);
        }

        public static Swe1rColorArgbF ToSwe1rColorArgbF(this UnityColor source) =>
            new Swe1rColorArgbF(source.r, source.g, source.b, source.a);

        public static Swe1rColorRgba32 ToSwe1rColorRgba32(this UnityColor source)
        {
            byte r = (byte)(source.r * byte.MaxValue);
            byte g = (byte)(source.g * byte.MaxValue);
            byte b = (byte)(source.b * byte.MaxValue);
            byte a = (byte)(source.a * byte.MaxValue);
            // TODO: consider Math.Round instead of casting to byte (rounding down)
            return new Swe1rColorRgba32(r, g, b, a);
        }

        public static Swe1rColorRgbB ToSwe1rColorRgbB(this UnityColor source)
        {
            byte r = (byte)(source.r * byte.MaxValue);
            byte g = (byte)(source.g * byte.MaxValue);
            byte b = (byte)(source.b * byte.MaxValue);
            // TODO: consider Math.Round instead of casting to byte (rounding down)
            return new Swe1rColorRgbB(r, g, b);
        }
    }
}
