// SPDX-License-Identifier: MIT

using ByteSerialization.Attributes;
using SWE1R.Assets.Blocks.ModelBlock.Meshes.Geometry;
using System.Collections.Generic;
using System.Linq;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices
{
    public class IndicesChunk01 : IndicesChunk
    {
        #region Properties (serialized)

        [Order(0)]
        public short Length { get; set; } // n_packed
        [Order(1)]
        public byte NextIndicesBase { get; set; } // v0_plus_n
        [Order(2)]
        public ReferenceByIndex<Vertex> StartVertex { get; set; }

        #endregion

        #region Properties (abstraction)

        public override IEnumerable<int> Indices => Enumerable.Empty<int>();

        public override IEnumerable<Triangle> Triangles => Enumerable.Empty<Triangle>();

        #endregion

        #region Constructor

        public IndicesChunk01() => 
            Tag = 1;

        #endregion

        #region Methods (: object)

        public override string ToString() =>
            $"({Tag} " +
            $"{nameof(Length)} = {Length}, " +
            $"{nameof(NextIndicesBase)} = {NextIndicesBase}, " +
            $"{nameof(StartVertex)} = {StartVertex})";

        #endregion
    }
}
