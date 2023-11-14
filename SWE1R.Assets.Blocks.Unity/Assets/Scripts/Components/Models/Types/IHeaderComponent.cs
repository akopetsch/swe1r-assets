// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.ModelBlock;
using UnityEngine;

namespace SWE1R.Assets.Blocks.Unity.Components.Models.Types
{
    public interface IHeaderComponent
    {
        GameObject gameObject { get; }

        void Import(Header header, ModelImporter modelImporter);
        Header Export(ModelExporter modelExporter);
    }
}
