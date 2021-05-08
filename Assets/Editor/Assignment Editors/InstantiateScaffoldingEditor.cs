using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InstantiateScaffolding))]
public class InstantiateScaffoldingEditor : Editor
{
    private InstantiateScaffolding instantiateScaff;

    public void Reset()
    {
        instantiateScaff = (InstantiateScaffolding)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        if(GUILayout.Button("Instantiate Ref Frame"))
        {
            instantiateScaff.InstantiateReferenceFrame();
        }
        else if (GUILayout.Button("AssignReferenceAnchors"))
        {
            instantiateScaff.AssignReferenceAnchors();
        }
    }
}
