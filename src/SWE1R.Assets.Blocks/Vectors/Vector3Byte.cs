// SPDX-License-Identifier: MIT

using ByteSerialization.IO;
using SystemNumericsVector3 = System.Numerics.Vector3;

namespace SWE1R.Assets.Blocks.Vectors
{
    public class Vector3Byte : Vector3<byte>
    {
        #region Properties (helper)

        public override double Magnitude =>
            GetMagnitude(X, Y, Z);

        #endregion

        #region Constructor

        public Vector3Byte() :
            base()
        { }

        public Vector3Byte(byte x, byte y, byte z) :
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
            X = reader.ReadByte();
            Y = reader.ReadByte();
            Z = reader.ReadByte();
        }

        #endregion

        #region Methods (operators - conversion)

        public static explicit operator Vector3Byte(Vector3Single v) =>
            new Vector3Byte((byte)v.X, (byte)v.Y, (byte)v.Z);

        public static implicit operator Vector3Single(Vector3Byte v) =>
            new Vector3Single(v.X, v.Y, v.Z);

        public static explicit operator Vector3Byte(SystemNumericsVector3 v) =>
            new Vector3Byte((byte)v.X, (byte)v.Y, (byte)v.Z);

        public static implicit operator SystemNumericsVector3(Vector3Byte v) =>
            new SystemNumericsVector3(v.X, v.Y, v.Z);

        #endregion
    }
}
