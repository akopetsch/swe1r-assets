// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices
{
    public class IndicesChunk01 : IndicesChunk
    {
        #region Properties (serialized)

        [Order(0)] public short Length { get; set; }
        [Order(1)] public byte MaxIndex { get; set; }
        [Order(2)] public ReferenceByIndex<Vertex> StartVertex { get; set; }

        #endregion

        #region Properties (abstraction)

        public override IEnumerable<int> Indices => Enumerable.Empty<int>();

        #endregion

        #region Constructor

        public IndicesChunk01() => 
            Tag = 1;

        #endregion

        #region Methods (: object)

        public override string ToString() =>
            $"({nameof(Tag)} = {GetByteString(Tag)}, " +
            $"{nameof(MaxIndex)} = {MaxIndex}, " +
            $"{nameof(StartVertex)} = {StartVertex})";

        #endregion
    }
}
