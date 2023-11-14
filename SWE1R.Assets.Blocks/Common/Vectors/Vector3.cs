// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Components.Values.Customs;
using ByteSerialization.IO;
using System;
using System.Runtime.InteropServices;

namespace SWE1R.Assets.Blocks.Common.Vectors
{
    public abstract class Vector3<T> : ICustomSerializable
        where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
    {
        public T X { get; set; }
        public T Y { get; set; }
        public T Z { get; set; }

        public static int StructureSize =>
            Marshal.SizeOf(typeof(T)) * 3; // TODO: implement helper in ByteSerializer

        public Vector3() { }

        public Vector3(T x, T y, T z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public void Serialize(CustomComponent customComponent) =>
            Serialize(customComponent.Writer);

        public void Deserialize(CustomComponent customComponent) =>
            Deserialize(customComponent.Reader);

        public abstract void Serialize(EndianBinaryWriter writer);

        public abstract void Deserialize(EndianBinaryReader reader);

        public bool Equals(Vector3<T> other)
        {
            if (other == null)
                return false;
            if (!X.Equals(other.X))
                return false;
            if (!Y.Equals(other.Y))
                return false;
            if (!Z.Equals(other.Z))
                return false;
            return true;
        }

        public override string ToString() => 
            $"(X={X}, Y={Y}, Z={Z})";
    }
}
