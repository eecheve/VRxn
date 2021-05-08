using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;
using static Enumerators;

public class ShowMOs : MonoBehaviour
{
    private Transform leftMolecule;
    private Transform rightMolecule;

    private List<GameObject> leftMOs = new List<GameObject>();
    private List<GameObject> rightMOs = new List<GameObject>();

    private void OnEnable()
    {
        GameManager.OnLeftFirstGrab += PopulateFirstLeftMOs;
        GameManager.OnRightFirstGrab += PopulateFirstRightMOs;
        GameManager.OnLeftHasSwapped += PopulateLeftMOs;
        GameManager.OnRightHasSwapped += PopulateRightMOs;
    }

    private void PopulateFirstLeftMOs()
    {
        leftMolecule = GameManager.Instance.LeftGrabbedParent;
        if (leftMolecule != null)
        {
            Debug.Log("Listening to PopulateFirstLeftMOs");
            InitiallizeMOs(leftMolecule, leftMOs);
        }
    }

    private void PopulateLeftMOs()
    {
        leftMolecule = GameManager.Instance.LeftGrabbedParent;
        if(leftMolecule!= null)
        {
            leftMOs.Clear();
            InitiallizeMOs(leftMolecule, leftMOs);
        }
    }

    private void InitiallizeMOs(Transform parent, List<GameObject> mos)
    {
        if (parent != null)
        {
            foreach (Transform child in parent)
            {
                Transform[] grandChildren = child.GetComponentsInChildren<Transform>();
                if (grandChildren != null)
                {
                    foreach (var grandChild in grandChildren)
                    {
                        if (grandChild.gameObject.CompareTag("AtomicOrbital"))
                        {
                            if (!mos.Contains(grandChild.gameObject))
                                mos.Add(grandChild.gameObject);
                        }
                    }
                }
            }
        }
    }

    private void PopulateFirstRightMOs()
    {
        rightMolecule = GameManager.Instance.RightGrabbedParent;
        if(rightMolecule != null)
        {
            Debug.Log("Listening to PopulateFirstRightMOs");
            InitiallizeMOs(rightMolecule, rightMOs);
        }
    }

    private void PopulateRightMOs()
    {
        rightMolecule = GameManager.Instance.RightGrabbedParent;
        if (rightMolecule != null)
        {
            rightMOs.Clear();
            InitiallizeMOs(rightMolecule, rightMOs);
        }
    }

    public void ShowLeftOrbitals(string orbitalType)
    {
        Enum.TryParse(orbitalType, out OrbitalType orbital);
        
        if(leftMolecule != null && leftMOs.Any())
        {
            string moleculeName = leftMolecule.name;
            MoleculeData moleculeData = MoleculeDataList.Instance.MoleculeList.Where(obj => obj.name == moleculeName).SingleOrDefault();
            if(moleculeData != null)
            {
                float[] coeffs = moleculeData.OrbitalCoeffsDict[orbital];
                for (int i = 0; i < coeffs.Length; i++)
                {
                    if (coeffs[i] < 0)
                    {
                        leftMOs[i].transform.up = leftMOs[i].transform.up * -1;
                        leftMOs[i].GetComponent<MoRotationState>().isRotated = true;
                        float value = coeffs[i] * -1;
                        Vector3 scale = new Vector3(value, value, value);
                        leftMOs[i].transform.localScale = scale;
                        ManageObjectMeshes(leftMOs[i], true);
                    }
                    else
                    {
                        leftMOs[i].GetComponent<MoRotationState>().isRotated = false;
                        Vector3 scale = new Vector3(coeffs[i], coeffs[i], coeffs[i]);
                        leftMOs[i].transform.localScale = scale;
                        ManageObjectMeshes(leftMOs[i], true);
                    }
                }
            }
        }
    }

    private void ManageObjectMeshes(GameObject gameObject, bool state)
    {
        MeshRenderer[] meshes = gameObject.GetComponentsInChildren<MeshRenderer>();
        if(meshes != null)
        {
            foreach (var mesh in meshes)
            {
                mesh.enabled = state;
            }
        }
    }

    private void ResetLeftMOs()
    {
        if (leftMOs.Any())
        {
            Vector3 scale = new Vector3(1f, 1f, 1f);
            foreach (var mo in leftMOs)
            {
                mo.transform.localScale = scale;
                if(mo.GetComponent<MoRotationState>().isRotated == true)
                {
                    mo.transform.up = mo.transform.up * -1;
                    mo.GetComponent<MoRotationState>().isRotated = false;
                }
            }
        }
    }

    public void HideLeftMOs()
    {
        ResetLeftMOs();
        if (leftMOs.Any())
        {
            foreach (var mo in leftMOs)
            {
                ManageObjectMeshes(mo, false);
            }
        }
    }

    public void ShowRightOrbitals(string orbitalType)
    {
        Enum.TryParse(orbitalType, out OrbitalType orbital);

        if (rightMolecule != null && rightMOs.Any())
        {
            string moleculeName = rightMolecule.name;
            MoleculeData moleculeData = MoleculeDataList.Instance.MoleculeList.Where(obj => obj.name == moleculeName).SingleOrDefault();
            if (moleculeData != null)
            {
                float[] coeffs = moleculeData.OrbitalCoeffsDict[orbital];
                for (int i = 0; i < coeffs.Length; i++)
                {
                    if (coeffs[i] < 0)
                    {
                        rightMOs[i].transform.up = rightMOs[i].transform.up * -1;
                        rightMOs[i].GetComponent<MoRotationState>().isRotated = true;
                        float value = coeffs[i] * -1;
                        Vector3 scale = new Vector3(value, value, value);
                        rightMOs[i].transform.localScale = scale;
                        ManageObjectMeshes(rightMOs[i], true);
                    }
                    else
                    {
                        Vector3 scale = new Vector3(coeffs[i], coeffs[i], coeffs[i]);
                        rightMOs[i].transform.localScale = scale;
                        ManageObjectMeshes(rightMOs[i], true);
                    }
                }
            }
        }
    }

    private void ResetRightMOs()
    {
        if (rightMOs.Any())
        {
            Vector3 scale = new Vector3(1f, 1f, 1f);
            foreach (var mo in rightMOs)
            {
                mo.transform.localScale = scale;
                if (mo.GetComponent<MoRotationState>().isRotated == true)
                {
                    mo.transform.up = mo.transform.up * -1;
                    mo.GetComponent<MoRotationState>().isRotated = false;
                }
            }
        }
    }

    public void HideRightMOs()
    {
        ResetRightMOs();
        if (rightMOs.Any())
        {
            foreach (var mo in rightMOs)
            {
                ManageObjectMeshes(mo, false);
            }
        }
    }

    private void OnDisable()
    {
        GameManager.OnLeftFirstGrab -= PopulateFirstLeftMOs;
        GameManager.OnRightFirstGrab -= PopulateFirstRightMOs;
        GameManager.OnLeftHasSwapped -= PopulateLeftMOs;
        GameManager.OnRightHasSwapped -= PopulateRightMOs;
    }
}
