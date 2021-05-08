using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AssignAtomicOrbitals))]
public class AssignAtomicOrbitalsEditor : Editor
{
    private AssignAtomicOrbitals assignAO;

    public void Reset()
    {
        assignAO = (AssignAtomicOrbitals)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if(GUILayout.Button("Instantiate Orbitals"))
        {
            assignAO.LoadInformation();
            assignAO.PopulateRefinedList();
            assignAO.RefineTransformPairs();
            assignAO.InstantiateAtomicOrbital();
            assignAO.InstantiateSecondaryOrbitals();
        }
    }
}
