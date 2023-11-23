// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Common.Images;
using SystemDrawingBitmap = System.Drawing.Bitmap;

namespace SWE1R.Assets.Blocks.CommandLine.Extensions
{
    public static class ImageRgba32Extensions
    {
        public static Image<Rgba32> ToImageSharp(this ImageRgba32 imageRgba32)
        {
            var result = new Image<Rgba32>(imageRgba32.Width, imageRgba32.Height);
            for (int x = 0; x < imageRgba32.Width; x++)
                for (int y = 0; y < imageRgba32.Height; y++)
                    result[x, y] = imageRgba32[x, y].ToImageSharp();
            return result;
        }

        public static ImageRgba32 ToImageRgba32(this SystemDrawingBitmap systemDrawingBitmap)
        {
            int w = systemDrawingBitmap.Width;
            int h = systemDrawingBitmap.Height;
            
            var imageRgba32 = new ImageRgba32(w, h);
            
            // pixels
            for (int x = 0; x < w; x++)
                for (int y = 0; y < h; y++)
                    imageRgba32[x, y] = 
                        systemDrawingBitmap.GetPixel(x, y).ToColorRgba32();

            // palette
            imageRgba32.Palette = 
                systemDrawingBitmap.Palette.Entries.Select(x => x.ToColorRgba32()).ToArray();

            return imageRgba32;
        }

        
    }
}
