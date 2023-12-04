// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;

namespace SWE1R.Assets.Blocks.ModelBlock.Materials
{
    /// <summary>
    /// <see href="https://github.com/akopetsch/Sw_Racer/blob/76c8ad9cea549ea18457846a135a7f25d48b3813/include/Swr_Model.h#L230">SWR_MODEL_Section4</see>
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
    }
}
