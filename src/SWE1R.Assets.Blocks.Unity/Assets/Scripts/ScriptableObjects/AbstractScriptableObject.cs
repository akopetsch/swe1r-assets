// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using UnityEngine;

namespace SWE1R.Assets.Blocks.Unity.ScriptableObjects
{
    public abstract class AbstractScriptableObject<T> : ScriptableObject
    {
        public abstract void Import(T source, ModelImporter importer);
        public abstract T Export(ModelExporter exporter);
    }
}
