﻿// SPDX-License-Identifier: MIT

using ByteSerialization;
using ByteSerialization.IO;
using System.Linq;

namespace SWE1R.Assets.Blocks.Vectors
{
    public class Matrix3x4Single : ICustomSerializable
    {
        #region Fields

        public const int Width = 4;
        public const int Height = 3;
        public const int ElementsCount = Width * Height;

        #endregion

        #region Properties

        public float[] Elements { get; private set; } = new float[ElementsCount];

        #endregion

        #region Indexers

        public float this[int row, int column]
        {
            get => Elements[row + column * Height];
            set => Elements[row + column * Height] = value;
            // TODO: verify indexer arithmetic
        }

        // TODO: row indexer

        #endregion

        #region Methods

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

        public bool Equals(Matrix3x4Single other)
        {
            if (other == null)
                return false;
            return Elements.SequenceEqual(other.Elements);
        }

        public override string ToString() =>
            $"({string.Join(", ", Elements)})"; // TODO: enclose rows with brackets

        #endregion
    }
}
