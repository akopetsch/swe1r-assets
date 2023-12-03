// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Unity.Objects;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Swe1rMaterialTexture = SWE1R.Assets.Blocks.ModelBlock.Materials.MaterialTexture;
using Swe1rTextureFormat = SWE1R.Assets.Blocks.Textures.TextureFormat;

namespace SWE1R.Assets.Blocks.Unity.ScriptableObjects
{
    public class MaterialTextureScriptableObject : AbstractScriptableObject<Swe1rMaterialTexture>
    {
        public int mask_Unk;
        public short width4;
        public short height4;
        public short always0_08;
        public short always0_0a;
        public Swe1rTextureFormat textureFormat;
        public byte byte_0c;
        public byte byte_0d;
        public short word_0e;
        public short width;
        public short height;
        public ushort width_unk;
        public ushort height_unk;
        public short flags;
        public short mask;
        [SerializeReference] public MaterialTextureChildObject[] children;
        public int textureIndex;

        public override void Import(Swe1rMaterialTexture source, ModelImporter importer)
        {
            mask_Unk = source.Mask_Unk;
            width4 = source.Width4;
            height4 = source.Height4;
            always0_08 = source.Always0_08;
            always0_0a = source.Always0_0a;
            textureFormat = source.Format;
            word_0e = source.Word_0e;
            width = source.Width;
            height = source.Height;
            width_unk = source.Width_Unk;
            height_unk = source.Height_Unk;
            flags = source.Flags;
            mask = source.Mask;
            children = source.Children.Select(x => x == null ? null : importer.GetMaterialTextureChildObject(x)).ToArray();
            textureIndex = source.TextureIndex.Value;
        }

        public override Swe1rMaterialTexture Export(ModelExporter exporter)
        {
            var result = new Swe1rMaterialTexture();
            result.Mask_Unk = mask_Unk;
            result.Width4 = width4;
            result.Height4 = height4;
            result.Always0_08 = always0_08;
            result.Always0_0a = always0_0a;
            result.Format = textureFormat;
            result.Word_0e = word_0e;
            result.Width = width;
            result.Height = height;
            result.Width_Unk = width_unk;
            result.Height_Unk = height_unk;
            result.Flags = flags;
            result.Mask = mask;
            result.Children = children.Select(x => x == null ? null : exporter.GetMaterialTextureChild(x)).ToArray();
            result.TextureIndex = textureIndex;
            return result;
        }
    }
}
