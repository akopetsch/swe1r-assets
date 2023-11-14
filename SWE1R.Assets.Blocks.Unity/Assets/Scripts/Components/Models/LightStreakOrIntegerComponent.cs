// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Unity.Extensions;
using UnityEngine;
using Swe1rLightStreak = SWE1R.Assets.Blocks.ModelBlock.LightStreak;
using Swe1rLightStreakOrInteger = SWE1R.Assets.Blocks.ModelBlock.LightStreakOrInteger;

namespace SWE1R.Assets.Blocks.Unity.Components.Models
{
    public class LightStreakOrIntegerComponent : MonoBehaviour
    {
        public SerializableNullable<int> integer;

        public void Import(Swe1rLightStreakOrInteger source, ModelImporter importer)
        {
            gameObject.name = importer.GetName(source);

            if (source.LightStreak != null)
                transform.Translate(source.LightStreak.Vector.ToUnityVector3());
            else
                integer = source.Integer.Value;
        }

        public Swe1rLightStreakOrInteger Export(ModelExporter exporter)
        {
            var result = new Swe1rLightStreakOrInteger();
            if (integer.HasValue)
                result.Integer = integer.Value;
            else
                result.LightStreak = new Swe1rLightStreak(transform.localPosition.ToSwe1rVector3Single());
            return result;
        }
    }
}
