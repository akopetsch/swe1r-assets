// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;
using SWE1R.Assets.Blocks.Textures;

namespace SWE1R.Assets.Blocks.ModelBlock.Materials
{
    /// <summary>
    /// <para>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="https://github.com/tim-tim707/SW_RACER_RE/blob/d2d15c27d81e51e91996563795643c91439147aa/src/types.h#L1427">
    ///       github.com - tim-tim707/SW_RACER_RE - types.h - swrModel_MaterialTexture</see></item>
    ///   <item>
    ///     <see href="https://github.com/Olganix/Sw_Racer/blob/76c8ad9cea549ea18457846a135a7f25d48b3813/include/Swr_Model.h#L241">
    ///       github.com - Olganix/Sw_Racer - Swr_Model.h - SWR_MODEL_Section5</see></item>
    /// </list>
    /// </para>
    /// </summary>
    [Sizeof(0x40)]
    public class MaterialTexture
    {
        #region Constants

        public const int ChildrenCount = 6;

        #endregion

        #region Properties (serialized)

        /// <summary>
        /// <para>SW_RACER_RE: unk0</para>
        /// Always 0, 1, 0x41 (0100 0001) or 0x49 (0100 1001). 
        /// Strong positive correllation with <see cref="Word_0e">Word_0e</see>.
        /// Strong positive correlation with the number of elements in <see cref="Children">Children</see> that are not null.
        /// </summary>
        [Order(0)]
        public int Mask_Unk { get; set; }
        /// <summary>
        /// <para>SW_RACER_RE: res[0]</para>
        /// Always four times <see cref="Width">Width</see>.
        /// </summary>
        [Order(1)]
        public short Width4 { get; set; }
        /// <summary>
        /// <para>SW_RACER_RE: res[1]</para>
        /// Always four times <see cref="Height">Height</see>.
        /// </summary>
        [Order(2)]
        public short Height4 { get; set; }
        /// <summary>
        /// <para>SW_RACER_RE: unk1[0]</para>
        /// Always zero.
        /// </summary>
        [Order(3)]
        public short Always0_08 { get; set; } = 0;
        /// <summary>
        /// <para>SW_RACER_RE: unk1[1]</para>
        /// Always zero.
        /// </summary>
        [Order(4)]
        public short Always0_0a { get; set; } = 0;
        /// <summary>
        /// <para>SW_RACER_RE: type</para>
        /// </summary>
        [Order(5)]
        public TextureFormat Format { get; set; }
        /// <summary>
        /// <para>SW_RACER_RE: num_children</para>
        /// Most of the time 0 (6383 times). Sometimes 4 (28 times). 
        /// Strong positive correlation with <see cref="Mask_Unk">Mask_Unk</see>. 
        /// Very strong positive correlation with the number of elements in <see cref="Children">Children</see> that are not null.
        /// </summary>
        [Order(6)]
        public short Word_0e { get; set; }
        /// <summary>
        /// <para>SW_RACER_RE: width</para>
        /// </summary>
        [Order(7)]
        public short Width { get; set; }
        /// <summary>
        /// <para>SW_RACER_RE: height</para>
        /// </summary>
        [Order(8)]
        public short Height { get; set; }
        /// <summary>
        /// <para>SW_RACER_RE: unk2</para>
        /// Always 128, 256 or 512 times <see cref="Width">Width</see>.
        /// </summary>
        [Order(9)]
        public ushort Width_Unk { get; set; }
        /// <summary>
        /// <para>SW_RACER_RE: unk3</para>
        /// Always 128, 256 or 512 times <see cref="Height">Height</see>.
        /// </summary>
        [Order(10)]
        public ushort Height_Unk { get; set; }
        /// <summary>
        /// <para>SW_RACER_RE: unk4</para>
        /// Original values are in <see cref="OriginalFlagsValues">OriginalFlagsValues</see>.
        /// </summary>
        [Order(11)]
        public short Flags { get; set; }
        /// <summary>
        /// <para>SW_RACER_RE: unk5</para>
        /// Strong negative correlation with 
        /// <see cref="Mask_Unk">Mask_Unk</see> and 
        /// <see cref="Word_0e">Word_0e</see>. 
        /// Significant negative correlation with the number of elements in <see cref="Children">Children</see> that are not null.
        /// Original values are in <see cref="OriginalMaskValues">OriginalMaskValues</see>.
        /// </summary>
        [Order(12)]
        public short Mask { get; set; }
        /// <summary>
        /// <para>SW_RACER_RE: specs</para>
        /// </summary>
        [Order(13), Length(ChildrenCount), ElementReference]
        public MaterialTextureChild[] Children { get; set; } = new MaterialTextureChild[ChildrenCount];
        /// <summary>
        /// <para>SW_RACER_RE: texture_index</para>
        /// </summary>
        [Order(14), Offset(0x38)]
        public TextureIndex TextureIndex { get; set; }

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

        #region Methods (: object)

        public override string ToString() =>
            $"({nameof(Width)}={Width}, " +
            $"{nameof(Height)}={Height}, " +
            $"{nameof(TextureIndex)}={TextureIndex})";

        #endregion
    }
}
