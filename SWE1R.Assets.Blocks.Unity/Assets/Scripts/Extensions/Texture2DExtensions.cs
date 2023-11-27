// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using UnityEngine;
using Swe1rImageRgba32 = SWE1R.Assets.Blocks.Images.ImageRgba32;
using UnityColor = UnityEngine.Color;

namespace SWE1R.Assets.Blocks.Unity.Extensions
{
    public static class Texture2DExtensions
    {
        public static Texture2D ToUnityTexture2D(this Swe1rImageRgba32 source)
        {
            var result = new Texture2D(source.Width, source.Height);
            for (int x = 0; x < source.Width; x++)
                for (int y = 0; y < source.Height; y++)
                {
                    UnityColor unityColor = source[x, y].ToUnityColor();
                    result.SetPixel(x, y, unityColor);
                }
            result.Apply();
            return result;
        }
    }
}
