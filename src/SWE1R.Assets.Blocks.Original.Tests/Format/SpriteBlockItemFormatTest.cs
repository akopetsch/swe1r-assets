// SPDX-License-Identifier: MIT

using ByteSerialization;
using SixLabors.ImageSharp;
using SWE1R.Assets.Blocks.Images.ImageSharp;
using SWE1R.Assets.Blocks.SpriteBlock;
using SWE1R.Assets.Blocks.SpriteBlock.Export;
using SWE1R.Assets.Blocks.Textures;
using SWE1R.Assets.Blocks.Utils;
using System.Diagnostics;
using Xunit.Abstractions;

namespace SWE1R.Assets.Blocks.Original.Tests.Format
{
    public partial class SpriteBlockItemFormatTest : BlockItemsFormatTestBase<SpriteBlockItem>
    {
        #region Constructor

        public SpriteBlockItemFormatTest(
            AnalyticsFixture analyticsFixture,
            OriginalBlocksProviderFixture originalBlocksProviderFixture,
            ITestOutputHelper output) :
            base(
                analyticsFixture, 
                originalBlocksProviderFixture, 
                output)
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
            var spriteExporter = new SpriteExporter(sprite);
            spriteExporter.Export();
            spriteExporter.Image.ToImageSharp()
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
                        var tileExporter = new SpriteTileExporter(tile, sprite);
                        tileExporter.Export();
                        tileExporter.Image.ToImageSharp()
                            .SaveAsPng(tilePath);

                        // Pixels.Length
                        int minimumLength = GetMinimumPixelsLength(sprite, tile);
                        int guessedLength = GetGuessedPixelsLength(sprite, tile);
                        int actualLength = tile.PixelsBytes.Length;
                        //Assert.True(minimumLength % 2 == 0);
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
            int bpp = sprite.Format.GetBpp();
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
