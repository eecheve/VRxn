using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AtomDataList))]
[ExecuteInEditMode]
public class AtomDataListEditor : Editor
{
    private AtomDataList dataList;

    private void OnEnable()
    {
        dataList = (AtomDataList)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if(GUILayout.Button("Update Dictionary"))
        {
            dataList.PopulateBondLengthsDict();
        }
    }
}
