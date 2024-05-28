// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;
using System.Collections.Generic;
using Swe1rIndicesChunk = SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices.IndicesChunk;
using Swe1rIndicesChunk06 = SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices.IndicesChunk06;
using Swe1rMesh = SWE1R.Assets.Blocks.ModelBlock.Meshes.Mesh;

namespace SWE1R.Assets.Blocks.Unity.Objects
{
    [Serializable]
    public class IndicesChunk06Object : IndicesChunkObject<Swe1rIndicesChunk06>
    {
        public byte index0;
        public byte index1;
        public byte index2;

        public byte index3;
        public byte index4;
        public byte index5;

        public override IEnumerable<int> Indices
        {
            get
            {
                yield return index0;
                yield return index1;
                yield return index2;

                yield return index3;
                yield return index4;
                yield return index5;
            }
        }

        public override void Import(Swe1rIndicesChunk06 source, ModelImporter modelImporter)
        {
            index0 = source.Index0;
            index1 = source.Index1;
            index2 = source.Index2;

            index3 = source.Index3;
            index4 = source.Index4;
            index5 = source.Index5;
        }

        public override Swe1rIndicesChunk Export(ModelExporter modelExporter, Swe1rMesh swe1rMesh) =>
            new Swe1rIndicesChunk06() {
                Index0 = index0,
                Index1 = index1,
                Index2 = index2,

                Index3 = index3,
                Index4 = index4,
                Index5 = index5,
            };
    }
}
