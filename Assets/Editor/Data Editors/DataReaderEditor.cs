using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DataReader))]
[ExecuteInEditMode]
public class DataReaderEditor : Editor
{
    private DataReader dataReader;
    
    private void OnEnable()
    {
        dataReader = (DataReader)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Populate Vector2 List"))
        {
            dataReader.PopulateVector2List();
        }
    }
}
