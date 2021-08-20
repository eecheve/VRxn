using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ButtonDebug))]
public class ButtonDebugEditor : Editor
{
    private ButtonDebug buttonDebug;

    //public void Reset()
    //{
    //    buttonDebug = (ButtonDebug)target;
    //}

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Expose();
    }

    private void Expose()
    {
        buttonDebug = (ButtonDebug)target;
        
        if (GUILayout.Button("Debug"))
            buttonDebug.DebugButton();
    }
}
