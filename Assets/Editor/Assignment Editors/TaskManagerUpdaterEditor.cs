using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TaskManagerIndexUpdater))]
public class TaskManagerUpdaterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if(GUILayout.Button("Update Task Names"))
        {
            TaskManagerIndexUpdater taskManager = (TaskManagerIndexUpdater)target;
            taskManager.ChangeTutorialIndices();
        }
    }
}
