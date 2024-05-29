// SPDX-License-Identifier: GPL-2.0-only

using SWE1R.Assets.Blocks.Colors;
using SWE1R.Assets.Blocks.Images;
using System;
using System.Linq;

namespace SWE1R.Assets.Blocks.Textures.Import
{
    public class RGBA32_TextureImporter : TextureImporter
    {
        #region Constructor

        public RGBA32_TextureImporter(ImageRgba32 image) :
            base(image)
        { }

        #endregion

        #region Methods (: TextureImporter)

        public override void Import()
        {
            int w = Image.Width;
            int h = Image.Height;

            PixelsBytes = new byte[w * h * ColorRgba32.StructureSize];
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    //int i = x * h + y;
                    int i = y * w + x;
                    byte[] bytes = Image[x, y].Bytes.Reverse().ToArray(); // TODO: use EndianBinaryWriter
                    Array.Copy(bytes, 0, PixelsBytes, i * bytes.Length, bytes.Length);
                }
            }
        }

        #endregion
    }
}
