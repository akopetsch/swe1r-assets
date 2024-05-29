// SPDX-License-Identifier: MIT

using System.IO;

namespace SWE1R.Assets.Blocks.Images
{
    public delegate ImageRgba32 LoadImageRgba32FromFileDelegate(string filename);
    public delegate ImageRgba32 LoadImageRgba32FromStreamDelegate(Stream stream);
}
