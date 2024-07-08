// SPDX-License-Identifier: MIT

using ByteSerialization;
using ByteSerialization.IO;
using System;

namespace SWE1R.Assets.Blocks.Colors
{
    public class ColorRgba32 : ICustomSerializable // TODO: struct
    {
        #region Fields (constants)

        private const int _8BitsMaxValue = byte.MaxValue;
        private const int _rShift = 24;
        private const int _gShift = 16;
        private const int _bShift = 8;
        private const int _mask = byte.MaxValue;

        #endregion

        #region Properties (serialized)

        // TODO: use ByteSerializer

        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public byte A { get; set; }

        #endregion

        #region Properties (helper)

        public int BytesValue
        {
            get
            {
                int r = R << _rShift;
                int g = G << _gShift;
                int b = B << _bShift;
                int a = A;
                return r | g | b | a;
            }
            set
            {
                R = (byte)((value & _mask) >> _rShift);
                G = (byte)((value & _mask) >> _gShift);
                B = (byte)((value & _mask) >> _bShift);
                A = (byte)(value & _mask);
            }
        }

        public byte[] Bytes =>
            new byte[] { A, B, G, R };

        public static int StructureSize =>
            4;

        public static ColorRgba32 White { get; } =
            new ColorRgba32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

        public static ColorRgba32 Pink { get; } =
            new ColorRgba32(byte.MaxValue, 0, byte.MaxValue, byte.MaxValue);

        #endregion

        #region Constructor

        public ColorRgba32() { }

        public ColorRgba32(int bytesValue) =>
            BytesValue = bytesValue;

        public ColorRgba32(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public ColorRgba32(byte[] bytes) :
            this(bytes[0], bytes[1], bytes[2], bytes[3])
        { // TODO: do not use this constructor?

        }

        #endregion

        #region Methods (serialization)

        public void Serialize(EndianBinaryWriter writer)
        {
            writer.Write(R);
            writer.Write(G);
            writer.Write(B);
            writer.Write(A);
        }

        public void Deserialize(EndianBinaryReader reader)
        {
            R = reader.ReadByte();
            G = reader.ReadByte();
            B = reader.ReadByte();
            A = reader.ReadByte();
        }

        #endregion

        #region Methods (: object)

        public override bool Equals(object obj)
        {
            if (obj is ColorRgba32 other)
            {
                if (R != other.R)
                    return false;
                if (G != other.G)
                    return false;
                if (B != other.B)
                    return false;
                if (A != other.A)
                    return false;
                return true;
            }
            return false;
        }

        public override int GetHashCode() =>
            HashCode.Combine(R, G, B, A);

        public override string ToString() =>
            $"({nameof(R)}={R}, " +
            $"{nameof(G)}={G}, " +
            $"{nameof(B)}={B}, " +
            $"{nameof(A)}={A})";

        #endregion
    }
}
