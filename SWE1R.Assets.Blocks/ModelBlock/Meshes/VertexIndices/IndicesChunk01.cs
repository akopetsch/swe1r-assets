// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices
{
    public class IndicesChunk01 : IndicesChunk
    {
        #region Properties

        [Order(0)] public short Length { get; set; }
        [Order(1)] public byte MaxIndex { get; set; }
        [Order(2)] public ReferenceByIndex<Vertex> StartVertex { get; set; }

        #endregion

        #region Constructor

        public IndicesChunk01() => 
            Tag = 1;

        #endregion
    }
}
