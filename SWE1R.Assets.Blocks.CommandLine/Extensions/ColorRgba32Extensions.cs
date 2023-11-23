// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Common.Colors;
using ImageSharpRgba32 = SixLabors.ImageSharp.PixelFormats.Rgba32;
using SystemDrawingColor = System.Drawing.Color;

namespace SWE1R.Assets.Blocks.CommandLine.Extensions
{
    public static class ColorRgba32Extensions
    {
        public static ImageSharpRgba32 ToImageSharp(this ColorRgba32 colorRgba32) =>
            new ImageSharpRgba32(
                colorRgba32.R, 
                colorRgba32.G, 
                colorRgba32.B, 
                colorRgba32.A);

        public static ColorRgba32 ToColorRgba32(this SystemDrawingColor systemDrawingColor) =>
            new ColorRgba32(
                systemDrawingColor.R,
                systemDrawingColor.G,
                systemDrawingColor.B,
                systemDrawingColor.A);
    }
}
