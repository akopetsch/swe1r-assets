// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Components.Values.Customs;
using ByteSerialization.IO;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;

namespace SWE1R.Assets.Blocks.ModelBlock.Materials
{
    /// <summary>
    /// <see href="https://github.com/akopetsch/Sw_Racer/blob/76c8ad9cea549ea18457846a135a7f25d48b3813/include/Swr_Model.h#L266">SWR_MODEL_Section5_b</see>
    /// </summary>
    public class MaterialTextureChild : ICustomSerializable
    {
        #region Fields

        private static readonly byte[] padding = new byte[4];

        #endregion

        #region Properties (serialized)

        public byte Byte_0 { get; set; } // TODO: N64 only
        public byte Byte_1 { get; set; }
        public byte Byte_2 { get; set; }
        public DimensionsBitmask DimensionsBitmask { get; set; }
        public byte Byte_4 { get; set; }
        public byte Byte_5 { get; set; }
        public byte Byte_6 { get; set; }
        public byte Byte_7 { get; set; }
        public byte Byte_c { get; set; }
        public byte Byte_d { get; set; }
        public byte Byte_e { get; set; }
        public byte Byte_f { get; set; }

        #endregion

        #region Properties (helper)

        public bool HasDoubleHeight =>
            DimensionsBitmask.HasFlag(DimensionsBitmask.DoubleHeight);

        public bool HasDoubleWidth =>
            DimensionsBitmask.HasFlag(DimensionsBitmask.DoubleWidth);

        public bool IsFlippedVertically =>
            DimensionsBitmask.HasFlag(DimensionsBitmask.FlippedVertically);

        public bool IsFlippedHorizontally =>
            DimensionsBitmask.HasFlag(DimensionsBitmask.FlippedHorizontally);

        #endregion

        #region Methods (serialization)

        public void Serialize(CustomComponent customComponent)
        {
            EndianBinaryWriter w = customComponent.Writer;

            w.Write(Byte_0);
            w.Write(Byte_1);
            w.Write(Byte_2);
            w.Write((byte)DimensionsBitmask);
            w.Write(Byte_4);
            w.Write(Byte_5);
            w.Write(Byte_6);
            w.Write(Byte_7);
            w.Write(padding);
            w.Write(Byte_c);
            w.Write(Byte_d);
            w.Write(Byte_e);
            w.Write(Byte_f);
        }

        public void Deserialize(CustomComponent customComponent)
        {
            EndianBinaryReader r = customComponent.Reader;

            Byte_0 = r.ReadByte();
            Byte_1 = r.ReadByte();
            Byte_2 = r.ReadByte();
            DimensionsBitmask = (DimensionsBitmask)r.ReadByte();
            Byte_4 = r.ReadByte();
            Byte_5 = r.ReadByte();
            Byte_6 = r.ReadByte();
            Byte_7 = r.ReadByte();
            r.ReadBytes(padding.Length);
            Byte_c = r.ReadByte();
            Byte_d = r.ReadByte();
            Byte_e = r.ReadByte();
            Byte_f = r.ReadByte();
        }

        #endregion
    }
}
