// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.ModelBlock;
using System;
using System.Collections.Generic;
using Swe1rModel = SWE1R.Assets.Blocks.ModelBlock.Model;

namespace SWE1R.Assets.Blocks.Unity.Components.Models.Types
{
    public class ModelComponentFactory
    {
        public static ModelComponentFactory Instance { get; } = new ModelComponentFactory();
        private ModelComponentFactory() { }

        private readonly
            Dictionary<ModelType, Type> componentTypeByModelType =
            new Dictionary<ModelType, Type>()
        {
            { ModelType.MAlt, typeof(MAltModelComponent) },
            { ModelType.Modl, typeof(ModlModelComponent) },
            { ModelType.Part, typeof(PartModelComponent) },
            { ModelType.Podd, typeof(PoddModelComponent) },
            { ModelType.Pupp, typeof(PuppModelComponent) },
            { ModelType.Scen, typeof(ScenModelComponent) },
            { ModelType.Trak, typeof(TrakModelComponent) },
        };

        public Type GetComponentType(Swe1rModel model) =>
            componentTypeByModelType[model.Type];
    }
}
