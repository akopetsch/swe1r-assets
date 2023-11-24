// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Components.Values.Customs;
using System;
using System.Linq;

namespace SWE1R.Assets.Blocks.Common.Colors
{
    public class ColorRgba5551 : ICustomSerializable // TODO: struct
    {
        #region Fields (constants)

        private const int _5BitsMaxValue = 31;
        private const int _1BitsMaxValue = 1;
        private const int _rShift = 11;
        private const int _gShift = 6;
        private const int _bShift = 1;
        private const int _rMask = 0xf800;
        private const int _gMask = 0x07c0;
        private const int _bMask = 0x003e;
        private const int _aMask = 0x0001;

        #endregion

        #region Properties (serialized)

        // TODO: use ByteSerializer

        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public bool A { get; set; }

        #endregion

        #region Properties (helper)

        public short BytesValue
        {
            get
            {
                int r = R << _rShift;
                int g = G << _gShift;
                int b = B << _bShift;
                int a = A ? 1 : 0;
                return (short)(r | g | b | a);
            }
            set
            {
                R = (byte)((value & _rMask) >> _rShift);
                G = (byte)((value & _gMask) >> _gShift);
                B = (byte)((value & _bMask) >> _bShift);
                A = (value & _aMask) > 0;
            }
        }

        public byte[] Bytes =>
            BitConverter.GetBytes(BytesValue);

        public static int StructureSize =>
            sizeof(short);

        #endregion

        #region Constructor

        public ColorRgba5551() { }

        public ColorRgba5551(short bytesValue) =>
            BytesValue = bytesValue;

        #endregion

        #region Methods (serialization)

        public void Serialize(CustomComponent customComponent) =>
            customComponent.Writer.Write(Bytes.Reverse().ToArray());

        public void Deserialize(CustomComponent customComponent) =>
            BytesValue = customComponent.Reader.ReadInt16();

        #endregion

        #region Methods (operators - conversion)

        public static explicit operator ColorRgba32(ColorRgba5551 c) =>
            (ColorRgba32)(ColorArgbF)c;

        public static explicit operator ColorRgba5551(ColorRgba32 c) =>
            (ColorRgba5551)(ColorArgbF)c;

        public static explicit operator ColorArgbF(ColorRgba5551 c)
        {
            float r = (float)c.R / _5BitsMaxValue;
            float g = (float)c.G / _5BitsMaxValue;
            float b = (float)c.B / _5BitsMaxValue;
            float a = c.A ? 1 : 0;
            return new ColorArgbF(a, r, g, b);
        }

        public static explicit operator ColorRgba5551(ColorArgbF c)
        {
            byte r = (byte)Math.Round(c.R * _5BitsMaxValue);
            byte g = (byte)Math.Round(c.G * _5BitsMaxValue);
            byte b = (byte)Math.Round(c.B * _5BitsMaxValue);
            byte a = (byte)Math.Round(c.A * _1BitsMaxValue);
            return new ColorRgba5551((short)((r << _rShift) | (g << _gShift) | (b << _bShift) | a));
        }

        #endregion

        #region Methods (: object)

        public override bool Equals(object obj)
        {
            if (obj is ColorRgba5551 other)
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
