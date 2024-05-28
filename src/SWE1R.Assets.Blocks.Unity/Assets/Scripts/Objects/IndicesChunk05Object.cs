// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;
using System.Collections.Generic;
using Swe1rIndicesChunk = SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices.IndicesChunk;
using Swe1rIndicesChunk05 = SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices.IndicesChunk05;
using Swe1rMesh = SWE1R.Assets.Blocks.ModelBlock.Meshes.Mesh;

namespace SWE1R.Assets.Blocks.Unity.Objects
{
    [Serializable]
    public class IndicesChunk05Object : IndicesChunkObject<Swe1rIndicesChunk05>
    {
        public byte index0;
        public byte index1;
        public byte index2;

        public override IEnumerable<int> Indices
        {
            get
            {
                yield return index0;
                yield return index1;
                yield return index2;
            }
        }

        public override void Import(Swe1rIndicesChunk05 source, ModelImporter modelImporter)
        {
            index0 = source.Index0;
            index1 = source.Index1;
            index2 = source.Index2;
        }

        public override Swe1rIndicesChunk Export(ModelExporter modelExporter, Swe1rMesh swe1rMesh) =>
            new Swe1rIndicesChunk05() {
                Index0 = index0,
                Index1 = index1,
                Index2 = index2,
            };
    }
}
