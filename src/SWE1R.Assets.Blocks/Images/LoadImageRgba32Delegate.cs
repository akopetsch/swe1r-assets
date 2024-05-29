// SPDX-License-Identifier: GPL-2.0-only

using System.IO;

namespace SWE1R.Assets.Blocks.Images
{
    public delegate ImageRgba32 LoadImageRgba32FromFileDelegate(string filename);
    public delegate ImageRgba32 LoadImageRgba32FromStreamDelegate(Stream stream);
}
