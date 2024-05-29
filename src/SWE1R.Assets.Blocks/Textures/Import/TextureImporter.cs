// SPDX-License-Identifier: GPL-2.0-only

using SWE1R.Assets.Blocks.Images;

namespace SWE1R.Assets.Blocks.Textures.Import
{
    public abstract class TextureImporter
    {
        #region Properties (input)

        public ImageRgba32 Image { get; }

        #endregion

        #region Properties (output)

        public byte[] PixelsBytes { get; protected set; }
        public byte[] PaletteBytes { get; protected set; }

        #endregion

        #region Constructor

        public TextureImporter(ImageRgba32 image)
        {
            Image = image;
        }

        #endregion

        #region Methods

        public abstract void Import();

        #endregion
    }
}
