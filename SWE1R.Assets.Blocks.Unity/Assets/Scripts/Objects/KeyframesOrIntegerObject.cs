// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;
using Swe1rKeyframesOrInteger = SWE1R.Assets.Blocks.ModelBlock.Animations.KeyframesOrInteger;

namespace SWE1R.Assets.Blocks.Unity.Objects
{
    [Serializable]
    public class KeyframesOrIntegerObject
    {
        public KeyframesObject keyframes;
        public SerializableNullable<int> integer;

        public KeyframesOrIntegerObject(Swe1rKeyframesOrInteger source, ModelImporter importer)
        {
            if (source.Keyframes != null)
                keyframes = new KeyframesObject(source.Keyframes, importer);
            else
                integer = source.Integer.Value;
        }

        public Swe1rKeyframesOrInteger Export(ModelExporter exporter)
        {
            var result = new Swe1rKeyframesOrInteger();
            if (integer.HasValue)
                result.Integer = integer.Value;
            else
                result.Keyframes = keyframes.Export(exporter);
            return result;
        }
    }
}
