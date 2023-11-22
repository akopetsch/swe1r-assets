// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Components.Values.Customs;
using ByteSerialization.IO;
using SWE1R.Assets.Blocks.Common.Vectors;
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

        #region Properties (serialized)

        public Vector3Int16 Position { get; set; }
        public short U { get; set; }
        public short V { get; set; }
        public Vector3SByte Normal { get; set; }
        public byte Alpha { get; set; }

        #endregion

        #region Properties (serialization)

        public static int StructureSize { get; } = 16;

        #endregion

        #region Methods (serialization)

        public void Serialize(CustomComponent customComponent)
        {
            EndianBinaryWriter w = customComponent.Writer;

            w.Write(Position.X);
            w.Write(Position.Y);
            w.Write(Position.Z);
            w.Write(padding);
            w.Write(U);
            w.Write(V);
            w.Write(Normal.X);
            w.Write(Normal.Y);
            w.Write(Normal.Z);
            w.Write(Alpha);
        }

        public void Deserialize(CustomComponent customComponent)
        {
            EndianBinaryReader r = customComponent.Reader;

            Position = new Vector3Int16();
            Position.Deserialize(r);
            r.ReadBytes(padding.Length);
            U = r.ReadInt16();
            V = r.ReadInt16();
            Normal = new Vector3SByte();
            Normal.Deserialize(r);
            Alpha = r.ReadByte();
        }

        #endregion

        #region Methods (export)

        public Vector2 GetEffectiveUV(MaterialTextureChild materialTextureChild)
        {
            const int divisor = 4096;
            const int doubleDivisor = divisor * 2;

            float u, v, uMax, vMax;
            if (materialTextureChild != null)
            {
                uMax = materialTextureChild.HasDoubleWidth ? doubleDivisor : divisor;
                vMax = materialTextureChild.HasDoubleHeight ? doubleDivisor : divisor;
                u = U / uMax;
                v = V / vMax;
                if (materialTextureChild.IsFlippedHorizontally) u -= 1;
                if (materialTextureChild.IsFlippedVertically) v -= 1;
                // TODO: also consider Material.Properties.IsFlipped
            }
            else
            {
                uMax = divisor;
                vMax = divisor;
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
            $"{nameof(Normal)} = {Normal}, " +
            $"{nameof(Alpha)} = {Alpha})";

        #endregion
    }
}
