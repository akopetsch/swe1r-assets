// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using SWE1R.Assets.Blocks.Common.Images;
using SWE1R.Assets.Blocks.TextureBlock;
using System.Linq;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes
{
    /// <summary>
    /// <see href="https://github.com/akopetsch/Sw_Racer/blob/76c8ad9cea549ea18457846a135a7f25d48b3813/include/Swr_Model.h#L230">SWR_MODEL_Section4</see>
    /// </summary>
    public class Material
    {
        #region Properties (serialization)

        [Order(0)]
        public int Int { get; set; }
        [Order(1)]
        public short Width_Unk_Dividend { get; set; }
        [Order(2)]
        public short Height_Unk_Dividend { get; set; }
        /// <summary>
        /// Sometimes null.
        /// </summary>
        [Order(3), Reference]
        public MaterialTexture Texture { get; set; }
        /// <summary>
        /// Always has a value.
        /// </summary>
        [Order(4), Reference]
        public MaterialProperties Properties { get; set; }

        #endregion

        #region Properties (export)

        public bool HasBackfaceCulling => (Int & 8) > 0; // TODO: confirm this

        #endregion

        #region Methods (export)

        public ImageRgba32 Hack_ExportEffectiveImage(Block<Texture> textureBlock) // HACK: ExportEffectiveImage
        {
            if (Texture != null)
            {
                if (Texture.IdField.Id != -1)
                {
                    MaterialTextureChild firstMaterialTextureChild = Texture.Children.FirstOrDefault();
                    ImageRgba32 result = Texture.ExportEffectiveImage(textureBlock, firstMaterialTextureChild);
                    if (Properties.AlphaBpp == 0)
                        result.SetAlpha(byte.MaxValue);
                    return result;
                }
            }
            return null;
        }

        #endregion
    }
}
