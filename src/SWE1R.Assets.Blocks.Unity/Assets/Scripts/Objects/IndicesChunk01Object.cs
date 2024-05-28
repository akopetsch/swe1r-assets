// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;
using System.Collections.Generic;
using System.Linq;
using Swe1rIndicesChunk = SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices.IndicesChunk;
using Swe1rIndicesChunk01 = SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices.IndicesChunk01;
using Swe1rMesh = SWE1R.Assets.Blocks.ModelBlock.Meshes.Mesh;
using Swe1rReferenceByIndexVertex = SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices
    .ReferenceByIndex<SWE1R.Assets.Blocks.ModelBlock.Meshes.Vertex>;

namespace SWE1R.Assets.Blocks.Unity.Objects
{
    [Serializable]
    public class IndicesChunk01Object : IndicesChunkObject<Swe1rIndicesChunk01>
    {
        public short length;
        public byte nextIndicesBase;
        public int startVertexIndex;

        public override IEnumerable<int> Indices => Enumerable.Empty<int>();

        public override void Import(Swe1rIndicesChunk01 source, ModelImporter modelImporter)
        {
            length = source.Length;
            nextIndicesBase = source.NextIndicesBase;
            startVertexIndex = source.StartVertex.Index.Value;
        }

        public override Swe1rIndicesChunk Export(ModelExporter modelExporter, Swe1rMesh swe1rMesh)
        {
            var result = new Swe1rIndicesChunk01() {
                Length = length,
                NextIndicesBase = nextIndicesBase,
            };
            result.StartVertex = new Swe1rReferenceByIndexVertex() {
                Collection = swe1rMesh.VisibleVertices,
                Index = startVertexIndex,
            };
            result.StartVertex.UpdateReference();
            return result;
        }
    }
}
