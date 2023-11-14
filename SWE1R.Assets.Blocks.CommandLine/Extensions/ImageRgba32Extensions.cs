// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Common.Images;

namespace SWE1R.Assets.Blocks.CommandLine.Extensions
{
    public static class ImageRgba32Extensions
    {
        public static Image<Rgba32> ToImageSharp(this ImageRgba32 source)
        {
            var result = new Image<Rgba32>(source.Width, source.Height);
            for (int x = 0; x < source.Width; x++)
                for (int y = 0; y < source.Height; y++)
                    result[x, y] = source[x, y].ToImageSharp();
            return result;
        }
    }
}
