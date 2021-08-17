using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualReferences : MonoBehaviour
{
    [SerializeField] private Transform[] molecules = null;

    public void SetPrismsActive(bool state)
    {
        foreach (var molecule in molecules)
        {
            foreach (Transform child in molecule)
            {
                if (child.CompareTag("HexagonalPrism"))
                {
                    child.gameObject.SetActive(state);
                }
            }
        }
    }

    public void SetOrbitalsActive(bool state)
    {
        foreach (var molecule in molecules)
        {
            foreach (Transform child in molecule)
            {
                if (child.CompareTag("Molecule"))
                {
                    foreach (Transform granChild in child)
                    {
                        if (granChild.childCount > 0)
                        {
                            foreach (Transform granGranChild in granChild)
                            {
                                if (granGranChild.CompareTag("Orbital"))
                                    granGranChild.gameObject.SetActive(state);
                            }
                        }
                    }
                }
            }
        }
    }
}
