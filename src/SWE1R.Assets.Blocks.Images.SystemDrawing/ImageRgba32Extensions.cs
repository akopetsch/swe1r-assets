// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SystemDrawingBitmap = System.Drawing.Bitmap;

namespace SWE1R.Assets.Blocks.Images.SystemDrawing
{
    public static class ImageRgba32Extensions
    {
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

            return imageRgba32.FlipY();
        }
    }
}
