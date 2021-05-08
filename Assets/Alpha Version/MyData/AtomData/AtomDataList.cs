using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomDataList : MonoBehaviour
{
    private static AtomDataList _instance;
    public static AtomDataList Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("The atom data list does not exist");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public List<AtomData> atomList;

    public Dictionary<string, float> BondLengthsDict { get; private set; }

    public void PopulateBondLengthsDict()
    {
        BondLengthsDict = new Dictionary<string, float>();

        if (atomList == null)
        {
            Debug.LogError("Atom list has not been initiallized");
        }
        else if(atomList.Count == 0)
        {
            Debug.Log("Atom list has been initialized but does not contain any atoms");
        }
        else
        {
            foreach (var atom in atomList)
            {
                for (int i = 0; i < atom.bondType.Length; i++)
                {
                    if (!BondLengthsDict.ContainsKey(atom.bondType[i]))
                    {
                        BondLengthsDict.Add(atom.bondType[i], atom.bondLength[i]);
                    }
                }
            }
        }
    }
}
