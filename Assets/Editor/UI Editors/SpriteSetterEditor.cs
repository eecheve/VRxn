using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpriteSetter))]
public class SpriteSetterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if(GUILayout.Button("Set Current Example"))
        {
            SpriteSetter setter = (SpriteSetter)target;
            setter.SetCurrentExample();
        }
        else if(GUILayout.Button("Set TS Placement"))
        {
            SpriteSetter setter = (SpriteSetter)target;
            setter.SetTransitionStatePlacement();
        }
    }
}
