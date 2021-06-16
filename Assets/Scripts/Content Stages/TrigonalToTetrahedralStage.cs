using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigonalToTetrahedralStage : MonoBehaviour
{
    [SerializeField] private List<MeshRenderer> planeMeshes = null;
    [SerializeField] private MeshRenderer tetrahedronMesh = null;

    private int counter = 0;

    public void TogglePlaneMeshes(bool state)
    {
        foreach (var mesh in planeMeshes)
        {
            mesh.enabled = state;
        }
    }

    public void ToggleTetrahedronMesh(bool state)
    {
        tetrahedronMesh.enabled = state;
    }

    public void ShowPlanePair()
    {
        if (counter == 0)
        {
            TogglePlaneMeshes(false);
            planeMeshes[0].enabled = true;
            planeMeshes[5].enabled = true;
            counter = 1;
        }
        else if(counter == 1)
        {
            TogglePlaneMeshes(false);
            planeMeshes[1].enabled = true;
            planeMeshes[2].enabled = true;
            counter = 2;
        }
        else if(counter == 2)
        {
            TogglePlaneMeshes(false);
            planeMeshes[3].enabled = true;
            planeMeshes[4].enabled = true;
            counter = 0;
        }
        else
        {
            Debug.LogError("TrigonalToTetrahedral: Invalid index value");
        }
    }
}
