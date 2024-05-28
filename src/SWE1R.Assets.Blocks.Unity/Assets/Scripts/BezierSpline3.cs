// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using System;
using UnityEngine;

namespace SWE1R.Assets.Blocks.Unity
{
    public class BezierSpline3 : MonoBehaviour
    {
        #region Enums

        public enum BezierControlPointMode
        {
            Free,
            Aligned,
            Mirrored
        }

        #endregion

        #region Fields

        [SerializeField]
        private BezierControlPointMode[] modes;

        [SerializeField]
        private Vector3[] points;

        [SerializeField]
        private bool is3d = true;

        #endregion

        #region Properties

        public bool Is3d
        {
            get
            {
                return is3d;
            }
            set
            {
                bool isNewValue = value != is3d;
                bool is2d = !value;

                is3d = value;

                if (isNewValue && is2d)
                {
                    for (int i = 0; i < points.Length; i++)
                    {
                        Vector3 p = points[i];
                        points[i] = new Vector3(p.x, 0, p.z); // TODO: use extension method
                    }
                }
            }
        }

        public int CurveCount
        {
            get
            {
                return (points.Length - 1) / 3;
            }
        }

        public int ControlPointCount
        {
            get
            {
                return points.Length;
            }
        }

        #endregion

        #region Methods (public)

        public void Reset()
        {
            points = new Vector3[] {
                new Vector3(10f, 0f, 0f),
                new Vector3(20f, 0f, 0f),
                new Vector3(30f, 0f, 0f),
                new Vector3(40f, 0f, 0f),
            };
                modes = new BezierControlPointMode[] {
                BezierControlPointMode.Aligned,
                BezierControlPointMode.Aligned
            };
        }

        public Vector3 GetControlPoint(int index)
        {
            return points[index];
        }

        public void SetControlPoint(int index, Vector3 point)
        {
            if (index % 3 == 0)
            {
                Vector3 delta = point - points[index];
                if (index > 0)
                {
                    points[index - 1] += delta;
                }
                if (index + 1 < points.Length)
                {
                    points[index + 1] += delta;
                }
            }
            points[index] = point;
            EnforceMode(index);
        }

        public BezierControlPointMode GetControlPointMode(int index)
        {
            return modes[(index + 1) / 3];
        }

        public void SetControlPointMode(int index, BezierControlPointMode mode)
        {
            modes[(index + 1) / 3] = mode;
            EnforceMode(index);
        }

        public Vector3 GetPoint(float t)
        {
            int i = GetCurvePointIndex(t);
            var curve = new CubicBezier3(points[i], points[i + 1], points[i + 2], points[i + 3]);
            return curve.GetPoint(GetCurveT(t));
        }

        public Vector3 GetVelocity(float t)
        {
            int i = GetCurvePointIndex(t);
            var curve = new CubicBezier3(points[i], points[i + 1], points[i + 2], points[i + 3]);
            return curve.GetFirstDerivative(GetCurveT(t));
        }

        public Vector3 GetDirection(float t)
        {
            return GetVelocity(t).normalized;
        }

        public void AddCurve()
        {
            Vector3 point = points[points.Length - 1];

            Array.Resize(ref points, points.Length + 3);

            point.x += 10f;
            points[points.Length - 3] = point;

            point.x += 10f;
            points[points.Length - 2] = point;

            point.x += 10f;
            points[points.Length - 1] = point;

            // increase modes length by 1
            Array.Resize(ref modes, modes.Length + 1);
            // new last mode is old last mode
            modes[modes.Length - 1] = modes[modes.Length - 2];
            EnforceMode(points.Length - 4);
        }

        // Gets the approximate length as sum of averages between chord lengths and polygon lengths.
        public float GetLength(float t = 1f)
        {
            float length = 0;
            int lastIndex = GetCurvePointIndex(t);
            for (int i = 0; i <= lastIndex; i += 3)
            {
                var curve = new CubicBezier3(points[i], points[i + 1], points[i + 2], points[i + 3]);
                length += curve.GetLength(GetCurveT(t));
            }
            return length;
        }

        #endregion

        #region Methods (private)

        private void EnforceMode(int index)
        {
            int modeIndex = (index + 1) / 3;
            BezierControlPointMode mode = modes[modeIndex];
            // do not enforce for free points or points at start or end
            if (mode == BezierControlPointMode.Free || modeIndex == 0 || modeIndex == modes.Length - 1)
            {
                return;
            }

            int middleIndex = modeIndex * 3;
            int fixedIndex, enforcedIndex;
            // if middle point or point before selected
            if (index <= middleIndex)
            {
                // keep previous point fixed
                fixedIndex = middleIndex - 1;
                // enforce on point after middle point
                enforcedIndex = middleIndex + 1;
            }
            else // else if point after selected
            {
                // keep selected point fixed
                fixedIndex = middleIndex + 1;
                // enforce on point before middle point
                enforcedIndex = middleIndex - 1;
            }

            Vector3 middle = points[middleIndex];
            Vector3 enforcedTangent = middle - points[fixedIndex];
            // For the aligned mode, we also have to make sure that the new tangent 
            // has the same length as the old one. So we normalize it and then multiply 
            // by the distance between the middle and the old enforced point.
            if (mode == BezierControlPointMode.Aligned)
            {
                enforcedTangent = enforcedTangent.normalized * Vector3.Distance(middle, points[enforcedIndex]);
            }
            points[enforcedIndex] = middle + enforcedTangent;
        }

        private float GetCurveT(float t)
        {
            if (t >= 1f)
            {
                return 1f;
            }
            return Mathf.Clamp01(t) * CurveCount - GetCurveIndex(t);
        }

        private int GetCurvePointIndex(float t)
        {
            if (t >= 1f)
            {
                return points.Length - 4;
            }
            return GetCurveIndex(t) * 3;
        }

        private int GetCurveIndex(float t)
        {
            return (int)(Mathf.Clamp01(t) * CurveCount);
        }

        #endregion
    }
}
