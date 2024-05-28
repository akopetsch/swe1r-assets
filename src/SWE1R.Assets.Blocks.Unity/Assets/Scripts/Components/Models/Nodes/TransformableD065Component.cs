// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Unity.Extensions;
using Swe1rFlaggedNode = SWE1R.Assets.Blocks.ModelBlock.Nodes.FlaggedNode;
using Swe1rTransformableD065 = SWE1R.Assets.Blocks.ModelBlock.Nodes.TransformableD065;
using UnityMatrix4x4 = UnityEngine.Matrix4x4;
using UnityVector3 = UnityEngine.Vector3;

namespace SWE1R.Assets.Blocks.Unity.Components.Models.Nodes
{
    public class TransformableD065Component : FlaggedNodeComponent<Swe1rTransformableD065>
    {
        public UnityMatrix4x4 matrix;
        public UnityVector3 vector;

        public override void Import(Swe1rTransformableD065 source)
        {
            base.Import(source);
            matrix = source.Matrix.ToUnity();
            vector = source.Vector.ToUnityVector3();
            ApplyMatrix(matrix);
        }

        public override Swe1rFlaggedNode Export(ModelExporter modelExporter)
        {
            var result = (Swe1rTransformableD065)base.Export(modelExporter);
            result.Matrix = matrix.ToSwe1r();
            result.Vector = vector.ToSwe1rVector3Single();
            return result;
        }
    }
}
