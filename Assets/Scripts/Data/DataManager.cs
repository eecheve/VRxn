using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] private MaterialsDict materials;

    public Dictionary<string, Material> MaterialsDict = new Dictionary<string, Material>();
    
    private void Awake()
    {
        foreach(var entry in materials.Materials)
        {
            MaterialsDict.Add(entry.name, entry.material);
        }
    }
}
