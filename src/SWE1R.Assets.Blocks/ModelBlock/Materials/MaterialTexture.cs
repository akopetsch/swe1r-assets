// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.Textures;

namespace SWE1R.Assets.Blocks.ModelBlock.Materials
{
    /// <summary>
    /// <see href="https://github.com/akopetsch/Sw_Racer/blob/76c8ad9cea549ea18457846a135a7f25d48b3813/include/Swr_Model.h#L241">SWR_MODEL_Section5</see>
    /// </summary>
    [Sizeof(0x40)]
    public class MaterialTexture
    {
        #region Constants

        public const int ChildrenCount = 6;

        #endregion

        #region Properties (serialized)

        /// <summary>
        /// Always 0, 1, 0x41 (0100 0001) or 0x49 (0100 1001). 
        /// Strong positive correllation with <see cref="Word_0e">Word_0e</see>.
        /// Strong positive correlation with the number of elements in <see cref="Children">Children</see> 
        /// that are not null.
        /// </summary>
        [Order(0)] public int Mask_Unk { get; set; }
        /// <summary>
        /// Always four times <see cref="Width">Width</see>.
        /// </summary>
        [Order(1)] public short Width4 { get; set; }
        /// <summary>
        /// Always four times <see cref="Height">Height</see>.
        /// </summary>
        [Order(2)] public short Height4 { get; set; }
        /// <summary>
        /// Always zero.
        /// </summary>
        [Order(3)] public short Always0_08 { get; set; } = 0;
        /// <summary>
        /// Always zero.
        /// </summary>
        [Order(4)] public short Always0_0a { get; set; } = 0;
        [Order(5)] public TextureFormat Format { get; set; }
        /// <summary>
        /// Most of the time 0 (6383 times). Sometimes 4 (28 times). 
        /// Strong positive correlation with <see cref="Mask_Unk">Mask_Unk</see>. 
        /// Very strong positive correlation with the number of elements in <see cref="Children">Children</see> that are not null.
        /// </summary>
        [Order(6)] public short Word_0e { get; set; }
        [Order(7)] public short Width { get; set; }
        [Order(8)] public short Height { get; set; }
        /// <summary>
        /// Always 128, 256 or 512 times <see cref="Width">Width</see>.
        /// </summary>
        [Order(9)] public ushort Width_Unk { get; set; }
        /// <summary>
        /// Always 128, 256 or 512 times <see cref="Height">Height</see>.
        /// </summary>
        [Order(10)] public ushort Height_Unk { get; set; }
        /// <summary>
        /// Original values are in <see cref="OriginalFlagsValues">OriginalFlagsValues</see>.
        /// </summary>
        [Order(11)] public short Flags { get; set; }
        /// <summary>
        /// Strong negative correlation with 
        /// <see cref="Mask_Unk">Mask_Unk</see> and 
        /// <see cref="Word_0e">Word_0e</see>.
        /// Significant negative correlation with the number of elements in <see cref="Children">Children</see> that are not null.
        /// Original values are in <see cref="OriginalMaskValues">OriginalMaskValues</see>.
        /// </summary>
        [Order(12)] public short Mask { get; set; }

        [Length(ChildrenCount)]
        [ElementReference]
        [Order(13)] public MaterialTextureChild[] Children { get; set; }

        [Offset(0x38)]
        [Order(14)] public TextureIndex TextureIndex { get; set; }

        #endregion

        #region Properties (original values)

        /// <summary>
        /// The original values of <see cref="Flags">Flags</see>.
        /// </summary>
        public static readonly short[] OriginalFlagsValues = new short[] {
            0,    // 28 times
            64,   // 29 times
            128,  // 270 times
            256,  // 289 times
            512,  // 3722 times
            683,  // 3 times
            1024, // 1633 times
            2048, // 437 times
        };

        /// <summary>
        /// The original values of <see cref="Mask">Mask</see>.
        /// </summary>
        public static readonly short[] OriginalMaskValues = new short[] {
            0x0007, // 1 time
            0x000f, // 10 time (does not predict special 10 textures)
            0x001f, // 6 times
            0x003f, // 61 times
            0x007f, // 192 times
            0x00ff, // 699 times
            0x016f, // 3 times
            0x01f7, // 3 times
            0x01ff, // 1905 times
            0x02aa, // 3 times
            0x0365, // 2 times
            0x03ff, // 3422 times
            0x07ff, // 76 times
            unchecked((short)0xd78f), // 28 times
        };

        #endregion

        #region Constructor

        public MaterialTexture() =>
            Children = new MaterialTextureChild[ChildrenCount];

        #endregion

        #region Methods (: object)

        public override string ToString() =>
            $"({nameof(Width)}={Width}, {nameof(Height)}={Height}, {nameof(TextureIndex)}={TextureIndex})";

        #endregion
    }
}
