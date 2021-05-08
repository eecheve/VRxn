using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MoleculeDataList))]
[ExecuteInEditMode]
public class MoleculeDataListEditor : Editor
{
    private MoleculeDataList moleculeData;

    private void OnEnable()
    {
        moleculeData = (MoleculeDataList)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Populate Dictionaries"))
        {
            moleculeData.InitiallizeDictionaries();
        }
    }
}
