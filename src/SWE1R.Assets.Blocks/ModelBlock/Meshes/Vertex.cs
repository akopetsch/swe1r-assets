// SPDX-License-Identifier: GPL-2.0-only

using ByteSerialization.Components.Values.Customs;
using ByteSerialization.IO;
using SWE1R.Assets.Blocks.Colors;
using SWE1R.Assets.Blocks.ModelBlock.Materials;
using SWE1R.Assets.Blocks.Vectors;
using System.Numerics;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes
{
    /// <summary>
    /// <see href="https://github.com/akopetsch/Sw_Racer/blob/76c8ad9cea549ea18457846a135a7f25d48b3813/include/Swr_Model.h#L530">SWR_MODEL_Section52</see>
    /// </summary>
    public class Vertex : ICustomSerializable
    {
        #region Fields (serialization)

        private static readonly byte[] padding = new byte[2];

        #endregion

        #region Constants

        public const int UvDivisor = 4096;
        public const int UvDoubleDivisor = 2 * UvDivisor;

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
                Byte_D = (byte)value.X;
                Byte_E = (byte)value.X;
                Byte_F = (byte)value.X;
            }
        }

        #endregion

        #region Methods (serialization)

        public void Serialize(CustomComponent customComponent)
        {
            EndianBinaryWriter w = customComponent.Writer;

            Position.Serialize(w);
            w.Write(padding);
            w.Write(U);
            w.Write(V);
            w.Write(Byte_C);
            w.Write(Byte_D);
            w.Write(Byte_E);
            w.Write(Byte_F);
        }

        public void Deserialize(CustomComponent customComponent)
        {
            EndianBinaryReader r = customComponent.Reader;

            Position = new Vector3Int16();
            Position.Deserialize(r);
            r.ReadBytes(padding.Length);
            U = r.ReadInt16();
            V = r.ReadInt16();
            Byte_C = r.ReadByte();
            Byte_D = r.ReadByte();
            Byte_E = r.ReadByte();
            Byte_F = r.ReadByte();
        }

        #endregion

        #region Methods (export)

        public Vector2 GetEffectiveUV(MaterialTextureChild materialTextureChild)
        {
            float u, v, uMax, vMax;
            if (materialTextureChild != null)
            {
                uMax = materialTextureChild.HasDoubleWidth ? UvDoubleDivisor : UvDivisor;
                vMax = materialTextureChild.HasDoubleHeight ? UvDoubleDivisor : UvDivisor;
                u = U / uMax;
                v = V / vMax;
                if (materialTextureChild.IsFlippedHorizontally)
                    u -= 1;
                if (materialTextureChild.IsFlippedVertically)
                    v -= 1;
                // TODO: also consider Material.Properties.IsFlipped
            }
            else
            {
                uMax = UvDivisor;
                vMax = UvDivisor;
                u = U / uMax;
                v = V / vMax;
            }

            return new Vector2(u, v);
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
