// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Textures;
using SWE1R.Assets.Blocks.Textures.Export;

namespace SWE1R.Assets.Blocks.SpriteBlock.Export
{
    public class SpriteTileExporter : TextureExporter
    {
        #region Properties (input)

        public SpriteTile SpriteTile { get; set; }
        public Sprite Sprite { get; set; }

        #endregion

        #region Constructor

        public SpriteTileExporter(SpriteTile spriteTile, Sprite sprite) : 
            base(
                spriteTile.PixelsBytes, 
                sprite.Format, 
                spriteTile.Width, 
                spriteTile.Height, 
                sprite.Palette?.Colors)
        {
            SpriteTile = spriteTile;
            Sprite = sprite;
        }

        #endregion

        #region Methods (: TextureExporter)

        protected override int GetVirtualWidth()
        {
            int bpp = Sprite.Format.GetBpp();

            // TODO: simplify:
            float bytesPerPixel = (float)bpp / 8;
            int bytesPerLine = (int)(Width * bytesPerPixel);
            int virtualBytesPerLine = bytesPerLine & 0xfff8; // round down by 8 (padding)
            if (bytesPerLine % 8 > 0)
                virtualBytesPerLine += 8;
            return (int)(virtualBytesPerLine / bytesPerPixel);
        }

        #endregion
    }
}
