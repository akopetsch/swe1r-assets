// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Components.Values.Customs;
using ByteSerialization.IO;

namespace SWE1R.Assets.Blocks.ModelBlock.Materials
{
    /// <summary>
    /// <see href="https://github.com/akopetsch/Sw_Racer/blob/76c8ad9cea549ea18457846a135a7f25d48b3813/include/Swr_Model.h#L368">SWR_MODEL_Section6</see>
    /// </summary>
    public class MaterialProperties : ICustomSerializable
    {
        #region Properties (serialization)

        /// <summary>
        /// Always one of these values: 0, 1, 8, 9
        /// </summary>
        public int AlphaBpp { get; set; }
        public short Word_4 { get; set; }
        public int[] Ints_6 { get; set; }
        public int[] Ints_e { get; set; }
        /// <summary>
        /// Always 0, except in model 3001. Seems like float16.
        /// </summary>
        public short Unk_16 { get; set; } // TODO: win-demo
        public int Bitmask1 { get; set; }
        public int Bitmask2 { get; set; }
        /// <summary>
        /// Always 0, except in model 3001. Seems like float16.
        /// </summary>
        public short Unk_20 { get; set; } // TODO: win-demo
        public byte Byte_22 { get; set; }
        public byte Byte_23 { get; set; }
        public byte Byte_24 { get; set; }
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

        #region Methods (serialization)

        public void Serialize(CustomComponent customComponent)
        {
            EndianBinaryWriter w = customComponent.Writer;

            w.Write(AlphaBpp);
            w.Write(Word_4);

            w.Write(Ints_6[0]);
            w.Write(Ints_6[1]);

            w.Write(Ints_e[0]);
            w.Write(Ints_e[1]);

            w.Write(Unk_16);

            w.Write(Bitmask1);
            w.Write(Bitmask2);

            w.Write(Unk_20);

            w.Write(Byte_22);
            w.Write(Byte_23);
            w.Write(Byte_24);
            w.Write(Byte_25);

            w.Write(Unk_26);
            w.Write(Unk_28);
            w.Write(Unk_2a);
            w.Write(Unk_2c);

            w.Write(Byte_2e);
            w.Write(Byte_2f);
            w.Write(Byte_30);
            w.Write(Byte_31);

            w.Write(Unk_32);
        }

        public void Deserialize(CustomComponent customComponent)
        {
            EndianBinaryReader r = customComponent.Reader;

            AlphaBpp = r.ReadInt32();
            Word_4 = r.ReadInt16();

            Ints_6 = new int[] {
                r.ReadInt32(),
                r.ReadInt32(),
            };

            Ints_e = new int[] {
                r.ReadInt32(),
                r.ReadInt32(),
            };

            Unk_16 = r.ReadInt16();

            Bitmask1 = r.ReadInt32();
            Bitmask2 = r.ReadInt32();

            Unk_20 = r.ReadInt16();

            Byte_22 = r.ReadByte();
            Byte_23 = r.ReadByte();
            Byte_24 = r.ReadByte();
            Byte_25 = r.ReadByte();

            Unk_26 = r.ReadInt16();
            Unk_28 = r.ReadInt16();
            Unk_2a = r.ReadInt16();
            Unk_2c = r.ReadInt16();

            Byte_2e = r.ReadByte();
            Byte_2f = r.ReadByte();
            Byte_30 = r.ReadByte();
            Byte_31 = r.ReadByte();

            Unk_32 = r.ReadInt16();
        }

        #endregion
    }
}
