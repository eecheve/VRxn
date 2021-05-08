using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MoleculeData))]
public class MoleculeDataEditor : Editor
{
    private bool m_rotatable;

    SerializedProperty moleculeName;
    SerializedProperty moleculeSprite;
    SerializedProperty rotatable;
    SerializedProperty rotationBarrier;
    SerializedProperty orbitalEnergies;
    SerializedProperty orbitalCoefficients;
    SerializedProperty orbitalDepictions;

    private void OnEnable()
    {
        moleculeName = serializedObject.FindProperty("moleculeName");
        moleculeSprite = serializedObject.FindProperty("moleculeSprite");
        rotatable = serializedObject.FindProperty("rotatable");
        rotationBarrier = serializedObject.FindProperty("rotationBarrier");
        orbitalEnergies = serializedObject.FindProperty("orbitalEnergies");
        orbitalCoefficients = serializedObject.FindProperty("orbitalCoeffs");
        orbitalDepictions = serializedObject.FindProperty("orbitalDepictions");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("General info", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(moleculeName);
        EditorGUILayout.PropertyField(moleculeSprite);

        EditorGUILayout.LabelField("Rotation Barriers", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(rotatable);
        m_rotatable = rotatable.boolValue;
        using (new EditorGUI.DisabledScope(m_rotatable == false))
        {
            EditorGUILayout.PropertyField(rotationBarrier);
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space(5f);

        EditorGUILayout.LabelField("Orbital Energies", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(orbitalEnergies);
        EditorGUILayout.Space(5f);

        EditorGUILayout.LabelField("Orbital Coefficients", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(orbitalCoefficients);

        EditorGUILayout.LabelField("Orbital Depictions", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(orbitalDepictions);

        serializedObject.ApplyModifiedProperties();
    }
}
