// SPDX-License-Identifier: MIT

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
