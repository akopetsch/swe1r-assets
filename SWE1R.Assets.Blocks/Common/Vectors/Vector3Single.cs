// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.IO;
using SystemNumericsVector3 = System.Numerics.Vector3;

namespace SWE1R.Assets.Blocks.Common.Vectors
{
    public class Vector3Single : Vector3<float>
    {
        #region Properties (helper)

        public override double Magnitude =>
            GetMagnitude(X, Y, Z);

        #endregion

        #region Constructor

        public Vector3Single() :
            base()
        { }

        public Vector3Single(float x, float y, float z) :
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
            X = reader.ReadSingle();
            Y = reader.ReadSingle();
            Z = reader.ReadSingle();
        }

        #endregion

        #region Methods (operators - arithmetics)

        // TODO: only use System.Numerics.Vector3 for arithmetics

        public static Vector3Single operator +(Vector3Single a, Vector3Single b) =>
            new Vector3Single(
                a.X + b.X,
                a.Y + b.Y,
                a.Z + b.Z);

        public static Vector3Single operator -(Vector3Single a, Vector3Single b) =>
            new Vector3Single(
                a.X - b.X, 
                a.Y - b.Y, 
                a.Z - b.Z);

        public static Vector3Single operator /(Vector3Single a, Vector3Single b) =>
            new Vector3Single(
                a.X - b.X,
                a.Y - b.Y,
                a.Z - b.Z);

        #endregion

        #region Methods (operators - conversion)

        public static implicit operator Vector3Single(SystemNumericsVector3 v) =>
            new Vector3Single(v.X, v.Y, v.Z);

        public static implicit operator SystemNumericsVector3(Vector3Single v) =>
            new SystemNumericsVector3(v.X, v.Y, v.Z);

        #endregion
    }
}
