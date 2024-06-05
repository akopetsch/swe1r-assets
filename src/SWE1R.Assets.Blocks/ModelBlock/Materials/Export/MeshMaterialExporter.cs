// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.Images;
using SWE1R.Assets.Blocks.TextureBlock;
using System.Linq;

namespace SWE1R.Assets.Blocks.ModelBlock.Materials.Export
{
    public class MeshMaterialExporter
    {
        #region Properties (input)

        public MeshMaterial MeshMaterial { get; set; }
        public Block<TextureBlockItem> TextureBlock { get; set; }

        #endregion

        #region Properties (output)

        public ImageRgba32 EffectiveImage {  get; set; }

        #endregion

        #region Constructor

        public MeshMaterialExporter(
            MeshMaterial meshMaterial, 
            Block<TextureBlockItem> textureBlockItem)
        {
            MeshMaterial = meshMaterial;
            TextureBlock = textureBlockItem;
        }

        #endregion

        #region Methods

        public void Export()
        {
            if (MeshMaterial.Texture != null)
            {
                if (MeshMaterial.Texture.TextureIndex != -1)
                {
                    MaterialTextureChild firstMaterialTextureChild = MeshMaterial.Texture.Children.FirstOrDefault(); // HACK: use first child as default
                    var texturerExporter = new MaterialTextureExporter(MeshMaterial.Texture, firstMaterialTextureChild, TextureBlock);
                    texturerExporter.Export();
                    EffectiveImage = texturerExporter.EffectiveImage;
                    if (MeshMaterial.Material.AlphaBpp == 0)
                        EffectiveImage.SetAlpha(byte.MaxValue);
                }
            }
        }

        #endregion
    }
}
