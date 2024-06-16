// SPDX-License-Identifier: MIT

using ByteSerialization.IO;
using SWE1R.Assets.Blocks.Colors;
using SWE1R.Assets.Blocks.Images;
using System;

namespace SWE1R.Assets.Blocks.Textures.Import
{
    public class RGBA32_TextureImporter : TextureImporter
    {
        #region Constructor

        public RGBA32_TextureImporter(ImageRgba32 image, Endianness endianness) :
            base(image, endianness)
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
                    ColorRgba32 p = Image[x, y];
                    p.BytesValue = BytesSwapper.SwapIf(p.BytesValue, Endianness == Endianness.BigEndian);
                    Array.Copy(
                        sourceArray: p.Bytes, 
                        sourceIndex: 0, 
                        destinationArray: PixelsBytes, 
                        destinationIndex: i * p.Bytes.Length, 
                        length: p.Bytes.Length);
                }
            }
        }

        #endregion
    }
}
