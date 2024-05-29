// SPDX-License-Identifier: GPL-2.0-only

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
