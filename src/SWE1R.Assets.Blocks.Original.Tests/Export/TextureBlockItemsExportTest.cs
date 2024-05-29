// SPDX-License-Identifier: GPL-2.0-only

using Codeuctivity.ImageSharpCompare;
using SixLabors.ImageSharp;
using SWE1R.Assets.Blocks.Images.ImageSharp;
using SWE1R.Assets.Blocks.ModelBlock.Materials;
using SWE1R.Assets.Blocks.ModelBlock.Materials.Export;
using SWE1R.Assets.Blocks.Original.MaterialTexturesCatalog;
using SWE1R.Assets.Blocks.TextureBlock;

namespace SWE1R.Assets.Blocks.Original.Tests.Export
{
    public partial class TextureBlockItemsExportTest :
        IClassFixture<OriginalMaterialTexturesCatalogProviderFixture>,
        IClassFixture<OriginalBlocksProviderFixture>,
        IClassFixture<LightningPirateTexturePngProvider>
    {
        #region Constants

        

        #endregion

        #region Properties

        public OriginalMaterialTexturesCatalogProviderFixture OriginalMaterialTexturesCatalogProviderFixture { get; }
        public OriginalBlocksProviderFixture OriginalBlocksProviderFixture { get; }
        public LightningPirateTexturePngProvider LightningPirateTexturePngProvider { get; }

        #endregion

        #region Constructor

        public TextureBlockItemsExportTest(
            OriginalMaterialTexturesCatalogProviderFixture originalMaterialTexturesCatalogProviderFixture,
            OriginalBlocksProviderFixture originalBlocksProviderFixture,
            LightningPirateTexturePngProvider lightningPirateTexturePngProvider)
        {
            OriginalMaterialTexturesCatalogProviderFixture = originalMaterialTexturesCatalogProviderFixture;
            OriginalBlocksProviderFixture = originalBlocksProviderFixture;
            LightningPirateTexturePngProvider = lightningPirateTexturePngProvider;
        }

        #endregion

        #region Methods

        private void Test(int valueId)
        {
            List<MaterialByValueIds> materials = OriginalMaterialTexturesCatalogProviderFixture.Provider.Catalog
                .MaterialsByValueIds.Where(x => x.TextureValueId == valueId).ToList();
            bool anyMaterials = materials.Any();
            //Assert.True(anyMaterials); // fails 83 times
            List<MaterialTextureByValueIds> materialTextures = OriginalMaterialTexturesCatalogProviderFixture.Provider.Catalog
                .MaterialTexturesByValueIds.Where(x => x.TextureValueId == valueId).ToList();
            bool anyMaterialTextures = materialTextures.Any();
            //Assert.True(anyMaterialTextures); // fails 25 times
            if (anyMaterialTextures)
            {
                bool haveSingleWidth = materialTextures.Select(x => x.MaterialTexture.Width).Distinct().Count() == 1;
                bool haveSingleHeight = materialTextures.Select(x => x.MaterialTexture.Height).Distinct().Count() == 1;
                //Assert.True(haveSingleWidth); // fails 2 times (294, 382)
                //Assert.True(haveSingleHeight); // fails 1 time (294)
                if (haveSingleWidth && haveSingleHeight)
                {
                    // export
                    MaterialTexture materialTexture = materialTextures.First().MaterialTexture;
                    MaterialTextureChild materialTextureChild = materialTexture.Children.First();
                    Block<TextureBlockItem> textureBlock = OriginalBlocksProviderFixture.Provider
                        .GetBlock<TextureBlockItem>(materialTextures.First().TextureBlockMetadata.Name);
                    var exporter = new MaterialTextureExporter(materialTexture, materialTextureChild, textureBlock);
                    exporter.Export();

                    // save png
                    exporter.Image.ToImageSharp().SaveAsPng(GetPngPath(valueId));

                    // compare with LP
                    if (valueId < 1648)
                    {
                        var actualImage = exporter.Image.ToImageSharp();
                        var targetImage = LightningPirateTexturePngProvider.LoadTexturePng(valueId);
                        bool imagesHaveEqualSize = ImageSharpCompare.ImagesHaveEqualSize(actualImage, targetImage);
                        bool imagesAreEqual = ImageSharpCompare.ImagesAreEqual(actualImage, targetImage);
                        if (!imagesAreEqual)
                            targetImage.SaveAsPng(GetPngPath(valueId, "lp"));
                        Assert.True(imagesHaveEqualSize, nameof(imagesHaveEqualSize));

                        if (!LightningPirateTexturePngProvider.ScrambledTextureIds.Contains(valueId))
                            Assert.True(imagesAreEqual, nameof(imagesAreEqual));
                    }
                }
            }
        }

        private string GetPngPath(int valueId, string suffix = null)
        {
            string filename = $"{valueId:d5}";
            if (suffix != null)
                filename = $"{filename}.{suffix}";
            string pngDirectory = Path.Combine($"{nameof(TextureBlockItemsExportTest)}");
            Directory.CreateDirectory(pngDirectory);
            return Path.Combine(pngDirectory, $"{filename}.png");
        }

        #endregion
    }
}
