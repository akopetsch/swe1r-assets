// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.Colors;
using SystemDrawingColor = System.Drawing.Color;

namespace SWE1R.Assets.Blocks.Images.SystemDrawing
{
    public static class ColorRgba32Extensions
    {
        public static ColorRgba32 ToColorRgba32(this SystemDrawingColor systemDrawingColor) =>
            new ColorRgba32(
                systemDrawingColor.R,
                systemDrawingColor.G,
                systemDrawingColor.B,
                systemDrawingColor.A);
    }
}
