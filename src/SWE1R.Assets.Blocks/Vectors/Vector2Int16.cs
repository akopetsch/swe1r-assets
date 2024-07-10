// SPDX-License-Identifier: MIT

using ByteSerialization.IO;
using SystemNumericsVector2 = System.Numerics.Vector2;

namespace SWE1R.Assets.Blocks.Vectors
{
    public class Vector2Int16 : Vector2<short>
    {
        #region Properties (helper)

        public override double Magnitude =>
            GetMagnitude(X, Y);

        #endregion

        #region Constructor

        public Vector2Int16() :
            base()
        { }

        public Vector2Int16(short x, short y) :
            base(x, y)
        { }

        #endregion

        #region Methods (serialization)

        public override void Serialize(EndianBinaryWriter writer)
        {
            writer.Write(X);
            writer.Write(Y);
        }

        public override void Deserialize(EndianBinaryReader reader)
        {
            X = reader.ReadInt16();
            Y = reader.ReadInt16();
        }

        #endregion

        #region Methods (operators - conversion)

        public static implicit operator SystemNumericsVector2(Vector2Int16 v) =>
            new SystemNumericsVector2(v.X, v.Y);

        #endregion
    }
}
