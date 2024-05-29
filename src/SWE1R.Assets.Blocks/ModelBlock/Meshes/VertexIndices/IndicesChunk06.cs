// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using ByteSerialization.Components.Values.Customs;
using ByteSerialization.IO;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.Geometry;
using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices
{
    public class IndicesChunk06 : IndicesChunk, ICustomSerializable
    {
        #region Properties (serialized)

        [Order(0)] 
        public byte Index0 { get; set; }
        [Order(1)]
        public byte Index1 { get; set; }
        [Order(2)]
        public byte Index2 { get; set; }
        [Order(3), Offset(5)]
        public byte Index3 { get; set; }
        [Order(4)]
        public byte Index4 { get; set; }
        [Order(5)]
        public byte Index5 { get; set; }

        #endregion

        #region Properties (abstraction)

        public override IEnumerable<int> Indices
        {
            get
            {
                yield return Index0;
                yield return Index1;
                yield return Index2;

                yield return Index3;
                yield return Index4;
                yield return Index5;
            }
        }

        public override IEnumerable<Triangle> Triangles
        {
            get
            {
                yield return Triangle0;
                yield return Triangle1;
            }
        }

        public Triangle Triangle0 => new Triangle(Index0, Index1, Index2);

        public Triangle Triangle1 => new Triangle(Index3, Index4, Index5);

        #endregion

        #region Constructor

        public IndicesChunk06() => 
            Tag = 6;

        #endregion

        #region Methods (: ICustomSerializable)

        public void Serialize(CustomComponent customComponent)
        {
            EndianBinaryWriter w = customComponent.Writer;

            w.Write(Tag);
            w.Write(Index0);
            w.Write(Index1);
            w.Write(Index2);
            w.Write((byte)0);
            w.Write(Index3);
            w.Write(Index4);
            w.Write(Index5);
        }

        public void Deserialize(CustomComponent customComponent)
        {
            // TODO: not called

            EndianBinaryReader r = customComponent.Reader;

            Tag = r.ReadByte();
            Index0 = r.ReadByte();
            Index1 = r.ReadByte();
            Index2 = r.ReadByte();
            r.ReadByte();
            Index3 = r.ReadByte();
            Index4 = r.ReadByte();
            Index5 = r.ReadByte();
        }

        #endregion

        #region Methods (: object)

        public override string ToString() =>
            $"({Tag} {Triangle0}, {Triangle1})";

        #endregion
    }
}
