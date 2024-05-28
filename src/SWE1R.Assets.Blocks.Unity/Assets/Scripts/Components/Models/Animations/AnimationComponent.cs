// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Unity.Objects;
using System.Collections.Generic;
using UnityEngine;
using Swe1rAnimation = SWE1R.Assets.Blocks.ModelBlock.Animations.Animation;
using Swe1rModelImporter = SWE1R.Assets.Blocks.Unity.ModelImporter;

namespace SWE1R.Assets.Blocks.Unity.Components.Models.Animations
{
    public class AnimationComponent : MonoBehaviour
    {
        #region Fields

        public float float_0f4;
        public float float_0f8;
        public float float_0fc;
        public int bitmask;
        public float float_108;
        public float float_10c;
        public float float_110;
        public int int114;
        public int int118;
        public List<float> keyframeTimestamps;
        public KeyframesOrIntegerObject keyframesOrInteger;
        public TargetOrIntegerObject targetOrInteger;
        public int int_128;

        #endregion

        public void Import(Swe1rAnimation source, Swe1rModelImporter importer)
        {
            gameObject.name = importer.GetName(source);

            float_0f4 = source.Float_0f4;
            float_0f8 = source.Float_0f8;
            float_0fc = source.Float_0fc;
            bitmask = source.Bitmask;
            float_108 = source.Float_108;
            float_10c = source.Float_10c;
            float_110 = source.Float_110;
            int114 = source.Int114;
            int118 = source.Int118;
            keyframeTimestamps = source.KeyframeTimestamps;
            keyframesOrInteger = new KeyframesOrIntegerObject(source.KeyframesOrInteger, importer);
            targetOrInteger = new TargetOrIntegerObject(source.TargetOrInteger, importer);
            int_128 = source.Int_128;
        }

        public Swe1rAnimation Export(ModelExporter exporter)
        {
            var result = new Swe1rAnimation();
            
            result.Float_0f4 = float_0f4;
            result.Float_0f8 = float_0f8;
            result.Float_0fc = float_0f8;
            result.Bitmask = bitmask;
            result.Float_108 = float_108;
            result.Float_10c = float_10c;
            result.Float_110 = float_110;
            result.KeyframeTimestamps = keyframeTimestamps;
            result.KeyframesOrInteger = keyframesOrInteger.Export(exporter);
            result.TargetOrInteger = exporter.GetTargetOrInteger(targetOrInteger);
            result.Int_128 = int_128;

            result.UpdateFramesCount();

            return result;
        }
    }
}
