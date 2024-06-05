// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using SWE1R.Assets.Blocks.Images;
using SWE1R.Assets.Blocks.ModelBlock.Materials.Import;
using SWE1R.Assets.Blocks.TextureBlock;
using System.IO;

namespace SWE1R.Assets.Blocks.ModelBlock.Materials
{
    /// <summary>
    /// <para>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="https://github.com/akopetsch/SW_RACER_RE/blob/d2d15c27d81e51e91996563795643c91439147aa/src/types.h#L1414">
    ///       github.com - akopetsch/SW_RACER_RE - types.h - swrModel_MeshMaterial</see></item>
    ///   <item>
    ///     <see href="https://github.com/akopetsch/Sw_Racer/blob/76c8ad9cea549ea18457846a135a7f25d48b3813/include/Swr_Model.h#L230">
    ///       github.com - akopetsch/Sw_Racer - Swr_Model.h - SWR_MODEL_Section4</see></item>
    /// </list>
    /// </para>
    /// </summary>
    public class Material
    {
        #region Properties (serialized)

        [Order(0)]
        public int Bitmask { get; set; }
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

        #region Properties (original values)

        /// <summary>
        /// The original values of <see cref="Bitmask">Bitmask</see>.
        /// </summary>
        public static readonly short[] OriginalBitmaskValues = new short[] {
            0x04, // 276 times
            0x06, // 720 times
            0x07, // 523 times
            0x0c, // 741 times
            0x0e, // 5074 times
            0x0f, // 4684 times
            0x17, // 13 times
            0x1f, // 69 times
            0x46, // 26 times
            0x47, // 500 times
            0x57, // 4 times
        };

        #endregion

        #region Properties (helper)

        public bool HasBackfaceCulling => (Bitmask & 8) > 0; // TODO: confirm this

        #endregion

        #region Methods (import helper)

        public static MaterialImporter Import(
            Stream textureImageStream, 
            Block<TextureBlockItem> textureBlock, 
            LoadImageRgba32FromStreamDelegate loadImageRgba32FromStreamDelegate)
        {
            ImageRgba32 imageRgba32 = loadImageRgba32FromStreamDelegate(textureImageStream);
            MaterialImporter importer = new MaterialImporterFactory().Get(imageRgba32, textureBlock);
            importer.Import();
            return importer;
        }

        #endregion
    }
}
