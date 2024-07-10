// SPDX-License-Identifier: MIT

using ByteSerialization;
using ByteSerialization.IO;
using SWE1R.Assets.Blocks.Colors;
using SWE1R.Assets.Blocks.Vectors;

namespace SWE1R.Assets.Blocks.ModelBlock.F3DEX2
{
    /// <summary>
    /// <para>
    /// See also:
    /// <list type="bullet">
    ///   <item>
    ///     <see href="https://ultra64.ca/files/documentation/online-manuals/man/n64man/gsp/gSPVertex.html#:~:text=Vtx%3B">
    ///       ultra64.ca - 'Online Manuals (OS 2.0J)' - 'gSPVertex'</see></item>
    ///   <item>
    ///     <see href="https://ultra64.ca/files/documentation/online-manuals/man/header/gbi.htm#:~:text=Vtx">
    ///       ultra64.ca - 'Online Manuals (OS 2.0J)' - gbi.h - Vtx</see></item>
    ///   <item>
    ///     <see href="https://github.com/tim-tim707/SW_RACER_RE/blob/d2d15c27d81e51e91996563795643c91439147aa/src/types.h#L1535">
    ///       github.com - tim-tim707/SW_RACER_RE - types.h - Vtx</see></item>
    ///   <item>
    ///     <see href="https://github.com/Olganix/Sw_Racer/blob/76c8ad9cea549ea18457846a135a7f25d48b3813/include/Swr_Model.h#L530">
    ///       github.com - Olganix/Sw_Racer - Swr_Model.h - SWR_MODEL_Section52</see></item>
    /// </list>
    /// </para>
    /// </summary>
    public class Vtx : ICustomSerializable
    {
        #region Properties (serialized)

        /// <summary>
        /// x, y, z
        /// </summary>
        public Vector3Int16 Ob { get; set; }
        /// <summary>
        /// Currently has no meaning
        /// </summary>
        public ushort Flag { get; set; }
        /// <summary>
        /// Texture coordinates
        /// </summary>
        public Vector2Int16 Tc { get; set; }
        public byte Byte_C { get; set; }
        public byte Byte_D { get; set; }
        public byte Byte_E { get; set; }
        /// <summary>
        /// Alpha
        /// </summary>
        public byte A { get; set; }

        #endregion

        #region Properties (C union style access)

        // Color & alpha
        public ColorRgba32 Cn
        {
            get => new ColorRgba32(
                Byte_C,
                Byte_D,
                Byte_E,
                A);
            set
            {
                Byte_C = value.R;
                Byte_D = value.G;
                Byte_E = value.B;
                A = value.A;
            }
        }

        /// <summary>
        /// Normal
        /// </summary>
        public Vector3SByte N
        {
            get => new Vector3SByte(
                Byte_C,
                Byte_D,
                Byte_E);
            set
            {
                Byte_C = (byte)value.X;
                Byte_D = (byte)value.Y;
                Byte_E = (byte)value.Z;
            }
        }

        #endregion

        #region Methods (: ICustomSerializable)

        public void Serialize(EndianBinaryWriter writer)
        {
            Ob.Serialize(writer);
            writer.Write(Flag);
            Tc.Serialize(writer);
            writer.Write(Byte_C);
            writer.Write(Byte_D);
            writer.Write(Byte_E);
            writer.Write(A);
        }

        public void Deserialize(EndianBinaryReader reader)
        {
            Ob = new Vector3Int16();
            Ob.Deserialize(reader);
            Flag = reader.ReadUInt16();
            Tc = new Vector2Int16();
            Tc.Deserialize(reader);
            Byte_C = reader.ReadByte();
            Byte_D = reader.ReadByte();
            Byte_E = reader.ReadByte();
            A = reader.ReadByte();
        }

        #endregion

        #region Methods (: object)

        public override string ToString() =>
            $"({Ob}, {Flag}, {Tc}, {Byte_C}, {Byte_D}, {Byte_E}, {A})";

        #endregion
    }
}
