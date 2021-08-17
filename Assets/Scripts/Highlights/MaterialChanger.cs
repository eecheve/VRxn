using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    [SerializeField] private GameObject[] objects = null;
    [SerializeField] private Material newMaterial = null;

    private List<Material> oldMaterials = new List<Material>();

    private void Awake()
    {
        foreach (var obj in objects)
        {
            MeshRenderer mesh = obj.GetComponent<MeshRenderer>();
            if (mesh != null)
            {
                oldMaterials.Add(mesh.material);
            }
        }
    }

    public void UpdateMaterials()
    {
        foreach (var obj in objects)
        {
            MeshRenderer mesh = obj.GetComponent<MeshRenderer>();
            if (mesh != null)
            {
                mesh.material = newMaterial;
            }
        }
    }

    public void ResetMaterials()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            MeshRenderer mesh = objects[i].GetComponent<MeshRenderer>();
            if (mesh != null)
            {
                mesh.material = oldMaterials[i];
            }
        }
    }
}
