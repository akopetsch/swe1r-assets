// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;
using System.Collections.Generic;
using Swe1rIndicesChunk = SWE1R.Assets.Blocks.ModelBlock.Meshes.VertexIndices.IndicesChunk;
using Swe1rMesh = SWE1R.Assets.Blocks.ModelBlock.Meshes.Mesh;

namespace SWE1R.Assets.Blocks.Unity.Objects
{
    [Serializable]
    public abstract class IndicesChunkObject
    {
        public abstract void Import(Swe1rIndicesChunk source, ModelImporter modelImporter);
        public abstract Swe1rIndicesChunk Export(ModelExporter modelExporter, Swe1rMesh swe1rMesh);

        public abstract IEnumerable<int> Indices { get; }
    }

    [Serializable]
    public abstract class IndicesChunkObject<T> : IndicesChunkObject where T : Swe1rIndicesChunk
    {
        public override void Import(Swe1rIndicesChunk source, ModelImporter modelImporter) => 
            Import((T)source, modelImporter);

        public abstract void Import(T source, ModelImporter modelImporter);
    }
}
