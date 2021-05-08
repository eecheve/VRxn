using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Enumerators;

[CustomEditor(typeof(Connectivity))]
[CanEditMultipleObjects]
public class ConnectivityEditor : Editor
{
    private Connectivity connect;

    public void Reset()
    {
        connect = (Connectivity)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
