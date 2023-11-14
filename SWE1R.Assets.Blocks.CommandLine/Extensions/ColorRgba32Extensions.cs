// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Common.Colors;

namespace SWE1R.Assets.Blocks.CommandLine.Extensions
{
    public static class ColorRgba32Extensions
    {
        public static Rgba32 ToImageSharp(this ColorRgba32 source) =>
            new Rgba32(source.R, source.G, source.B, source.A);
    }
}
