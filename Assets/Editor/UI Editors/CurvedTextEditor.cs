using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CurvedText))]
public class CurvedTextEditor : Editor
{
    private CurvedText curvedText;

    SerializedProperty diameter;
    SerializedProperty segmentAngle;

    private void OnEnable()
    {
        diameter = serializedObject.FindProperty("diameter");
        segmentAngle = serializedObject.FindProperty("segmentAngle");
    }

    public override void OnInspectorGUI()
    {
        //serializedObject.Update();
        //GUILayout.Label("Curved Text Properties", EditorStyles.boldLabel);
        //EditorGUILayout.PropertyField(diameter);
        //EditorGUILayout.PropertyField(segmentAngle);
        //EditorGUILayout.Space(5f);
        //serializedObject.ApplyModifiedProperties();

        DrawDefaultInspector();
    }
}
