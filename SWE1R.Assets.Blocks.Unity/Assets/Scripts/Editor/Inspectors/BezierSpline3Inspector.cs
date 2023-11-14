// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using UnityEditor;
using UnityEngine;

namespace SWE1R.Assets.Blocks.Unity.Editor.Inspectors
{
    [CustomEditor(typeof(BezierSpline3))]
    public class BezierSplineInspector : UnityEditor.Editor
    {
        private const int stepsPerCurve = 10;
        private const float directionScale = 0.5f;
        private const float handleSize = 0.04f;
        private const float pickSize = 0.06f;
        private BezierSpline3 spline;
        private Transform handleTransform;
        private Quaternion handleRotation;
        private int selectedIndex = -1;

        private static Color[] modeColors = {
            Color.white,    // free
            Color.yellow,   // aligned
            Color.cyan      // mirrored
        };

        public override void OnInspectorGUI()
        {
            spline = target as BezierSpline3;

            // selected point
            if (selectedIndex >= 0 && selectedIndex < spline.ControlPointCount)
            {
                DrawSelectedPointInspector();
            }

            // button: add curve
            if (GUILayout.Button("Add Curve"))
            {
                Undo.RecordObject(spline, "Add Curve");
                spline.AddCurve();
                EditorUtility.SetDirty(spline);
            }

            // toggle: is 3D?
            bool is3d = EditorGUILayout.Toggle("3D", spline.Is3d);
            if (spline.Is3d != is3d) // change?
            {
                Undo.RecordObject(spline, "Switch 2D/3D");
                spline.Is3d = is3d;
            }
        }

        private void DrawSelectedPointInspector()
        {
            GUILayout.Label("Selected Point");
            EditorGUI.BeginChangeCheck();
            Vector3 point = EditorGUILayout.Vector3Field("Position", spline.GetControlPoint(selectedIndex));
            if (spline.Is3d)
            {
                point = new Vector3(point.x, 0, point.z); // TODO: use extension method
            }
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(spline, "Move Point");
                EditorUtility.SetDirty(spline);
                spline.SetControlPoint(selectedIndex, point);
            }
            EditorGUI.BeginChangeCheck();
            BezierSpline3.BezierControlPointMode mode = (BezierSpline3.BezierControlPointMode)
                EditorGUILayout.EnumPopup("Mode", spline.GetControlPointMode(selectedIndex));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(spline, "Change Point Mode");
                spline.SetControlPointMode(selectedIndex, mode);
                EditorUtility.SetDirty(spline);
            }
        }

        private void OnSceneGUI()
        {
            spline = target as BezierSpline3;
            handleTransform = spline.transform;
            handleRotation = Tools.pivotRotation == PivotRotation.Local ?
                handleTransform.rotation : Quaternion.identity;

            Vector3 p0 = ShowPoint(0);
            for (int i = 1; i < spline.ControlPointCount; i += 3)
            {
                Vector3 p1 = ShowPoint(i);
                Vector3 p2 = ShowPoint(i + 1);
                Vector3 p3 = ShowPoint(i + 2);

                Handles.color = Color.gray;
                Handles.DrawLine(p0, p1);
                Handles.DrawLine(p2, p3);

                Handles.DrawBezier(p0, p3, p1, p2, Color.white, null, 2f);
                p0 = p3;
            }
            ShowDirections();
        }

        private void ShowDirections()
        {
            Handles.color = Color.green;
            Vector3 point = spline.GetPoint(0f);
            Handles.DrawLine(
                handleTransform.TransformPoint(point),
                handleTransform.TransformPoint(point + spline.GetDirection(0f) * directionScale));
            int steps = stepsPerCurve * spline.CurveCount;
            for (int i = 1; i <= steps; i++)
            {
                point = spline.GetPoint(i / (float)steps);
                Handles.DrawLine(
                    handleTransform.TransformPoint(point),
                    handleTransform.TransformPoint(point + spline.GetDirection(i / (float)steps) * directionScale));
            }
        }

        private Vector3 ShowPoint(int index)
        {
            Vector3 point = handleTransform.TransformPoint(spline.GetControlPoint(index));
            float size = HandleUtility.GetHandleSize(point);
            Handles.color = modeColors[(int)spline.GetControlPointMode(index)];
            if (Handles.Button(point, handleRotation, size * handleSize, size * pickSize, Handles.DotHandleCap))
            {
                selectedIndex = index;
                Repaint();
            }
            if (selectedIndex == index)
            {
                EditorGUI.BeginChangeCheck();
                point = Handles.DoPositionHandle(point, handleRotation);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(spline, "Move Point");
                    EditorUtility.SetDirty(spline);
                    Vector3 newPoint = handleTransform.InverseTransformPoint(point);
                    if (!spline.Is3d)
                    {
                        newPoint = new Vector3(newPoint.x, 0, newPoint.z);
                    }
                    spline.SetControlPoint(index, newPoint);
                }
            }
            return point;
        }
    }
}
