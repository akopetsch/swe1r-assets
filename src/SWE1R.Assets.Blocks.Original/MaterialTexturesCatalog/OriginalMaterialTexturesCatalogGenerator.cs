// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using Newtonsoft.Json;
using SWE1R.Assets.Blocks.Metadata;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Materials;
using SWE1R.Assets.Blocks.TextureBlock;
using static SWE1R.Assets.Blocks.Original.MaterialTexturesCatalog.OriginalMaterialTexturesCatalog;

namespace SWE1R.Assets.Blocks.Original.MaterialTexturesCatalog
{
    public class OriginalMaterialTexturesCatalogGenerator
    {
        #region Fields

        private readonly MetadataProvider _metadataProvider = new();
        private readonly OriginalBlocksProvider _originalBlocksProvider = new();

        #endregion

        #region Methods

        public void Generate()
        {
            Console.WriteLine($"{nameof(OriginalMaterialTexturesCatalogGenerator)}.{nameof(Generate)}()");

            var result = new OriginalMaterialTexturesCatalog();

            _originalBlocksProvider.Load();

            var releases = _metadataProvider.Releases
                .DistinctBy(x => (x.ModelBlockId, x.TextureBlockId)).ToList();
            foreach (ReleaseMetadata release in releases)
            {
                Console.WriteLine(release.Name);

                BlockMetadata modelBlockMetadata = _metadataProvider.GetBlock<ModelBlockItem>(release);
                BlockMetadata textureBlockMetadata = _metadataProvider.GetBlock<TextureBlockItem>(release);

                Block<ModelBlockItem> modelBlock =
                    _originalBlocksProvider.GetBlock<ModelBlockItem>(modelBlockMetadata.Name);
                var blockItemsMetadata =
                    _metadataProvider.GetBlockItems(modelBlockMetadata).DistinctBy(x => x.ValueId).ToList();

                // load Material, MaterialTexture
                foreach (BlockItemMetadata blockItemMetadata in blockItemsMetadata)
                {
                    Console.WriteLine(
                        $"{nameof(BlockItemMetadata.Index)}={blockItemMetadata.Index}, " +
                        $"{nameof(BlockItemMetadata.ValueId)}={blockItemMetadata.ValueId}");
                    ModelBlockItem modelBlockItem = modelBlock[blockItemMetadata.Index];
                    modelBlockItem.Load();

                    // Material
                    foreach (Material material in modelBlockItem.Model.GetMaterials())
                    {
                        var materialByValueIds = new MaterialByValueIds() {
                            ReleaseMetadata = release,
                            ModelBlockMetadata = modelBlockMetadata,
                            TextureBlockMetadata = textureBlockMetadata,
                            Material = material,
                            ModelValueId = blockItemMetadata.ValueId,
                            TextureValueId = GetTextureValueId(material.Texture, textureBlockMetadata),
                        };
                        result.MaterialsByValueIds.Add(materialByValueIds);
                    }

                    // MaterialTexture
                    foreach (MaterialTexture materialTexture in modelBlockItem.Model.GetMaterialTextures())
                    {
                        var materialTextureByValueIds = new MaterialTextureByValueIds() {
                            ReleaseMetadata = release,
                            ModelBlockMetadata = modelBlockMetadata,
                            TextureBlockMetadata = textureBlockMetadata,
                            MaterialTexture = materialTexture,
                            ModelValueId = blockItemMetadata.ValueId,
                            TextureValueId = GetTextureValueId(materialTexture, textureBlockMetadata),
                        };
                        result.MaterialTexturesByValueIds.Add(materialTextureByValueIds);
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            // serialize JSON
            var settings = OriginalMaterialTexturesCatalog.JsonSerializerSettings;
            string json = JsonConvert.SerializeObject(result, settings);
            File.WriteAllText(JsonFilename, json);
        }

        private int? GetTextureValueId(MaterialTexture materialTexture, BlockMetadata textureBlockMetadata)
        {
            if (materialTexture == null)
                return null;
            else if (materialTexture.TextureIndex == -1)
                return -1;
            else
                return _metadataProvider.GetBlockItem(
                    BlockItemType.TextureBlockItem,
                    textureBlockMetadata.Id,
                    materialTexture.TextureIndex.Value)
                .ValueId;
        }

        #endregion
    }
}
