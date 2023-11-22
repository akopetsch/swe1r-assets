// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.IO;
using System.Numerics;

namespace SWE1R.Assets.Blocks.Common.Vectors
{
    public class Vector3SByte : Vector3<sbyte>
    {
        #region Properties (helper)

        public override double Magnitude =>
            GetMagnitude(X, Y, Z);

        #endregion

        #region Constructor

        public Vector3SByte() :
            base()
        { }

        public Vector3SByte(sbyte x, sbyte y, sbyte z) :
            base(x, y, z)
        { }

        #endregion

        #region Methods (serialization)

        public override void Serialize(EndianBinaryWriter writer)
        {
            writer.Write(X);
            writer.Write(Y);
            writer.Write(Z);
        }

        public override void Deserialize(EndianBinaryReader reader)
        {
            X = reader.ReadSByte();
            Y = reader.ReadSByte();
            Z = reader.ReadSByte();
        }

        #endregion

        #region Methods (operators - conversion)

        public static explicit operator Vector3SByte(Vector3Single v) =>
            new Vector3SByte((sbyte)v.X, (sbyte)v.Y, (sbyte)v.Z);

        public static implicit operator Vector3Single(Vector3SByte v) =>
            new Vector3Single(v.X, v.Y, v.Z);

        public static explicit operator Vector3SByte(Vector3 v) =>
            new Vector3SByte((sbyte)v.X, (sbyte)v.Y, (sbyte)v.Z);

        public static implicit operator Vector3(Vector3SByte v) =>
            new Vector3(v.X, v.Y, v.Z);

        #endregion
    }
}
