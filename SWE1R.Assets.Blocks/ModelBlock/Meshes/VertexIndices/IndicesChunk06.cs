// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using ByteSerialization.Components.Values.Customs;
using ByteSerialization.IO;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.Geometry;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices
{
    public class IndicesChunk06 : IndicesChunk, ICustomSerializable
    {
        #region Properties (serialized)

        [Order(0)] public byte Index0 { get; set; }
        [Order(1)] public byte Index1 { get; set; }
        [Order(2)] public byte Index2 { get; set; }

        [Offset(5)]
        [Order(3)] public byte Index3 { get; set; }
        [Order(4)] public byte Index4 { get; set; }
        [Order(5)] public byte Index5 { get; set; }

        #endregion

        #region Properties (helper)

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
    }
}
