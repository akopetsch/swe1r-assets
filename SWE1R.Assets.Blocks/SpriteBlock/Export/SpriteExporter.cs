// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Images;

namespace SWE1R.Assets.Blocks.SpriteBlock.Export
{
    public class SpriteExporter
    {
        #region Properties (input)

        public Sprite Sprite { get; set; }

        #endregion

        #region Properties (output)

        public ImageRgba32 Image { get; set; }

        #endregion

        #region Constructor

        public SpriteExporter(Sprite sprite)
        {
            Sprite = sprite;
        }

        #endregion

        #region Methods

        public void Export()
        {
            Image = new ImageRgba32(Sprite.Width, Sprite.Height);
            for (int tileY = 0; tileY < Sprite.TilesGridHeight; tileY++)
            {
                for (int tileX = 0; tileX < Sprite.TilesGridWidth; tileX++)
                {
                    SpriteTile tile = Sprite.GetTile(tileX, tileY);
                    if (tile != null)
                    {
                        var tileExporter = new SpriteTileExporter(tile, Sprite);
                        tileExporter.Export();
                        (int spriteX, int spriteY) = Sprite.GetTilePosition(tileX, tileY);
                        Image.Insert(tileExporter.Image.FlipY(), spriteX, spriteY);
                    }
                }
            }
        }

        #endregion
    }
}
