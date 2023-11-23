// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Components.Values.Customs;
using System;
using System.Linq;

namespace SWE1R.Assets.Blocks.Common.Colors
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

        public static ColorRgba32 Pink { get; } =
            new ColorRgba32(byte.MaxValue, byte.MaxValue, 0, byte.MaxValue);

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
        {

        }

        #endregion

        #region Methods (serialization)

        public void Serialize(CustomComponent customComponent) =>
            customComponent.Writer.Write(Bytes.Reverse().ToArray());

        public void Deserialize(CustomComponent customComponent) =>
            BytesValue = customComponent.Reader.ReadInt32();

        #endregion

        #region Methods (operators - conversion)

        public static explicit operator ColorArgbF(ColorRgba32 c)
        {
            float r = (float)c.R / byte.MaxValue;
            float g = (float)c.G / byte.MaxValue;
            float b = (float)c.B / byte.MaxValue;
            float a = (float)c.A / byte.MaxValue;
            return new ColorArgbF(a, r, g, b);
        }

        public static explicit operator ColorRgba32(ColorArgbF c)
        {
            byte r = (byte)Math.Round((double)c.R * byte.MaxValue);
            byte g = (byte)Math.Round((double)c.G * byte.MaxValue);
            byte b = (byte)Math.Round((double)c.B * byte.MaxValue);
            byte a = (byte)Math.Round((double)c.A * byte.MaxValue);
            return new ColorRgba32(r, g, b, a);
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
