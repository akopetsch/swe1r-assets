// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Images;
using SWE1R.Assets.Blocks.TextureBlock;
using System.Linq;

namespace SWE1R.Assets.Blocks.ModelBlock.Materials.Export
{
    public class MaterialExporter
    {
        #region Properties (input)

        public Material Material { get; set; }
        public Block<TextureBlockItem> TextureBlock { get; set; }

        #endregion

        #region Properties (output)

        public ImageRgba32 EffectiveImage {  get; set; }

        #endregion

        #region Constructor

        public MaterialExporter(
            Material material, 
            Block<TextureBlockItem> textureBlockItem)
        {
            Material = material;
            TextureBlock = textureBlockItem;
        }

        #endregion

        #region Methods

        public void Export()
        {
            if (Material.Texture != null)
            {
                if (Material.Texture.TextureIndex != -1)
                {
                    MaterialTextureChild firstMaterialTextureChild = Material.Texture.Children.FirstOrDefault(); // HACK: use first child as default
                    var texturerExporter = new MaterialTextureExporter(Material.Texture, firstMaterialTextureChild, TextureBlock);
                    texturerExporter.Export();
                    EffectiveImage = texturerExporter.EffectiveImage;
                    if (Material.Properties.AlphaBpp == 0)
                        EffectiveImage.SetAlpha(byte.MaxValue);
                }
            }
        }

        #endregion
    }
}
