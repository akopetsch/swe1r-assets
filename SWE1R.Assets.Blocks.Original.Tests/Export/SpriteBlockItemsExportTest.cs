// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SixLabors.ImageSharp;
using SWE1R.Assets.Blocks.Images.ImageSharp;
using SWE1R.Assets.Blocks.Original.MaterialTexturesCatalog;
using SWE1R.Assets.Blocks.SpriteBlock;
using SWE1R.Assets.Blocks.SpriteBlock.Export;

namespace SWE1R.Assets.Blocks.Original.Tests.Export
{
    public partial class SpriteBlockItemsExportTest :
        IClassFixture<OriginalMaterialTexturesCatalogProviderFixture>,
        IClassFixture<OriginalBlocksProviderFixture>
    {
        #region Properties

        public OriginalBlocksProviderFixture OriginalBlocksProviderFixture { get; }

        #endregion

        #region Constructor

        public SpriteBlockItemsExportTest(
            OriginalBlocksProviderFixture originalBlocksProviderFixture)
        {
            OriginalBlocksProviderFixture = originalBlocksProviderFixture;
        }

        #endregion

        #region Methods

        protected void Test(int valueId)
        {
            // get sprite
            SpriteBlockItem spriteBlockItem = OriginalBlocksProviderFixture.Provider
                .GetFirstBlockItemByValueId<SpriteBlockItem>(valueId);
            spriteBlockItem.Load();

            // export sprite
            var exporter = new SpriteExporter(spriteBlockItem.Sprite);
            exporter.Export();

            // save as png
            exporter.Image.ToImageSharp().SaveAsPng(GetPngPath(valueId));
        }

        protected string GetPngPath(int valueId)
        {
            string pngDirectory = Path.Combine(GetType().Name);
            Directory.CreateDirectory(pngDirectory);
            return Path.Combine(pngDirectory, $"{valueId:d5}.png");
        }

        #endregion
    }
}
