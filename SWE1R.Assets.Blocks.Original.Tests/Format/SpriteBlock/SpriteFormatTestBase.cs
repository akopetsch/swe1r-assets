// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization;
using SixLabors.ImageSharp;
using SWE1R.Assets.Blocks.Images.ImageSharp;
using SWE1R.Assets.Blocks.SpriteBlock;
using SWE1R.Assets.Blocks.Textures;
using SWE1R.Assets.Blocks.Utils;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using Xunit.Abstractions;

namespace SWE1R.Assets.Blocks.Original.Tests.Format.SpriteBlock
{
    public class SpriteFormatTestBase : BlockItemsFormatTestBase<SpriteBlockItem>
    {
        #region Constructor

        public SpriteFormatTestBase(AnalyticsFixture analyticsFixture, ITestOutputHelper output, string blockIdName) :
            base(analyticsFixture, output, blockIdName)
        { }

        #endregion

        #region Methods (: BlockItemsTestBase)

        protected override void CompareItemInternal(int index)
        {
            SpriteBlockItem spriteBlockItem = DeserializeItem(index, out ByteSerializerContext context);
            Sprite sprite = spriteBlockItem.Sprite;

            Assert.Equal(0, sprite.Word_6);
            Assert.Equal(32, sprite.Word_E);

            // save sprite
            string folderPath = Path.Combine("dump", BlockDefaultFilenames.SpriteBlock, BlockItem.GetIndexString(index));
            Directory.CreateDirectory(folderPath);
            sprite.ExportImage().ToImageSharp()
                .SaveAsPng(Path.Combine(folderPath, "overall.png"));

            bool hasSpecialTiles = sprite.Tiles.Any(HasSpecialDimensions);

            List<SpriteTile> lastRow = sprite.GetTilesRow(sprite.TilesGridHeight - 1).ToList();
            List<short> lastRowHeights = lastRow.Select(t => t?.Height).Where(x => x.HasValue).Select(x => x.Value).Distinct().ToList();
            if (lastRowHeights.Count > 1)
                Debug.WriteLine($"{nameof(lastRowHeights)}: {string.Join(",", lastRowHeights)}");

            for (int tileY = 0; tileY < sprite.TilesGridHeight; tileY++)
            {
                for (int tileX = 0; tileX < sprite.TilesGridWidth; tileX++)
                {
                    SpriteTile tile = sprite.GetTile(tileX, tileY);
                    if (tile != null)
                    {
                        if (hasSpecialTiles)
                            Debug.WriteLine($"{tileX}-{tileY}: {tile.Width}x{tile.Height}");

                        // save tile
                        string tilePath = Path.Combine(folderPath, $"{tileX}-{tileY}.png");
                        tile.ExportImage(sprite).ToImageSharp()
                            .SaveAsPng(tilePath);

                        // Pixels.Length
                        int minimumLength = GetMinimumPixelsLength(sprite, tile);
                        int guessedLength = GetGuessedPixelsLength(sprite, tile);
                        int actualLength = tile.PixelsBytes.Length;
                        Assert.True(minimumLength % 2 == 0);
                    }
                    else
                    {
                        // TODO: ...
                    }
                }
            }
        }

        private int GetMinimumPixelsLength(Sprite sprite, SpriteTile tile)
        {
            int bpp = sprite.TextureFormat.GetBpp();
            int length = (tile.Width * tile.Height * bpp) / 8;
            return length;
        }

        private int GetGuessedPixelsLength(Sprite sprite, SpriteTile tile)
        {
            int length = GetMinimumPixelsLength(sprite, tile);
            return length;
        }

        private bool HasSpecialDimensions(SpriteTile tile) =>
            !MathUtil.IsPowerOfTwo(tile.Width) ||
            !MathUtil.IsPowerOfTwo(tile.Height);

        #endregion
    }
}
