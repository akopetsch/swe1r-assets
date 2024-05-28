// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Unity.Objects;
using UnityEngine;
using Swe1rMaterial = SWE1R.Assets.Blocks.ModelBlock.Materials.Material;

namespace SWE1R.Assets.Blocks.Unity.ScriptableObjects
{
    public class MaterialScriptableObject : AbstractScriptableObject<Swe1rMaterial>
    {
        public int integer;
        public short width_Unk_Dividend;
        public short height_Unk_Dividend;
        public MaterialTextureScriptableObject texture;
        [SerializeReference] public MaterialPropertiesObject properties;

        public override void Import(Swe1rMaterial source, ModelImporter importer)
        {
            integer = source.Int;
            width_Unk_Dividend = source.Width_Unk_Dividend;
            height_Unk_Dividend = source.Height_Unk_Dividend;
            if (source.Texture != null)
                texture = importer.GetMaterialTextureScriptableObject(source.Texture);
            properties = importer.GetMaterialPropertiesObject(source.Properties);
        }

        public override Swe1rMaterial Export(ModelExporter exporter)
        {
            var result = new Swe1rMaterial();
            result.Int = integer;
            result.Width_Unk_Dividend = width_Unk_Dividend;
            result.Height_Unk_Dividend = height_Unk_Dividend;
            if (texture != null)
                result.Texture = exporter.GetMaterialTexture(texture);
            result.Properties = exporter.GetMaterialProperties(properties);
            return result;
        }
    }
}
