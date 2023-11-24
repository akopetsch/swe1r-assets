// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Components.Values.Customs;
using ByteSerialization.IO;
using SWE1R.Assets.Blocks.Common.Colors;
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

        #region Constants

        public const int UvDivisor = 4096;
        public const int DoubleUvDivisor = 2 * UvDivisor;

        #endregion

        #region Properties (serialized)

        public Vector3Int16 Position { get; set; }
        public short U { get; set; }
        public short V { get; set; }
        public ColorRgba32 Color { get; set; }

        #endregion

        #region Properties (serialization)

        public static int StructureSize { get; } = 16;

        #endregion

        #region Methods (serialization)

        public void Serialize(CustomComponent customComponent)
        {
            EndianBinaryWriter w = customComponent.Writer;

            Position.Serialize(w);
            w.Write(padding);
            w.Write(U);
            w.Write(V);
            Color.Serialize(w);
        }

        public void Deserialize(CustomComponent customComponent)
        {
            EndianBinaryReader r = customComponent.Reader;

            Position = new Vector3Int16();
            Position.Deserialize(r);
            r.ReadBytes(padding.Length);
            U = r.ReadInt16();
            V = r.ReadInt16();
            Color = new ColorRgba32();
            Color.Deserialize(r);
        }

        #endregion

        #region Methods (export)

        public Vector2 GetEffectiveUV(MaterialTextureChild materialTextureChild)
        {
            float u, v, uMax, vMax;
            if (materialTextureChild != null)
            {
                uMax = materialTextureChild.HasDoubleWidth ? DoubleUvDivisor : UvDivisor;
                vMax = materialTextureChild.HasDoubleHeight ? DoubleUvDivisor : UvDivisor;
                u = U / uMax;
                v = V / vMax;
                if (materialTextureChild.IsFlippedHorizontally) u -= 1;
                if (materialTextureChild.IsFlippedVertically) v -= 1;
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
            $"{nameof(Color)} = {Color})";

        #endregion
    }
}
