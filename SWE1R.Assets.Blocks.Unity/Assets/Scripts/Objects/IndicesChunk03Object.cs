// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;
using System.Collections.Generic;
using Swe1rIndicesChunk = SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices.IndicesChunk;
using Swe1rIndicesChunk03 = SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices.IndicesChunk03;
using Swe1rMesh = SWE1R.Assets.Blocks.ModelBlock.Meshes.Mesh;

namespace SWE1R.Assets.Blocks.Unity.Objects
{
    [Serializable]
    public class IndicesChunk03Object : IndicesChunkObject<Swe1rIndicesChunk03>
    {
        public byte maxIndex;

        public override IEnumerable<int> Indices { get { yield return maxIndex; } }

        public override void Import(Swe1rIndicesChunk03 source, ModelImporter modelImporter) =>
            maxIndex = source.MaxIndex;

        public override Swe1rIndicesChunk Export(ModelExporter modelExporter, Swe1rMesh swe1rMesh) =>
            new Swe1rIndicesChunk03() {
                MaxIndex = maxIndex,
            };
    }
}
