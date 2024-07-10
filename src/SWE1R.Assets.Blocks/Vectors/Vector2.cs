// SPDX-License-Identifier: MIT

using ByteSerialization;
using ByteSerialization.IO;
using System;
using System.Runtime.InteropServices;

namespace SWE1R.Assets.Blocks.Vectors
{
    public abstract class Vector2<T> : ICustomSerializable // TODO: use struct
        where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
    {
        #region Properties (serialized)

        public T X { get; set; }
        public T Y { get; set; }

        #endregion

        #region Properties (helper)

        public abstract double Magnitude { get; }

        public static int StructureSize =>
            Marshal.SizeOf(typeof(T)) * 2; // TODO: implement helper in ByteSerializer

        #endregion

        #region Constructor

        public Vector2() { }

        public Vector2(T x, T y)
        {
            X = x;
            Y = y;
        }

        #endregion

        #region Methods (serialization)

        public abstract void Serialize(EndianBinaryWriter writer);

        public abstract void Deserialize(EndianBinaryReader reader);

        #endregion

        #region Methods (helper)

        protected double GetMagnitude(double x, double y) =>
            Math.Sqrt(
                Math.Pow(x, 2) +
                Math.Pow(y, 2));

        public bool Equals(Vector3<T> other)
        {
            if (other == null)
                return false;
            if (!X.Equals(other.X))
                return false;
            if (!Y.Equals(other.Y))
                return false;
            return true;
        }

        #endregion

        #region Methods (: object)

        public override string ToString() =>
            $"({nameof(X)}={X}, " +
            $"{nameof(Y)}={Y})";

        #endregion
    }
}
