// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Unity.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using Swe1rKeyframes = SWE1R.Assets.Blocks.ModelBlock.Animations.Keyframes;

namespace SWE1R.Assets.Blocks.Unity.Objects
{
    [Serializable]
    public class KeyframesObject
    {
        public List<MaterialTextureScriptableObject> materialTextures;
        public List<float> floats;

        public KeyframesObject(Swe1rKeyframes source, ModelImporter importer)
        {
            materialTextures = source.MaterialTextures?
                        .Select(mt => importer.GetMaterialTextureScriptableObject(mt)).ToList();
            floats = source.Floats;
        }

        public Swe1rKeyframes Export(ModelExporter exporter)
        {
            var result = new Swe1rKeyframes();
            if (materialTextures?.Count > 0)
                result.MaterialTextures = materialTextures.Select(mt => exporter.GetMaterialTexture(mt)).ToList();
            else
                result.Floats = floats;
            return result;
        }
    }
}
