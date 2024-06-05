// SPDX-License-Identifier: MIT

using ByteSerialization;
using ByteSerialization.IO;
using SWE1R.Assets.Blocks.ModelBlock.Meshes;

namespace SWE1R.Assets.Blocks.ModelBlock.Materials
{
    /// <summary>
    /// <para>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="https://github.com/akopetsch/SW_RACER_RE/blob/d2d15c27d81e51e91996563795643c91439147aa/src/types.h#L1452">
    ///       github.com - akopetsch/SW_RACER_RE - types.h - swrModel_MaterialTextureChild</see></item>
    ///   <item>
    ///     <see href="https://github.com/akopetsch/Sw_Racer/blob/76c8ad9cea549ea18457846a135a7f25d48b3813/include/Swr_Model.h#L266">
    ///       github.com - akopetsch/Sw_Racer - Swr_Model.h - SWR_MODEL_Section5_b</see></item>
    /// </list>
    /// </para>
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

        #region Methods (: ICustomSerializable)

        public void Serialize(EndianBinaryWriter writer)
        {
            writer.Write(Byte_0);
            writer.Write(Byte_1);
            writer.Write(Byte_2);
            writer.Write((byte)DimensionsBitmask);
            writer.Write(Byte_4);
            writer.Write(Byte_5);
            writer.Write(Byte_6);
            writer.Write(Byte_7);
            writer.Write(padding);
            writer.Write(Byte_c);
            writer.Write(Byte_d);
            writer.Write(Byte_e);
            writer.Write(Byte_f);
        }

        public void Deserialize(EndianBinaryReader r)
        {
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
