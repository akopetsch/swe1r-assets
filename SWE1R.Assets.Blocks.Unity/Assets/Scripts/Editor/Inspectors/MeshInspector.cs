// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using SWE1R.Assets.Blocks.Unity.Components.Models.Meshes;
using SWE1R.Assets.Blocks.Unity.Objects;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SWE1R.Assets.Blocks.Unity.Editor.Inspectors
{
    [CustomEditor(typeof(MeshComponent))]
    public  class MeshInspector : UnityEditor.Editor
    {
        private MeshComponent Mesh { get; set; }
        private GUIStyle Style { get; set; }
        private LabeledVectors LabeledVertices { get; set; }
        private LabeledVectors LabeledCollision { get; set; }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Mesh = (MeshComponent)target;
            Style = new GUIStyle();
            LabeledVertices = GetLabeledVectors(Mesh.vertices.Select(v => (Vector3)v.position).ToList(), Color.red, Color.magenta);
            if (Mesh.collisionVertices != null)
                LabeledCollision = GetLabeledVectors(Mesh.collisionVertices.shortVectors, Color.cyan, Color.blue);

            if (GUILayout.Button("Load OBJ File"))
            {
                // TODO: implement loading obj file
            }
        }
        private LabeledVectors GetLabeledVectors(List<Vector3> vectors, Color vectorColor, Color labelColor)
        {
            var lv = new LabeledVectors();
            Transform t = Mesh.transform;
            int i = 0;
            if (vectors != null)
                foreach (Vector3 v in vectors)
                    lv.AddLabel(t.TransformPoint(v), $"<color=#{ColorUtility.ToHtmlStringRGBA(labelColor)}>{i++}</color>");
            lv.ForEach(v => v.Color = vectorColor);
            return lv;
        }

        private void OnSceneGUI()
        {
            if (Event.current.type == EventType.Repaint)
            {
                Draw(LabeledVertices);
                Draw(LabeledCollision);

                if (Mesh?.vertices != null)
                {
                    Transform t = Mesh.transform;
                    foreach (VertexObject vertex in Mesh.vertices)
                    {
                        if (vertex.byte_F != 255)
                        {
                            Handles.color = Color.green;
                            Vector3 v = t.TransformPoint(vertex.position);
                            Handles.SphereHandleCap(0, v, Quaternion.identity, HandleUtility.GetHandleSize(v) * 0.0005f * vertex.byte_F, EventType.Repaint);
                        }
                    }
                }
            }
        }

        private void Draw(LabeledVectors vectors)
        {
            if (vectors != null)
            {
                foreach (LabeledVector lv in vectors)
                {
                    Vector3 v = lv.Vector;
                    Handles.color = lv.Color;
                    Handles.SphereHandleCap(0, v, Quaternion.identity, HandleUtility.GetHandleSize(v) * 0.05f, EventType.Repaint);
                    Handles.Label(v, lv.Text, Style);
                }
            }
        }
    }
}
