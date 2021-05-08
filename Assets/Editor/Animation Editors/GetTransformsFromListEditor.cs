using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GetTransformsFromList))]
public class GetTransformsFromListEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GetTransformsFromList getTransforms = (GetTransformsFromList)target;
        if(GUILayout.Button("Load Transforms"))
        {
            Debug.Log("Loading transforms...");
            getTransforms.LoadTransformList();
        }
        else if(GUILayout.Button("Unload Transforms"))
        {
            Debug.Log("Unloading transforms...");
            getTransforms.UnloadTransformList();
        }
    }
}
