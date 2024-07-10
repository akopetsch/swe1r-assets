// SPDX-License-Identifier: MIT

using ByteSerialization;
using ByteSerialization.IO;
using System;
using System.Linq;

namespace SWE1R.Assets.Blocks.Vectors
{
    /// <summary>
    /// <see href="https://github.com/tim-tim707/SW_RACER_RE/blob/d2d15c27d81e51e91996563795643c91439147aa/src/types.h#L84">
    ///   github.com - tim-tim707/SW_RACER_RE - types.h - rdMatrix34</see>
    /// </summary>
    public class Matrix3x4Single : ICustomSerializable
    {
        #region Fields (const)

        public const int Width = 3;
        public const int Height = 4;
        public const int ElementsCount = Width * Height;

        #endregion

        #region Properties (serialized)

        public float[] Elements { get; private set; } = new float[ElementsCount];

        #endregion

        #region Properties (helper: row vectors)

        public Vector3Single RightVector
        {
            get => this[0];
            set => this[0] = value;
        }

        public Vector3Single LeftVector
        {
            get => this[1];
            set => this[1] = value;
        }

        public Vector3Single UpVector
        {
            get => this[2];
            set => this[2] = value;
        }

        public Vector3Single Scale
        {
            get => this[3];
            set => this[3] = value;
        }

        public Vector3Single[] RowVectors
        {
            get => new Vector3Single[]
            {
                RightVector, 
                LeftVector, 
                UpVector, 
                Scale
            };
            set
            {
                if (RowVectors == null)
                    throw new ArgumentNullException();
                if (RowVectors.Length != Height)
                    throw new ArgumentException();

                RightVector = value[0];
                LeftVector = value[1];
                UpVector = value[2];
                Scale = value[3];
            }
        }

        #endregion

        #region Indexers

        public float this[int row, int column]
        {
            get => Elements[row * Width + column];
            set => Elements[row * Width + column] = value;
        }

        public Vector3Single this[int row]
        {
            get => new Vector3Single(
                this[row, 0], 
                this[row, 1], 
                this[row, 2]);
            set
            {
                this[row, 0] = value.X;
                this[row, 1] = value.Y;
                this[row, 2] = value.Z;
            }
        }

        #endregion

        #region Methods (serialization)

        public void Serialize(EndianBinaryWriter writer)
        {
            foreach (float element in Elements)
                writer.Write(element);
        }

        public void Deserialize(EndianBinaryReader reader)
        {
            Elements = new float[ElementsCount];
            for (int i = 0; i < ElementsCount; i++)
                Elements[i] = reader.ReadSingle();
        }

        #endregion

        #region Methods (: object)

        public bool Equals(Matrix3x4Single other)
        {
            if (other == null)
                return false;
            return Elements.SequenceEqual(other.Elements);
        }

        public override string ToString() =>
            $"({string.Join(", ", RowVectors.Cast<object>().ToArray())})";

        #endregion
    }
}
