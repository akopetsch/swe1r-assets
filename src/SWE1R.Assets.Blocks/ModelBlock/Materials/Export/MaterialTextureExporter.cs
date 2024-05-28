// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Images;
using SWE1R.Assets.Blocks.TextureBlock;
using SWE1R.Assets.Blocks.Textures.Export;

namespace SWE1R.Assets.Blocks.ModelBlock.Materials.Export
{
    public class MaterialTextureExporter
    {
        #region Properties (input)

        public MaterialTexture MaterialTexture { get; set; }
        public MaterialTextureChild MaterialTextureChild { get; set; }
        public Block<TextureBlockItem> TextureBlock { get; set; }

        #endregion

        #region Properties (output)

        public ImageRgba32 Image { get; set; }
        public ImageRgba32 EffectiveImage { get; set; }

        #endregion

        #region Constructor

        public MaterialTextureExporter(
            MaterialTexture materialTexture,
            MaterialTextureChild child,
            Block<TextureBlockItem> textureBlock)
        {
            MaterialTexture = materialTexture;
            MaterialTextureChild = child;
            TextureBlock = textureBlock;
        }

        #endregion

        #region Methods

        public void Export()
        {
            // get texture
            int textureIndex = MaterialTexture.TextureIndex.Value;
            if (textureIndex == -1)
                return;
            TextureBlockItem textureBlockItem = TextureBlock[textureIndex];
            textureBlockItem.Load();

            // image
            var textureExporter = new TextureExporter(
                textureBlockItem.PixelsPart.Bytes,
                MaterialTexture.Format,
                MaterialTexture.Width,
                MaterialTexture.Height,
                textureBlockItem.PaletteColors);
            textureExporter.Export();
            Image = textureExporter.Image;

            // effective image
            EffectiveImage = Image;
            if (MaterialTextureChild != null)
                EffectiveImage = Image.Mirror(
                    MaterialTextureChild.HasDoubleWidth,
                    MaterialTextureChild.HasDoubleHeight);
        }

        #endregion
    }
}
