// SPDX-License-Identifier: GPL-2.0-only

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

        public SpriteBlockItem SpriteBlockItem { get; private set; }

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
            SpriteBlockItem = new SpriteBlockItem();
            SpriteBlockItem.Sprite = new Sprite() {
                Width = Convert.ToInt16(Image.Width),
                Height = Convert.ToInt16(Image.Height),
                Format = TextureFormat.RGBA5551_I8,
                Palette = ImportPalette(),
            };
            SpriteBlockItem.Sprite.Tiles = ImportTiles();
            SpriteBlockItem.Sprite.UpdateTilesCount();
            SpriteBlockItem.Save();
        }

        private SpritePalette ImportPalette()
        {
            var paletteImporter = new RGBA5551_PaletteImporter(Image.Palette);
            paletteImporter.Import();
            var palette = new SpritePalette() {
                Colors = paletteImporter.OutputPalette,
            };
            return palette;
        }

        private SpriteTile[] ImportTiles()
        {
            var sprite = SpriteBlockItem.Sprite;
            int tilesCount =
                sprite.TilesGridWidth *
                sprite.TilesGridHeight;
            var tiles = new SpriteTile[tilesCount];
            for (int tileY = 0; tileY < sprite.TilesGridHeight; tileY++)
            {
                for (int tileX = 0; tileX < sprite.TilesGridWidth; tileX++)
                {
                    (int spriteX, int spriteY) = sprite.GetTilePosition(tileX, tileY);
                    ImageRgba32 tileImage = Image.Crop(
                        spriteX, spriteY, SpriteTile.MaxWidth, SpriteTile.MaxHeight).FlipY();
                    var tile = new SpriteTile() {
                        Width = (short)tileImage.Width,
                        Height = (short)tileImage.Height,
                    };
                    var importer = new RGBA5551_I8_TextureImporter(tileImage, Image.Palette);
                    importer.Import();
                    tile.PixelsBytes = importer.PixelsBytes;
                    int tileIndex = sprite.GetTileIndex(tileX, tileY);
                    tiles[tileIndex] = tile;
                }
            }
            return tiles;
        }

        #endregion
    }
}
