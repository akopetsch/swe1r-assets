// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

using UnityEditor;
using UnityEngine;

namespace SWE1R.Assets.Blocks.Unity.Assets
{
    /// <summary>
    /// Custom property drawer for <see cref="SerializableNullable{T}"/>.
    /// <para>
    /// The code is a refactored version of the code under:
    /// <see href="https://discussions.unity.com/t/why-doesnt-unity-property-editor-show-a-nullable-variable/226218/3"/>
    /// </para>
    /// </summary>
    [CustomPropertyDrawer(typeof(SerializableNullable<>))]
    internal class SerializableNullableDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Don't make child fields be indented
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Calculate rects
            var setRect = new Rect(position.x, position.y, 15, position.height);
            var consumed = setRect.width + 5;
            var valueRect = new Rect(position.x + consumed, position.y, position.width - consumed, position.height);

            // Draw fields - pass GUIContent.none to each so they are drawn without labels
            var hasValueProp = property.FindPropertyRelative("hasValue");
            EditorGUI.PropertyField(setRect, hasValueProp, GUIContent.none);
            bool guiEnabled = GUI.enabled;
            GUI.enabled = guiEnabled && hasValueProp.boolValue;
            EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("v"), GUIContent.none);
            GUI.enabled = guiEnabled;

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}
