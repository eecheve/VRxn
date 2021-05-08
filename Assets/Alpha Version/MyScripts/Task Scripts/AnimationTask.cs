using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimationTask : MonoBehaviour
{
    [Header("Initial Parameters")]
    [SerializeField] private Material axialInitialMat = null;
    [SerializeField] private Material eqInitialMat = null;

    [Header("Axial")]
    [SerializeField] private Material axialMaterial = null;
    [SerializeField] private MeshRenderer[] axialMeshes = null;
    
    [Header("Equatorial")]
    [SerializeField] private Material eqMaterial = null;
    [SerializeField] private MeshRenderer[] eqMeshes = null;

    public void ChangeAxialMeshes()
    {
        foreach (var mesh in axialMeshes)
        {
            if(mesh.materials.Length > 1)
            {
                mesh.materials[1] = axialMaterial;
            }
            else
            {
                mesh.material = axialMaterial;
            }
        }
    }

    public void RevertAxialMeshes()
    {
        foreach (var mesh in axialMeshes)
        {
            if (mesh.materials.Length > 1)
            {
                mesh.materials[1] = axialInitialMat;
            }
            else
            {
                mesh.material = axialInitialMat;
            }
        }
    }

    public void ChangeEquatorialMeshes()
    {
        foreach (var mesh in eqMeshes)
        {
            if (mesh.materials.Length > 1)
            {
                mesh.materials[1] = eqMaterial;
            }
            else
            {
                mesh.material = eqMaterial;
            }
        }
    }

    public void RevertEquatorialMeshes()
    {
        foreach (var mesh in eqMeshes)
        {
            if (mesh.materials.Length > 1)
            {
                mesh.materials[1] = eqInitialMat;
            }
            else
            {
                mesh.material = eqInitialMat;
            }
        }
    }
}
