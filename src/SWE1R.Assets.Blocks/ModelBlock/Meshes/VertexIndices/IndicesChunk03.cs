// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using ByteSerialization.Components.Values.Customs;
using ByteSerialization.IO;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.Geometry;
using System.Collections.Generic;
using System.Linq;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices
{
    public class IndicesChunk03 : IndicesChunk, ICustomSerializable
    {
        #region Fields

        private static readonly byte[] padding = new byte[6];

        #endregion

        #region Properties

        [Order(0), Offset(7)]
        public byte MaxIndex { get; set; }

        #endregion

        #region Properties (abstraction)

        public override IEnumerable<int> Indices => Enumerable.Empty<int>();

        public override IEnumerable<Triangle> Triangles => Enumerable.Empty<Triangle>();

        #endregion

        #region Constructor

        public IndicesChunk03() => 
            Tag = 3;

        #endregion

        #region Methods (: ICustomSerializable)

        public void Serialize(CustomComponent customComponent)
        {
            EndianBinaryWriter w = customComponent.Writer;

            w.Write(Tag);
            w.Write(padding);
            w.Write(MaxIndex);
        }

        public void Deserialize(CustomComponent customComponent)
        {
            // TODO: not called

            EndianBinaryReader r = customComponent.Reader;

            Tag = r.ReadByte();
            r.ReadBytes(padding.Length);
            MaxIndex = r.ReadByte();
        }

        #endregion

        #region Methods (: object)

        public override string ToString() =>
            $"({Tag} {nameof(MaxIndex)} = {MaxIndex})";

        #endregion
    }
}
