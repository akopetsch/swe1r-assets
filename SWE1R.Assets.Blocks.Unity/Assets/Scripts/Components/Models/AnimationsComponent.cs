// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Unity.Components.Models.Animations;
using SWE1R.Assets.Blocks.Unity.Extensions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Swe1rAnimation = SWE1R.Assets.Blocks.ModelBlock.Animations.Animation;
using Swe1rModel = SWE1R.Assets.Blocks.ModelBlock.Model;

namespace SWE1R.Assets.Blocks.Unity.Components.Models
{
    public class AnimationsComponent : MonoBehaviour
    {
        public void Import(List<Swe1rAnimation> source, ModelImporter importer)
        {
            gameObject.name = nameof(Swe1rModel.Animations);

            foreach (Swe1rAnimation animation in source)
                gameObject.AddChild().AddComponent<AnimationComponent>().Import(animation, importer);
        }

        public List<Swe1rAnimation> Export(ModelExporter exporter) =>
            gameObject.GetComponentsInChildren<AnimationComponent>()
                .Select(ac => ac.Export(exporter)).ToList();
    }
}
