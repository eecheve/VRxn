using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChartDataManager))]
[ExecuteInEditMode]
public class ChartDataManagerEditor : Editor
{
    private ChartDataManager dataManager;

    private void OnEnable()
    {
        dataManager = (ChartDataManager)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Populate Chart Data"))
        {
            dataManager.PopulateChartData();
        }
        else if(GUILayout.Button("Empty Chart Data"))
        {
            dataManager.EmptyChartData();
        }
    }
}
