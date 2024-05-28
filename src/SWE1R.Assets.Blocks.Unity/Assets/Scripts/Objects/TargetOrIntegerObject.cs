// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Unity.Components.Models.Nodes;
using SWE1R.Assets.Blocks.Unity.ScriptableObjects;
using System;
using UnityEngine;
using Swe1rTarget = SWE1R.Assets.Blocks.ModelBlock.Animations.Target;
using Swe1rTargetOrInteger = SWE1R.Assets.Blocks.ModelBlock.Animations.TargetOrInteger;
using Swe1rTransformableD065 = SWE1R.Assets.Blocks.ModelBlock.Nodes.TransformableD065;

namespace SWE1R.Assets.Blocks.Unity.Objects
{
    [Serializable]
    public class TargetOrIntegerObject
    {
        [SerializeReference] public MaterialReferenceObject doubleMaterial;
        [SerializeReference] public MaterialScriptableObject material;
        [SerializeReference] public TransformableD065Component transformableD065;
        public int? integer;

        public TargetOrIntegerObject(Swe1rTargetOrInteger source, ModelImporter modelImporter)
        {
            if (source.Integer.HasValue)
                integer = source.Integer.Value;
            else if (source.Target != null)
            {
                if (source.Target.MaterialReference != null)
                    doubleMaterial = modelImporter.GetMaterialReferenceObject(
                        source.Target.MaterialReference);
                else if (source.Target.Material != null)
                    material = modelImporter.GetMaterialScriptableObject(
                        source.Target.Material);
                else if (source.Target.TransformableD065 != null)
                    transformableD065 = modelImporter.GetFlaggedNodeComponent<TransformableD065Component>(
                        source.Target.TransformableD065);
            }
        }

        public Swe1rTargetOrInteger Export(ModelExporter modelExporter)
        {
            var swe1rTargetOrInteger = new Swe1rTargetOrInteger();
            if (integer.HasValue)
                swe1rTargetOrInteger.Integer = integer.Value;
            else
            {
                var target = swe1rTargetOrInteger.Target = new Swe1rTarget();
                if (doubleMaterial != null)
                    target.MaterialReference = modelExporter.GetMaterialReference(doubleMaterial);
                else if (material != null)
                    target.Material = modelExporter.GetMaterial(material);
                else if (transformableD065 != null)
                    target.TransformableD065 = (Swe1rTransformableD065)modelExporter.GetFlaggedNode(transformableD065.gameObject);
            }
            return swe1rTargetOrInteger;
        }
    }
}
