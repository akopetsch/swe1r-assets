// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.CommandLine.Extensions;
using SWE1R.Assets.Blocks.Common.Images;
using SystemDrawingBitmap = System.Drawing.Bitmap;
using SystemDrawingImage = System.Drawing.Image;

namespace SWE1R.Assets.Blocks.CommandLine
{
    public static class SystemDrawingImageRgba32Loader
    {
        public static ImageRgba32 LoadImageRgba32(string imageFilename)
        {
            using var systemDrawingBitmap =
                (SystemDrawingBitmap)SystemDrawingImage.FromFile(imageFilename);
            return systemDrawingBitmap.ToImageRgba32();
        }
    }
}
