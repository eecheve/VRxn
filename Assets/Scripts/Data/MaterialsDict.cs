using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MaterialsDictionary", menuName = "Custom Objects")]
public class MaterialsDict : ScriptableObject
{
    [Serializable] public struct MaterialDict
    {
        public string name;
        public Material material;
    }

    [SerializeField] private MaterialDict[] materials;

    public MaterialDict[] Materials { get { return materials; } private set { materials = value; } }
}
