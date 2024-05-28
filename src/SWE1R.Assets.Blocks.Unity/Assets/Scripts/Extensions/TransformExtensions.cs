// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using UnityEngine;

namespace SWE1R.Assets.Blocks.Unity.Extensions
{
    public static  class TransformExtensions
    {
        public static void FromMatrix(this Transform transform, Matrix4x4 matrix)
        {
            transform.localScale = matrix.ExtractScale();
            transform.localRotation = matrix.ExtractRotation();
            transform.localPosition = matrix.ExtractPosition();
        }
    }
}
