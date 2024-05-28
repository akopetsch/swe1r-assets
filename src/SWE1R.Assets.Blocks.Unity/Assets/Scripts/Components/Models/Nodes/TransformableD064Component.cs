// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Unity.Extensions;
using Swe1rFlaggedNode = SWE1R.Assets.Blocks.ModelBlock.Nodes.FlaggedNode;
using Swe1rTransformableD064 = SWE1R.Assets.Blocks.ModelBlock.Nodes.TransformableD064;
using UnityMatrix4x4 = UnityEngine.Matrix4x4;

namespace SWE1R.Assets.Blocks.Unity.Components.Models.Nodes
{
    public class TransformableD064Component : FlaggedNodeComponent<Swe1rTransformableD064>
    {
        public UnityMatrix4x4 matrix;

        public override void Import(Swe1rTransformableD064 source)
        {
            base.Import(source);
            matrix = source.Matrix.ToUnity();
            ApplyMatrix(matrix);
        }

        public override Swe1rFlaggedNode Export(ModelExporter modelExporter)
        {
            var result = (Swe1rTransformableD064)base.Export(modelExporter);
            result.Matrix = matrix.ToSwe1r();
            return result;
        }
    }
}
