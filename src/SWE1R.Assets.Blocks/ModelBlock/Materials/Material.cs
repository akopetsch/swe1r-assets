// SPDX-License-Identifier: MIT

using ByteSerialization;
using ByteSerialization.IO;

namespace SWE1R.Assets.Blocks.ModelBlock.Materials
{
    /// <summary>
    /// <para>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="https://github.com/tim-tim707/SW_RACER_RE/blob/d2d15c27d81e51e91996563795643c91439147aa/src/types.h#L1463">
    ///       github.com - tim-tim707/SW_RACER_RE - types.h - swrModel_Material</see></item>
    ///   <item>
    ///     <see href="https://github.com/Olganix/Sw_Racer/blob/76c8ad9cea549ea18457846a135a7f25d48b3813/include/Swr_Model.h#L368">
    ///       github.com - Olganix/Sw_Racer - Swr_Model.h - SWR_MODEL_Section6</see></item>
    /// </list>
    /// </para>
    /// </summary>
    public class Material : ICustomSerializable
    {
        #region Properties (serialization)

        /// <summary>
        /// <para>SW_RACER_RE: unk1</para>
        /// Always one of these values: 0, 1, 8, 9
        /// </summary>
        public int AlphaBpp { get; set; }
        /// <summary>
        /// <para>SW_RACER_RE: unk2</para>
        /// </summary>
        public short Word_4 { get; set; }
        /// <summary>
        /// <para>SW_RACER_RE: color_combine_mode_cycle1, alpha_combine_mode_cycle1</para>
        /// </summary>
        public int[] Ints_6 { get; set; }
        /// <summary>
        /// <para>SW_RACER_RE: color_combine_mode_cycle2, alpha_combine_mode_cycle2</para>
        /// </summary>
        public int[] Ints_e { get; set; }
        /// <summary>
        /// <para>SW_RACER_RE: unk5</para>
        /// Always 0, except in model 3001. Seems like float16.
        /// </summary>
        public short Unk_16 { get; set; } // TODO: win-demo
        /// <summary>
        /// <para>SW_RACER_RE: render_mode_1</para>
        /// </summary>
        public int Bitmask1 { get; set; }
        /// <summary>
        /// <para>SW_RACER_RE: render_mode_2</para>
        /// </summary>
        public int Bitmask2 { get; set; }
        /// <summary>
        /// <para>SW_RACER_RE: unk8</para>
        /// Always 0, except in model 3001. Seems like float16.
        /// </summary>
        public short Unk_20 { get; set; } // TODO: win-demo
        /// <summary>
        /// <para>SW_RACER_RE: primitive_color[0]</para>
        /// </summary>
        public byte Byte_22 { get; set; }
        /// <summary>
        /// <para>SW_RACER_RE: primitive_color[1]</para>
        /// </summary>
        public byte Byte_23 { get; set; }
        /// <summary>
        /// <para>SW_RACER_RE: primitive_color[2]</para>
        /// </summary>
        public byte Byte_24 { get; set; }
        /// <summary>
        /// <para>SW_RACER_RE: primitive_color[3]</para>
        /// </summary>
        public byte Byte_25 { get; set; }
        /// <summary>
        /// Always 0, except in model 3001. Seems like float16.
        /// </summary>
        public short Unk_26 { get; set; } // TODO: win-demo
        /// <summary>
        /// Always 0, except in model 3001. Seems like float16.
        /// </summary>
        public short Unk_28 { get; set; } // TODO: win-demo
        /// <summary>
        /// Always 0, except in model 3001. Seems like float16.
        /// </summary>
        public short Unk_2a { get; set; } // TODO: win-demo
        /// <summary>
        /// Always 0, except in model 3001. Seems like float16.
        /// </summary>
        public short Unk_2c { get; set; } // TODO: win-demo
        public byte Byte_2e { get; set; }
        public byte Byte_2f { get; set; }
        public byte Byte_30 { get; set; }
        public byte Byte_31 { get; set; }
        /// <summary>
        /// Always 0, except in model 3001. Seems like float16.
        /// </summary>
        public short Unk_32 { get; set; }

        #endregion

        #region Methods (helper)

        public int CombinedBitmask =>
            Bitmask1 | Bitmask2;

        public bool IsFlipped =>
            CombinedBitmask == 0xF0A2008; // TODO: confirm this

        #endregion

        #region Methods (: ICustomSerializable)

        public void Serialize(EndianBinaryWriter writer)
        {
            writer.Write(AlphaBpp);
            writer.Write(Word_4);

            writer.Write(Ints_6[0]);
            writer.Write(Ints_6[1]);

            writer.Write(Ints_e[0]);
            writer.Write(Ints_e[1]);

            writer.Write(Unk_16);

            writer.Write(Bitmask1);
            writer.Write(Bitmask2);

            writer.Write(Unk_20);

            writer.Write(Byte_22);
            writer.Write(Byte_23);
            writer.Write(Byte_24);
            writer.Write(Byte_25);

            writer.Write(Unk_26);
            writer.Write(Unk_28);
            writer.Write(Unk_2a);
            writer.Write(Unk_2c);

            writer.Write(Byte_2e);
            writer.Write(Byte_2f);
            writer.Write(Byte_30);
            writer.Write(Byte_31);

            writer.Write(Unk_32);
        }

        public void Deserialize(EndianBinaryReader reader)
        {
            AlphaBpp = reader.ReadInt32();
            Word_4 = reader.ReadInt16();

            Ints_6 = new int[] {
                reader.ReadInt32(),
                reader.ReadInt32(),
            };

            Ints_e = new int[] {
                reader.ReadInt32(),
                reader.ReadInt32(),
            };

            Unk_16 = reader.ReadInt16();

            Bitmask1 = reader.ReadInt32();
            Bitmask2 = reader.ReadInt32();

            Unk_20 = reader.ReadInt16();

            Byte_22 = reader.ReadByte();
            Byte_23 = reader.ReadByte();
            Byte_24 = reader.ReadByte();
            Byte_25 = reader.ReadByte();

            Unk_26 = reader.ReadInt16();
            Unk_28 = reader.ReadInt16();
            Unk_2a = reader.ReadInt16();
            Unk_2c = reader.ReadInt16();

            Byte_2e = reader.ReadByte();
            Byte_2f = reader.ReadByte();
            Byte_30 = reader.ReadByte();
            Byte_31 = reader.ReadByte();

            Unk_32 = reader.ReadInt16();
        }

        #endregion
    }
}
