// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Unity.ScriptableObjects;
using System;
using UnityEngine;
using Swe1rDoubleMaterial = SWE1R.Assets.Blocks.ModelBlock.Animations.MaterialReference;

namespace SWE1R.Assets.Blocks.Unity.Objects
{
    [Serializable]
    public class MaterialReferenceObject
    {
        [SerializeReference] public MaterialScriptableObject material;

        public MaterialReferenceObject(Swe1rDoubleMaterial source, ModelImporter importer) =>
            material = importer.GetMaterialScriptableObject(source.Material);

        public Swe1rDoubleMaterial Export(ModelExporter exporter) =>
            new Swe1rDoubleMaterial() {
                Material = exporter.GetMaterial(material)
            };
    }
}
