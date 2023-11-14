﻿// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using ByteSerialization.Attributes;

namespace SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices
{
    [Sizeof(8)]
    public abstract class IndicesChunk
    {
        #region Properties (serialized)

        [RecordTypeIdentifier((byte)01, typeof(IndicesChunk01))]
        [RecordTypeIdentifier((byte)03, typeof(IndicesChunk03))]
        [RecordTypeIdentifier((byte)05, typeof(IndicesChunk05))]
        [RecordTypeIdentifier((byte)06, typeof(IndicesChunk06))]
        [Order(0)] public byte Tag { get; set; }

        #endregion
    }
}
