// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Colors;
using ImageSharpRgba32 = SixLabors.ImageSharp.PixelFormats.Rgba32;

namespace SWE1R.Assets.Blocks.Images.ImageSharp
{
    public static class ColorRgba32Extensions
    {
        public static ImageSharpRgba32 ToImageSharp(this ColorRgba32 colorRgba32) =>
            new ImageSharpRgba32(
                colorRgba32.R,
                colorRgba32.G,
                colorRgba32.B,
                colorRgba32.A);
    }
}
