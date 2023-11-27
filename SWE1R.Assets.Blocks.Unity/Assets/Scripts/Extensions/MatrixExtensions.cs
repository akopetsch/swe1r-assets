// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using UnityEngine;
using Swe1rMatrix3x4Single = SWE1R.Assets.Blocks.Vectors.Matrix3x4Single;
using UnityMatrix4x4 = UnityEngine.Matrix4x4;

namespace SWE1R.Assets.Blocks.Unity.Extensions
{
    public static class MatrixExtensions
    {
        #region Methods (conversion)

        public static UnityMatrix4x4 ToUnity(this Swe1rMatrix3x4Single source)
        {
            var result = UnityMatrix4x4.identity;
            for (int i = 0; i < Swe1rMatrix3x4Single.Height; i++)
                for (int j = 0; j < Swe1rMatrix3x4Single.Width; j++)
                    result[i, j] = source[i, j];
            return result;
        }

        public static Swe1rMatrix3x4Single ToSwe1r(this UnityMatrix4x4 source)
        {
            var result = new Swe1rMatrix3x4Single();
            for (int i = 0; i < Swe1rMatrix3x4Single.Height; i++)
                for (int j = 0; j < Swe1rMatrix3x4Single.Width; j++)
                    result[i, j] = source[i, j];
            return result;
        }

        #endregion

        #region Methods (extraction)

        public static Quaternion ExtractRotation(this UnityMatrix4x4 matrix)
        {
            Vector3 forward;
            forward.x = matrix.m02;
            forward.y = matrix.m12;
            forward.z = matrix.m22;

            Vector3 upwards;
            upwards.x = matrix.m01;
            upwards.y = matrix.m11;
            upwards.z = matrix.m21;

            return Quaternion.LookRotation(forward, upwards);
        }

        public static Vector3 ExtractPosition(this UnityMatrix4x4 matrix)
        {
            Vector3 position;
            position.x = matrix.m03;
            position.y = matrix.m13;
            position.z = matrix.m23;
            return position;
        }

        public static Vector3 ExtractScale(this UnityMatrix4x4 matrix)
        {
            Vector3 scale;
            scale.x = new Vector4(matrix.m00, matrix.m10, matrix.m20, matrix.m30).magnitude;
            scale.y = new Vector4(matrix.m01, matrix.m11, matrix.m21, matrix.m31).magnitude;
            scale.z = new Vector4(matrix.m02, matrix.m12, matrix.m22, matrix.m32).magnitude;
            return scale;
        }

        #endregion
    }
}
