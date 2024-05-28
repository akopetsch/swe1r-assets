// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System.IO;

namespace SWE1R.Assets.Blocks.Images
{
    public delegate ImageRgba32 LoadImageRgba32FromFileDelegate(string filename);
    public delegate ImageRgba32 LoadImageRgba32FromStreamDelegate(Stream stream);
}
