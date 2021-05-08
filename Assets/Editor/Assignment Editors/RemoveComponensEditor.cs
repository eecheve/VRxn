using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RemoveComponents))]
public class RemoveComponensEditor : Editor
{
    private RemoveComponents remove;

    public void Reset()
    {
        remove = (RemoveComponents)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if(GUILayout.Button("Remove unwanted references"))
        {
            remove.RemoveAll();
        }
    }
}
