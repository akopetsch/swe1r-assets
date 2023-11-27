// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Unity.Extensions;
using Swe1rBounds3Single = SWE1R.Assets.Blocks.Vectors.Bounds3Single;
using Swe1rFlaggedNode = SWE1R.Assets.Blocks.ModelBlock.Nodes.FlaggedNode;
using Swe1rMeshGroup3064 = SWE1R.Assets.Blocks.ModelBlock.Nodes.MeshGroup3064;
using UnityVector3 = UnityEngine.Vector3;

namespace SWE1R.Assets.Blocks.Unity.Components.Models.Nodes
{
    public class MeshGroup3064Component : FlaggedNodeComponent<Swe1rMeshGroup3064>
    {
        public UnityVector3 boundsMin;
        public UnityVector3 boundsMax;

        public override void Import(Swe1rMeshGroup3064 source)
        {
            base.Import(source);
            boundsMin = source.Bounds.Min.ToUnityVector3();
            boundsMax = source.Bounds.Max.ToUnityVector3();
        }

        public override Swe1rFlaggedNode Export(ModelExporter modelExporter)
        {
            var result = (Swe1rMeshGroup3064)base.Export(modelExporter);
            result.Bounds = new Swe1rBounds3Single() {
                Min = boundsMin.ToSwe1rVector3Single(),
                Max = boundsMax.ToSwe1rVector3Single(),
            };
            return result;
        }
    }
}
