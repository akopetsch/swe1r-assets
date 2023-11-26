// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Colors;
using SWE1R.Assets.Blocks.Images;
using System;
using System.Diagnostics;
using System.Linq;

namespace SWE1R.Assets.Blocks.Textures.Import
{
    public class RGBA5551_I8_TextureImporter : TextureImporter
    {
        #region Constructor

        public RGBA5551_I8_TextureImporter(ImageRgba32 image) : 
            base(image)
        { }

        #endregion

        #region Methods (: TextureImporter)

        public override void Import()
        {
            int w = Image.Width;
            int h = Image.Height;

            // indices
            PixelsBytes = new byte[w * h];
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    int index = Image.GetPaletteIndex(x, y);
                    Debug.Assert(index >= 0);
                    //int i = x * h + y;
                    int i = y * w + x;
                    PixelsBytes[i] = (byte)index;
                }
            }

            // palette
            byte[] palette = Image.Palette
                .Select(c => (ColorRgba5551)c)
                .SelectMany(c => c.Bytes.Reverse()) // TODO: use EndianBinaryWrite
                .ToArray();
            PaletteBytes = new byte[512];
            Array.Copy(palette, PaletteBytes, palette.Length);
        }

        #endregion
    }
}
