// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Unity.Extensions;
using System.Collections.Generic;
using UnityEngine;
using Swe1rModel = SWE1R.Assets.Blocks.ModelBlock.Model;
using Swe1rHeaderData = SWE1R.Assets.Blocks.ModelBlock.HeaderData;
using Swe1rLightStreakOrInteger = SWE1R.Assets.Blocks.ModelBlock.LightStreakOrInteger;

namespace SWE1R.Assets.Blocks.Unity.Components.Models
{
    public class DataComponent : MonoBehaviour
    {
        public void Import(Swe1rHeaderData source, ModelImporter importer)
        {
            gameObject.name = nameof(Swe1rModel.Data);

            foreach (Swe1rLightStreakOrInteger lightStreakOrInteger in source.List)
                gameObject.AddChild().AddComponent<LightStreakOrIntegerComponent>().Import(lightStreakOrInteger, importer);
        }

        public Swe1rHeaderData Export(ModelExporter exporter)
        {
            var headerData = new Swe1rHeaderData();

            headerData.List = new List<Swe1rLightStreakOrInteger>(); // TODO: auto creation
            foreach (GameObject childGameObject in gameObject.GetChildren())
                headerData.List.Add(childGameObject.GetComponent<LightStreakOrIntegerComponent>().Export(exporter));

            headerData.UpdateSize();

            return headerData;
        }
    }
}
