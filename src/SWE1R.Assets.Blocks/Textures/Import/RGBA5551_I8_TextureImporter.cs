// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.Colors;
using SWE1R.Assets.Blocks.Images;
using System.Diagnostics;

namespace SWE1R.Assets.Blocks.Textures.Import
{
    public class RGBA5551_I8_TextureImporter : TextureImporter
    {
        #region Properties

        public ColorRgba32[] Palette { get; set; }

        #endregion

        #region Constructor

        public RGBA5551_I8_TextureImporter(ImageRgba32 image, ColorRgba32[] palette = null) : // TODO: !!! HACK: second parameter
            base(image) =>
            Palette = palette;

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
                    int index = Image.GetPaletteIndex(x, y, Palette);
                    Debug.Assert(index >= 0);
                    //int i = x * h + y;
                    int i = y * w + x;
                    PixelsBytes[i] = (byte)index;
                }
            }

            // palette
            var paletteImporter = new RGBA5551_PaletteImporter(Image.Palette ?? Palette);
            paletteImporter.Import();
            PaletteBytes = paletteImporter.OutputBytes;
        }

        #endregion
    }
}
