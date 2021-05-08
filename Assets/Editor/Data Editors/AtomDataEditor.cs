using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AtomData))]
[CanEditMultipleObjects]
[ExecuteInEditMode]
public class AtomDataEditor : Editor
{
    SerializedProperty atom;
    SerializedProperty symbol;
    SerializedProperty bondType;
    SerializedProperty bondLength;
    SerializedProperty atomicMass;

    private void OnEnable()
    {
        atom = serializedObject.FindProperty("atom");
        symbol = serializedObject.FindProperty("symbol");
        bondType = serializedObject.FindProperty("bondType");
        bondLength = serializedObject.FindProperty("bondLength");
        atomicMass = serializedObject.FindProperty("atomicMass");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(atom);
        EditorGUILayout.PropertyField(symbol);
        EditorGUILayout.PropertyField(atomicMass);
        EditorGUILayout.Space(5f);

        GUILayout.Label("Bond Information", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUIUtility.labelWidth = 100f;
        EditorGUILayout.PropertyField(bondLength, GUILayout.ExpandWidth(false));
        EditorGUIUtility.labelWidth = 100f;
        EditorGUILayout.PropertyField(bondType, GUILayout.ExpandWidth(false));
        EditorGUILayout.EndHorizontal();
        serializedObject.ApplyModifiedProperties();
    }
}
