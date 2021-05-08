using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enumerators;

public class AssignAtomicOrbitals : MonoBehaviour
{
    #region singleton
    private static AssignAtomicOrbitals _instance;
    public static AssignAtomicOrbitals Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("ConnectAssign is not in the scene");

            return _instance;
        }
    }
    #endregion

    [SerializeField] private GameObject pzOrbital = null;

    private List<Transform> atomList;

    public List<Transform> primaryAtomList { get; private set; }
    private List<Transform> secondaryAtomList;
    private List<KeyValuePair<Transform, Transform>> atomPairs;

    public void LoadInformation()
    {
        _instance = this;
        
        Debug.Log("AssignAtomicOrbitals: Loading Information");
        atomList = ConnectAssign.Instance.AtomList;

        primaryAtomList = new List<Transform>();
        secondaryAtomList = new List<Transform>();
        atomPairs = new List<KeyValuePair<Transform, Transform>>();
    }
    
    public void PopulateRefinedList()
    {
        Debug.Log("AssignAtomicOrbitals: Refining atom list");
        foreach (var atom in atomList)
        {
            if(atom.GetComponent<Connectivity>() != null)
            {
                Connectivity connect = atom.GetComponent<Connectivity>();
                if (connect.atomLabel != AtomLabel.Backbone && !primaryAtomList.Contains(atom))
                {
                    primaryAtomList.Add(atom);
                }
                else if(connect.atomLabel == AtomLabel.Backbone && !secondaryAtomList.Contains(atom))
                {
                    if (connect.eGeometry == EGeometry.TrigonalPlanar || connect.eGeometry == EGeometry.Linear)
                        secondaryAtomList.Add(atom);
                }
            }
        }
    }

    private List<KeyValuePair<Transform, Transform>> GetTransformPairsFromList(List<Transform> list)
    {
        Debug.Log("AssignAtomicOrbitals: Getting atom pairs");
        List<KeyValuePair<Transform, Transform>> allPairs = new List<KeyValuePair<Transform, Transform>>();
        List<KeyValuePair<Transform, Transform>> refinedPairs = new List<KeyValuePair<Transform, Transform>>();

        for (int i = 0; i < list.Count; i++)
            for (int j = i + 1; j < list.Count; j++)
                if (j < list.Count)
                    allPairs.Add(new KeyValuePair<Transform, Transform>(list[i], list[j]));

        foreach (var pair in allPairs)
        {
            if (pair.Key.GetComponent<Connectivity>().atomLabel !=
                    pair.Value.GetComponent<Connectivity>().atomLabel)
            {
                if (!refinedPairs.Contains(pair))
                    refinedPairs.Add(pair);
            }
        }

        return refinedPairs;
    }

    public void RefineTransformPairs()
    {
        Debug.Log("AssignAtomicOrbitals: Refininf transform pairs");
        List<KeyValuePair<Transform, Transform>> newPairs = new List<KeyValuePair<Transform, Transform>>();
        newPairs = GetTransformPairsFromList(primaryAtomList);

        newPairs.Sort((v1, v2) => (v1.Key.position - v1.Value.position).sqrMagnitude
                                   .CompareTo((v2.Key.position - v2.Value.position).sqrMagnitude));


        atomPairs.Add(newPairs[0]);
        atomPairs.Add(newPairs[1]);

        foreach (var pair in atomPairs)
            Debug.Log(pair.Key.name + "-" + pair.Value.name);
    }
    
    public void InstantiateAtomicOrbital()
    {
        Debug.Log("Instantiating Atomic orbitals");
        foreach (var pair in atomPairs)
        {
            Vector3 up = pair.Key.position - pair.Value.position;
            GameObject pzKey = Instantiate(pzOrbital, pair.Key.position, pair.Key.rotation, pair.Key);
            GameObject pzValue = Instantiate(pzOrbital, pair.Value.position, pair.Value.rotation, pair.Value);

            pzKey.name = pair.Key.name + "_pz";
            pzValue.name = pair.Value.name + "_pz";

            pzKey.transform.up = up;
            pzValue.transform.up = up * -1;
        }
     }

    public void InstantiateSecondaryOrbitals()
    {
        foreach (var secondaryAtom in secondaryAtomList)
        {
            Connectivity connect = secondaryAtom.GetComponent<Connectivity>();
                        
            foreach (var primaryAtom in primaryAtomList)
            {
                if (connect.connectedAtoms.Contains(primaryAtom))
                {
                    foreach (Transform child in primaryAtom)
                    {
                        if (child.name.Contains("_pz"))
                        {
                            Vector3 up = child.transform.up;

                            GameObject pz = Instantiate(pzOrbital, secondaryAtom.position, secondaryAtom.rotation, secondaryAtom);
                            pz.name = secondaryAtom.name + "_pz";
                            pz.transform.up = up;

                            break;
                        }
                    }
                    break;
                }
            }
        }
    }
}
