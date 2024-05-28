// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

namespace SWE1R.Assets.Blocks.Images.ImageSharp
{
    public static class ImageRgba32Extensions
    {
        public static Image<Rgba32> ToImageSharp(this ImageRgba32 imageRgba32)
        {
            var result = new Image<Rgba32>(imageRgba32.Width, imageRgba32.Height);
            imageRgba32 = imageRgba32.FlipY();
            for (int x = 0; x < imageRgba32.Width; x++)
                for (int y = 0; y < imageRgba32.Height; y++)
                    result[x, y] = imageRgba32[x, y].ToImageSharp();
            return result;
        }
    }
}