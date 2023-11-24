// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.IO;
using SystemNumericsVector3 = System.Numerics.Vector3;

namespace SWE1R.Assets.Blocks.Common.Vectors
{
    public class Vector3Int16 : Vector3<short>
    {
        #region Properties (helper)

        public override double Magnitude =>
            GetMagnitude(X, Y, Z);

        #endregion

        #region Constructor

        public Vector3Int16() :
            base()
        { }

        public Vector3Int16(short x, short y, short z) :
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
            X = reader.ReadInt16();
            Y = reader.ReadInt16();
            Z = reader.ReadInt16();
        }

        #endregion

        #region Methods (operators - conversion)

        public static explicit operator Vector3Int16(Vector3Single v) =>
            new Vector3Int16((short)v.X, (short)v.Y, (short)v.Z);

        public static implicit operator Vector3Single(Vector3Int16 v) =>
            new Vector3Single(v.X, v.Y, v.Z);

        public static explicit operator Vector3Int16(SystemNumericsVector3 v) =>
            new Vector3Int16((short)v.X, (short)v.Y, (short)v.Z);

        public static implicit operator SystemNumericsVector3(Vector3Int16 v) =>
            new SystemNumericsVector3(v.X, v.Y, v.Z);

        #endregion
    }
}
