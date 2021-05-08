using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChildrenTransformGetter))]
public class ChildTransformGetterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ChildrenTransformGetter transformGetter = (ChildrenTransformGetter)target;
               
        if(GUILayout.Button("Load Information"))
        {
            transformGetter.LoadObjectInformation();
        }
    }
}
