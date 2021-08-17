using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildMeshManager : MonoBehaviour
{
    private List<MeshRenderer> meshes = new List<MeshRenderer>();

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            MeshRenderer mesh = child.gameObject.GetComponent<MeshRenderer>();
            if (mesh != null && meshes.Contains(mesh) == false)
            {
                meshes.Add(mesh);
            }
            if(child.childCount > 0)
            {
                foreach (Transform grandChild in child)
                {
                    MeshRenderer m2 = grandChild.gameObject.GetComponent<MeshRenderer>();
                    if (m2 != null && meshes.Contains(m2) == false)
                    {
                        meshes.Add(m2);
                    }
                }
            }
        }
    }

    public void ManageChildMeshes(bool state)
    {
        foreach (var mesh in meshes)
        {
            mesh.enabled = state;
        }
    }
}
