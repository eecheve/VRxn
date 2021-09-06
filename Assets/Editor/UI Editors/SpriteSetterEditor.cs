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
        else if(GUILayout.Button("Set Substituent Types"))
        {
            SpriteSetter setter = (SpriteSetter)target;
            setter.SetSubstituentTypes();
        }
        else if(GUILayout.Button("Set Orientation Task"))
        {
            SpriteSetter setter = (SpriteSetter)target;
            setter.SetRotationTask();
        }
        else if(GUILayout.Button("Set Model Stages"))
        {
            SpriteSetter setter = (SpriteSetter)target;
            setter.SetModelStages();
        }
    }
}
