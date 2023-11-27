// Copyright 2023 SWE1R.Assets Maintainers
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
            Image = new ImageRgba32(
                MaterialTexture.Width,
                MaterialTexture.Height);

            // get texture
            int textureIndex = MaterialTexture.TextureIndex;
            if (textureIndex == -1)
                return;
            TextureBlockItem textureBlockItem = TextureBlock[textureIndex];
            textureBlockItem.Load();

            // get pixels
            var textureExporter = new TextureExporter(
                textureBlockItem.PixelsPart.Bytes,
                MaterialTexture.TextureFormat,
                MaterialTexture.Width,
                MaterialTexture.Height,
                textureBlockItem.PaletteColors);
            textureExporter.Export();

            if (MaterialTextureChild != null)
                EffectiveImage = Image.Mirror(
                    MaterialTextureChild.HasDoubleWidth,
                    MaterialTextureChild.HasDoubleHeight);
        }

        #endregion
    }
}
