// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.ModelBlock;
using System;
using System.Collections.Generic;

namespace SWE1R.Assets.Blocks.Unity.Components.Models.Types
{
    public class HeaderComponentFactory
    {
        public static HeaderComponentFactory Instance { get; } = new HeaderComponentFactory();
        private HeaderComponentFactory() { }

        private readonly
            Dictionary<ModelType, Type> componentTypeByModelType =
            new Dictionary<ModelType, Type>()
        {
            { ModelType.MAlt, typeof(MAltHeaderComponent) },
            { ModelType.Modl, typeof(ModlHeaderComponent) },
            { ModelType.Part, typeof(PartHeaderComponent) },
            { ModelType.Podd, typeof(PoddHeaderComponent) },
            { ModelType.Pupp, typeof(PuppHeaderComponent) },
            { ModelType.Scen, typeof(ScenHeaderComponent) },
            { ModelType.Trak, typeof(TrakHeaderComponent) },
        };

        public Type GetComponentType(Header header) =>
            componentTypeByModelType[header.Type];
    }
}
