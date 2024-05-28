// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using UnityEngine;

namespace SWE1R.Assets.Blocks.Unity
{
    /// <summary>
    /// Computation helper class for BezierSpline.
    /// </summary>
    public class CubicBezier3
    {
        #region Properties

        public Vector3[] Points { get; private set; }
        public int N { get { return Points.Length - 1; } }

        #endregion

        #region Constructor

        public CubicBezier3(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            Points = new Vector3[] { p0, p1, p2, p3 };
        }

        #endregion

        #region Methods (public)

        public Vector3 GetPoint(float t)
        {
            t = Mathf.Clamp01(t);
            float oneMinusT = 1f - t;
            return
                oneMinusT * oneMinusT * oneMinusT * Points[0] +
                3f * oneMinusT * oneMinusT * t * Points[1] +
                3f * oneMinusT * t * t * Points[2] +
                t * t * t * Points[3];
        }

        public Vector3 GetFirstDerivative(float t)
        {
            t = Mathf.Clamp01(t);
            float oneMinusT = 1f - t;
            return
                3f * oneMinusT * oneMinusT * (Points[1] - Points[0]) +
                6f * oneMinusT * t * (Points[2] - Points[1]) +
                3f * t * t * (Points[3] - Points[2]);
        }

        public Vector3 GetSecondDerivative(float t)
        {
            t = Mathf.Clamp01(t);
            float oneMinusT = 1f - t;
            return
                6f * oneMinusT * (Points[2] - 2 * Points[1] + Points[0]) +
                6f * t * (Points[3] - Points[2] + Points[1]);
        }

        public float GetCurvature(float t)
        {
            t = Mathf.Clamp01(t);
            Vector3 d1 = GetFirstDerivative(t);
            Vector3 d2 = GetSecondDerivative(t);
            return Vector3.Cross(d1, d2).magnitude /
                Mathf.Pow(d1.magnitude, 3);
        }

        // Gets the approximate length as average between chord length and polygon length.
        public float GetLength(float t)
        {
            // see: https://www.opengl.org/discussion_boards/showthread.php/172373-3D-Cubic-Bezier-Segment-Length

            CubicBezier3 sub = Subdivide(t);
            float chordLength = (sub.Points[0] - sub.Points[3]).magnitude;
            float polygonLength =
                (sub.Points[0] - sub.Points[1]).magnitude +
                (sub.Points[1] - sub.Points[2]).magnitude +
                (sub.Points[2] - sub.Points[3]).magnitude;
            return (chordLength + polygonLength) / 2;
        }

        #endregion

        #region Methods (private)

        private CubicBezier3 Subdivide(float t)
        {
            Vector3 p0 = DeCasteljau(0, 0, t);
            Vector3 p1 = DeCasteljau(1, 0, t);
            Vector3 p2 = DeCasteljau(2, 0, t);
            Vector3 p3 = DeCasteljau(3, 0, t);
            return new CubicBezier3(p0, p1, p2, p3);
        }

        private Vector3 DeCasteljau(int j, int i, float t)
        {
            if (j == 0)
            {
                return Points[i];
            }
            return
                (1f - t) * DeCasteljau(j - 1, i, t) +
                       t * DeCasteljau(j - 1, i + 1, t);
        }

        #endregion
    }
}
