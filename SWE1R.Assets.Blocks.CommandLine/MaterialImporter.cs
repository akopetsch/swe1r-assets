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
        #region Properties (input)

        public ImageRgba32 Image { get; }
        public Block<TextureBlockItem> TextureBlock { get; }

        #endregion

        #region Properties (output)

        public Material Material { get; private set; }
        public TextureBlockItem TextureBlockItem { get; private set; }

        #endregion

        #region Constructor

        public MaterialImporter(ImageRgba32 image, Block<TextureBlockItem> textureBlock)
        {
            Image = image;
            TextureBlock = textureBlock;
        }

        #endregion

        #region Methods

        public void Import()
        {
            TextureBlockItem = CreateTextureBlockItem();
            TextureBlockItem.Block = TextureBlock;
            TextureBlock.Add(TextureBlockItem);

            Material = CreateMaterial();
        }

        protected abstract TextureBlockItem CreateTextureBlockItem();

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
            mt.TextureIndex = TextureBlockItem.Index;
            return mt;
        }

        protected abstract MaterialTextureChild CreateMaterialTextureChild();

        protected abstract MaterialProperties CreateMaterialProperties();

        private (short width, short height) GetSize() =>
            (Convert.ToInt16(Image.Width), 
            Convert.ToInt16(Image.Height));

        private (short width4, short height4) GetSize4() =>
            (Convert.ToInt16(Image.Width * 4), 
            Convert.ToInt16(Image.Height * 4));

        private (ushort width_Unk, ushort height_Unk) GetSizeUnk()
        {
            Vector2 result = Image.Size * 512;
            result = result.ScaleWithinBounds(ushort.MaxValue, ushort.MaxValue);
            return ((ushort)result.X, (ushort)result.Y);
        }

        #endregion
    }
}
