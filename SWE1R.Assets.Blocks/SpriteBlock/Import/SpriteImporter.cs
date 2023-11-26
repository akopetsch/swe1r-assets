// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Images;
using SWE1R.Assets.Blocks.Textures;
using SWE1R.Assets.Blocks.Textures.Import;
using System;

namespace SWE1R.Assets.Blocks.SpriteBlock.Import
{
    public class SpriteImporter
    {
        #region Properties (input)

        public ImageRgba32 Image { get; }

        #endregion

        #region Properties (output)

        public Sprite Sprite { get; private set; }

        #endregion

        #region Constructor

        public SpriteImporter(ImageRgba32 image)
        {
            Image = image;
        }

        #endregion

        #region Methods

        public void Import()
        {
            Sprite = new Sprite() {
                Width = Convert.ToInt16(Image.Width),
                Height = Convert.ToInt16(Image.Height),
                TextureFormat = TextureFormat.RGBA5551_I8,
            };
            Sprite.Tiles = ImportTiles();
            Sprite.UpdateTilesCount();
        }

        private SpriteTile[] ImportTiles()
        {
            int tilesCount = 
                Sprite.TilesGridWidth * 
                Sprite.TilesGridHeight;
            var tiles = new SpriteTile[tilesCount];
            for (int tileY = 0; tileY < Sprite.TilesGridHeight; tileY++)
            {
                for (int tileX = 0; tileX < Sprite.TilesGridWidth; tileX++)
                {
                    (int spriteX, int spriteY) = Sprite.GetTilePosition(tileX, tileY);
                    ImageRgba32 tileImage = Image.Crop(
                        spriteX, spriteY, SpriteTile.MaxWidth, SpriteTile.MaxHeight);
                    var tile = new SpriteTile() {
                        Width = (short)tileImage.Width,
                        Height = (short)tileImage.Height,
                    };
                    var importer = new RGBA5551_I8_TextureImporter(tileImage, Image.Palette);
                    importer.Import();
                    tile.PixelsBytes = importer.PixelsBytes;
                }
            }
            return tiles;
        }

        #endregion
    }
}
