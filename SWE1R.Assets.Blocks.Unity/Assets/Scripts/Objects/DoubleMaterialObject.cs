// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Unity.ScriptableObjects;
using System;
using UnityEngine;
using Swe1rDoubleMaterial = SWE1R.Assets.Blocks.ModelBlock.Animations.DoubleMaterial;

namespace SWE1R.Assets.Blocks.Unity.Objects
{
    [Serializable]
    public class DoubleMaterialObject
    {
        [SerializeReference] public MaterialScriptableObject foo1;
        [SerializeReference] public MaterialScriptableObject foo2;

        public DoubleMaterialObject(Swe1rDoubleMaterial source, ModelImporter importer)
        {
            foo1 = importer.GetMaterialScriptableObject(source.Foo1);
            foo2 = importer.GetMaterialScriptableObject(source.Foo2);
        }

        public Swe1rDoubleMaterial Export(ModelExporter exporter)
        {
            var result = new Swe1rDoubleMaterial();
            result.Foo1 = exporter.GetMaterial(foo1);
            result.Foo2 = exporter.GetMaterial(foo2);
            return result;
        }
    }
}
