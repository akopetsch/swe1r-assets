// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using UnityEngine;
using Swe1rModel = SWE1R.Assets.Blocks.ModelBlock.Model;

namespace SWE1R.Assets.Blocks.Unity.Components.Models.Types
{
    public interface IModelComponent
    {
        GameObject gameObject { get; }

        void Import(Swe1rModel model, ModelImporter modelImporter);
        Swe1rModel Export(ModelExporter modelExporter);
    }
}
