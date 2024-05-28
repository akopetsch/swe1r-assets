// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Unity.Components.Models.Nodes;
using System;
using UnityEngine;
using Swe1rMeshGroup3064 = SWE1R.Assets.Blocks.ModelBlock.Nodes.MeshGroup3064;
using Swe1rMeshGroupOrShorts = SWE1R.Assets.Blocks.ModelBlock.Meshes.MeshGroupOrShorts;

namespace SWE1R.Assets.Blocks.Unity.Objects
{
    [Serializable]
    public class MeshGroupOrShortsObject
    {
        [SerializeReference] public MeshGroup3064Component meshGroup3064;
        [SerializeReference] public short[] shorts;

        public MeshGroupOrShortsObject(Swe1rMeshGroupOrShorts source, ModelImporter modelImporter)
        {
            if (source.MeshGroup != null)
                meshGroup3064 = modelImporter.GetFlaggedNodeComponent<MeshGroup3064Component>(source.MeshGroup);
            else if (source.Shorts != null)
                shorts = source.Shorts;
        }

        public Swe1rMeshGroupOrShorts Export(ModelExporter modelExporter)
        {
            var result = new Swe1rMeshGroupOrShorts();
            if (meshGroup3064 != null)
                result.MeshGroup = (Swe1rMeshGroup3064)modelExporter.GetFlaggedNode(meshGroup3064.gameObject);
            else if (shorts.Length > 0)
                result.Shorts = shorts;
            return result;
        }
    }
}
