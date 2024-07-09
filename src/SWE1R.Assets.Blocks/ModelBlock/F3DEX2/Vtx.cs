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
        #region Fields (serialization)

        private static readonly byte[] PaddingBytes = new byte[2];

        #endregion

        #region Properties (serialized)

        public Vector3Int16 Position { get; set; }
        /// <summary>
        /// Offset: 0x08
        /// </summary>
        public short U { get; set; }
        /// <summary>
        /// Offset: 0x0A
        /// </summary>
        public short V { get; set; }
        public byte Byte_C { get; set; }
        public byte Byte_D { get; set; }
        public byte Byte_E { get; set; }
        /// <summary>
        /// In the original asset files, if the vertex has a normal and not a color, the value is always 255. 
        /// But that is just an indicator. A value somewhere else in the binary structures determines whether 
        /// the game interprets it as a normal or a color, which means that if you change this value to 255 
        /// that does not automatically make the vertex to have a normal instead of a color in the game. 
        /// </summary>
        public byte Byte_F { get; set; }

        #endregion

        #region Properties (serialization)

        public static int StructureSize { get; } = 16;

        #endregion

        #region Properties (C union style access)

        public ColorRgba32 Color
        {
            get => new ColorRgba32(
                Byte_C,
                Byte_D,
                Byte_E,
                Byte_F);
            set
            {
                Byte_C = value.R;
                Byte_D = value.G;
                Byte_E = value.B;
                Byte_F = value.A;
            }
        }

        public Vector3SByte Normal
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
            Position.Serialize(writer);
            writer.Write(PaddingBytes);
            writer.Write(U);
            writer.Write(V);
            writer.Write(Byte_C);
            writer.Write(Byte_D);
            writer.Write(Byte_E);
            writer.Write(Byte_F);
        }

        public void Deserialize(EndianBinaryReader reader)
        {
            Position = new Vector3Int16();
            Position.Deserialize(reader);
            reader.Read<byte>(PaddingBytes.Length);
            U = reader.ReadInt16();
            V = reader.ReadInt16();
            Byte_C = reader.ReadByte();
            Byte_D = reader.ReadByte();
            Byte_E = reader.ReadByte();
            Byte_F = reader.ReadByte();
        }

        #endregion

        #region Methods (: object)

        public override string ToString() =>
            $"({nameof(Position)} = {Position}, " +
            $"{nameof(U)} = {U}, " +
            $"{nameof(V)} = {V}, " +
            $"({Byte_C}, {Byte_D}, {Byte_E}, {Byte_F}))";

        #endregion
    }
}
