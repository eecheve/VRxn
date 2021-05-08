using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ConnectAssign))]
public class ConnectAssignEditor : Editor
{
    private bool connectivityAssigned = false;
    private ConnectAssign connectAssign;

    public void Reset()
    {
        connectivityAssigned = false;
        connectAssign = (ConnectAssign)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ButtonManager(connectAssign);
    }

    private void ButtonManager(ConnectAssign connectAssign)
    {
        if (GUILayout.Button("Assign Connectivity"))
        {
            connectAssign.LoadInformation();
            connectAssign.AssignCollider();
            connectAssign.AssignConnectivity();
            connectAssign.SetupAtomRigidbodies();
            connectAssign.SetupAtomConnectivities();
            connectAssign.ParentBondsToAtoms();
            connectivityAssigned = true;
        }
        else if(GUILayout.Button("Add Fixed Joints"))
        {
            connectAssign.AssignFixedJoints();
        }
        using (new EditorGUI.DisabledScope(connectivityAssigned == false))
        {          
            if (GUILayout.Button("Remove All"))
            {
                connectAssign.RemoveBoxColliders();
                connectAssign.RemoveSphereColliders();
                connectAssign.RemoveConnectivity();
                connectAssign.RemoveAllFixedJoints();
                connectAssign.RemoveAllRigidbodies();
                connectAssign.ResetParents();
                connectAssign.ClearLists();
                connectivityAssigned = false;
            }
        }

        GUILayout.Space(5.0f);
        /*if (GUILayout.Button("LoadInfoPreAnimation"))
        {
            connectAssign.LoadInformation();
        }
        else if (GUILayout.Button("RemoveReferencesForAnim"))
        {
            connectAssign.RemoveAllFixedJoints();
            connectAssign.RemoveConnectivity();
            connectAssign.RemoveXROffsetGrabbables();
            connectAssign.RemoveSphereColliders();
            connectAssign.RemoveBoxColliders();
            connectAssign.RemoveBondInfoComponents();
            connectAssign.ReassignCarbonMaterial();
        }*/
    }
}
