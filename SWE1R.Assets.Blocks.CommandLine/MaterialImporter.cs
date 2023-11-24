// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Common.Images;
using SWE1R.Assets.Blocks.Common.Vectors;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.TextureBlock;
using System.Numerics;
using Material = SWE1R.Assets.Blocks.ModelBlock.Meshes.Material;

namespace SWE1R.Assets.Blocks.CommandLine
{
    public abstract class MaterialImporter
    {
        #region Properties

        public ImageRgba32 Image { get; }
        public Block<Texture> TextureBlock { get; }

        public Material Material { get; private set; }
        public Texture Texture { get; private set; }

        #endregion

        #region Constructor

        public MaterialImporter(ImageRgba32 image, Block<Texture> textureBlock)
        {
            Image = image;
            TextureBlock = textureBlock;
        }

        #endregion

        #region Methods

        public void Import()
        {
            Texture = CreateTexture();
            Texture.Block = TextureBlock;
            TextureBlock.Add(Texture);

            Material = CreateMaterial();
        }

        protected abstract Texture CreateTexture();

        protected virtual Material CreateMaterial() =>
            new Material() {
                Texture = CreateMaterialTexture(),
                Properties = CreateMaterialProperties(),
            };

        protected virtual MaterialTexture CreateMaterialTexture()
        {
            var mt = new MaterialTexture();
            (mt.Width4, mt.Height4) = GetSize4();
            (mt.Width, mt.Height) = GetSize();
            (mt.Width_Unk, mt.Height_Unk) = GetSizeUnk();
            mt.Children[0] = CreateMaterialTextureChild();
            mt.TextureIndex = Texture.Index;
            return mt;
        }

        protected abstract MaterialTextureChild CreateMaterialTextureChild();

        protected abstract MaterialProperties CreateMaterialProperties();

        private (short width, short height) GetSize()
        {
            Vector2 size = Image.Size;
            Vector2 max = MaterialTexture.MaxSize;
            if (!max.Contains(size))
                throw new MaterialImporterException($"{size} exceeds {max}");
            return ((short)size.X, (short)size.Y);
        }

        private (short width4, short height4) GetSize4()
        {
            Vector2 size = Image.Size * 4;
            Vector2 max = MaterialTexture.MaxSize4;
            if (!max.Contains(size))
                throw new MaterialImporterException($"{size} exceeds {max}");
            return ((short)size.X, (short)size.Y);
        }

        private (ushort width_Unk, ushort height_Unk) GetSizeUnk()
        {
            Vector2 result = Image.Size * 512;
            result = result.ScaleWithinBounds(ushort.MaxValue, ushort.MaxValue);
            return ((ushort)result.X, (ushort)result.Y);
        }

        #endregion
    }
}
