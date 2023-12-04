// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Images;
using SWE1R.Assets.Blocks.TextureBlock;
using SWE1R.Assets.Blocks.Textures;
using SWE1R.Assets.Blocks.Textures.Import;
using SWE1R.Assets.Blocks.Vectors;
using System;
using System.Numerics;

namespace SWE1R.Assets.Blocks.ModelBlock.Materials.Import
{
    public abstract class MaterialImporter
    {
        #region Properties (input)

        public ImageRgba32 Image { get; }
        public TextureFormat TextureFormat { get; set; }
        public Block<TextureBlockItem> TextureBlock { get; }

        #endregion

        #region Properties (output)

        public Material Material { get; private set; }
        public TextureBlockItem TextureBlockItem { get; private set; }

        #endregion

        #region Constructor

        public MaterialImporter(ImageRgba32 image, TextureFormat textureFormat, Block<TextureBlockItem> textureBlock)
        {
            Image = image;
            TextureFormat = textureFormat;
            TextureBlock = textureBlock;
        }

        #endregion

        #region Methods

        public void Import()
        {
            TextureBlockItem = CreateTextureBlockItem();
            Material = CreateMaterial();
        }

        private TextureBlockItem CreateTextureBlockItem()
        {
            var blockItem = new TextureBlockItem();
            var importer = new TextureImporterFactory().Get(Image, TextureFormat);
            importer.Import();
            blockItem.PixelsPart.Bytes = importer.PixelsBytes;
            blockItem.PalettePart.Bytes = importer.PaletteBytes ?? new byte[] { };
            blockItem.Block = TextureBlock;
            TextureBlock.Add(blockItem);
            return blockItem;
        }

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

        protected virtual MaterialProperties CreateMaterialProperties() =>
            new MaterialProperties() {
                // bitmasks to make it opaque:
                Bitmask1 = unchecked((int)0xC8000000),
                Bitmask2 = 0x0112038,
                // Bitmask2 = 0x00112078
            };

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
