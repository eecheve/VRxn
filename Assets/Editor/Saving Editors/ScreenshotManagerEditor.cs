using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScreenshotManager))]
[ExecuteInEditMode]
public class ScreenshotManagerEditor : Editor
{
    private ScreenshotManager manager;

    private void OnEnable()
    {
        manager = (ScreenshotManager)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Take Screenshot"))
        {
            manager.TakeScreenshot();
        }
    }
}
