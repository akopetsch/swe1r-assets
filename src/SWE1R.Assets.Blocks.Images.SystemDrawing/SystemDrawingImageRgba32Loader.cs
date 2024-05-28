// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SystemDrawingBitmap = System.Drawing.Bitmap;
using SystemDrawingImage = System.Drawing.Image;

namespace SWE1R.Assets.Blocks.Images.SystemDrawing
{
    public class SystemDrawingImageRgba32Loader
    {
        public ImageRgba32 Load(Stream stream)
        {
            using var systemDrawingBitmap =
                (SystemDrawingBitmap)SystemDrawingImage.FromStream(stream);
            return systemDrawingBitmap.ToImageRgba32().FlipY();
        }
    }
}
