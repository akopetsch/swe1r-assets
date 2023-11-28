// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using Newtonsoft.Json;
using SWE1R.Assets.Blocks.ModelBlock;
using SWE1R.Assets.Blocks.ModelBlock.Materials;
using System.Diagnostics;

namespace SWE1R.Assets.Blocks.Original.Tests.Export.TextureBlock.ModelBlockTexturesFixtures
{
    public class ModelBlockMaterialsAndMaterialTextures
    {
        #region Properties

        public Dictionary<int, List<MaterialAndModelIndex>> MaterialsByTextureIndex { get; set; } =
            new Dictionary<int, List<MaterialAndModelIndex>>();
        public List<MaterialAndModelIndex> MaterialsWithoutTexture { get; set; } = 
            new List<MaterialAndModelIndex>();
        public Dictionary<int, List<MaterialTextureAndModelIndex>> MaterialTexturesByTextureIndex { get; set; } =
            new Dictionary<int, List<MaterialTextureAndModelIndex>>();

        #endregion

        #region Classes

        public class MaterialAndModelIndex
        {
            public Material Material { get; set; }
            public int ModelIndex { get; set; }
        }

        public class MaterialTextureAndModelIndex
        {
            public MaterialTexture MaterialTexture { get; set; }
            public int ModelIndex { get; set; }
        }

        #endregion

        #region Methods

        public static ModelBlockMaterialsAndMaterialTextures LoadJson(string blockIdName)
        {
            string jsonPath = GetJsonPath(blockIdName);
            if (File.Exists(jsonPath))
                return JsonConvert.DeserializeObject<ModelBlockMaterialsAndMaterialTextures>(
                    File.ReadAllText(jsonPath));
            else
                return null;
        }

        private void SaveJson(string blockIdName)
        {
            string jsonPath = GetJsonPath(blockIdName);
            Directory.CreateDirectory(GetJsonFolderName());
            string json = JsonConvert.SerializeObject(this);
            File.WriteAllText(GetJsonPath(blockIdName), json);
        }

        private static string GetJsonFolderName() =>
            nameof(ModelBlockMaterialsAndMaterialTextures);

        private static string GetJsonPath(string blockIdName) =>
            Path.Combine(GetJsonFolderName(), $"{blockIdName}.json");

        public static ModelBlockMaterialsAndMaterialTextures Load(string blockIdName)
        {
            var result = new ModelBlockMaterialsAndMaterialTextures();

            Block<ModelBlockItem> modelBlock = new OriginalBlockProvider().LoadBlock<ModelBlockItem>(blockIdName);

            // load Material, MaterialTexture
            foreach (ModelBlockItem modelBlockItem in modelBlock)
            {
                Debug.WriteLine(modelBlockItem.Index);
                modelBlockItem.Load();

                // Material
                foreach (Material material in modelBlockItem.Model.GetMaterials())
                {
                    var materialAndModelIndex = new MaterialAndModelIndex() {
                        Material = material,
                        ModelIndex = modelBlockItem.Index.Value,
                    };
                    if (material.Texture != null)
                        result.GetMaterials(material.Texture.TextureIndex.Value).Add(materialAndModelIndex);
                    else
                        result.MaterialsWithoutTexture.Add(materialAndModelIndex);
                }

                // MaterialTexture
                foreach (MaterialTexture materialTexture in modelBlockItem.Model.GetMaterialTextures())
                {
                    var materialTextureAndModelIndex = new MaterialTextureAndModelIndex() {
                        MaterialTexture = materialTexture,
                        ModelIndex = modelBlockItem.Index.Value,
                    };
                    result.GetMaterialTextures(materialTexture.TextureIndex.Value).Add(materialTextureAndModelIndex);
                }
            }

            // save json
            result.SaveJson(blockIdName);

            return result;
        }

        public List<MaterialAndModelIndex> GetMaterials(int textureIndex)
        {
            if (MaterialsByTextureIndex.TryGetValue(textureIndex, out List<MaterialAndModelIndex> existingList))
                return existingList;
            else
                return MaterialsByTextureIndex[textureIndex] = new List<MaterialAndModelIndex>();
        }

        public List<MaterialTextureAndModelIndex> GetMaterialTextures(int textureIndex)
        {
            if (MaterialTexturesByTextureIndex.TryGetValue(textureIndex, out List<MaterialTextureAndModelIndex> existingList))
                return existingList;
            else
                return MaterialTexturesByTextureIndex[textureIndex] = new List<MaterialTextureAndModelIndex>();
        }

        #endregion
    }
}
