// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using ByteSerialization.Components.Values.Customs;
using ByteSerialization.IO;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.Geometry;
using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices
{
    public class IndicesChunk05 : IndicesChunk, ICustomSerializable
    {
        #region Fields

        private static readonly byte[] padding = new byte[4];

        #endregion

        #region Properties

        [Order(0)]
        public byte Index0 { get; set; }
        [Order(1)]
        public byte Index1 { get; set; }
        [Order(2)]
        public byte Index2 { get; set; }

        #endregion

        #region Properties (abstraction)

        public override IEnumerable<int> Indices
        {
            get
            {
                yield return Index0;
                yield return Index1;
                yield return Index2;
            }
        }

        public override IEnumerable<Triangle> Triangles
        {
            get
            {
                yield return Triangle;
            }
        }

        public Triangle Triangle => 
            new Triangle(Index0, Index1, Index2);

        #endregion

        #region Constructor

        public IndicesChunk05() => 
            Tag = 5;

        #endregion

        #region Methods (: ICustomSerializable)

        public void Serialize(CustomComponent customComponent)
        {
            EndianBinaryWriter w = customComponent.Writer;

            w.Write(Tag);
            w.Write(Index0);
            w.Write(Index1);
            w.Write(Index2);
            w.Write(padding);
        }

        public void Deserialize(CustomComponent customComponent)
        {
            // TODO: not called

            EndianBinaryReader r = customComponent.Reader;

            Tag = r.ReadByte();
            Index0 = r.ReadByte();
            Index1 = r.ReadByte();
            Index2 = r.ReadByte();
            r.ReadBytes(padding.Length);
        }

        #endregion

        #region Methods (: object)

        public override string ToString() =>
            $"({Tag} {Triangle})";

        #endregion
    }
}
