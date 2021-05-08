using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StretchBonds : MonoBehaviour
{
    [SerializeField] private int maxVirtualBondCount = 60;
    [SerializeField] private Material virtualBondMaterial = null;

    private List<BondInfo> bondList;

    private GameObject[] virtualBonds;

    private bool isStretching = false;
    private bool bondListPopulated = false;
    private bool bondsAreHidden = false;

    private void Awake()
    {
        bondList = new List<BondInfo>();
        InitializeVirtualBonds();
        PopulateReferences(); //<--- 1
    }

    private void Update()
    {
        if(GameManager.Instance.SameParentGrab == true)
        {
            StartStretch();
            isStretching = true;
        }
        else if(GameManager.Instance.SameParentGrab == false && isStretching == true)
        {
            StopStretch();
            isStretching = false;
        }
    }

    private void InitializeVirtualBonds()
    {
        virtualBonds = new GameObject[maxVirtualBondCount];
        for (int i = 0; i < maxVirtualBondCount; i++)
        {
            virtualBonds.SetValue(GameObject.CreatePrimitive(PrimitiveType.Cylinder), i);
            virtualBonds[i].transform.localScale = new Vector3(0.02f, 0.1f, 0.02f);
            Destroy(virtualBonds[i].GetComponent<CapsuleCollider>());
            virtualBonds[i].GetComponent<MeshRenderer>().material = virtualBondMaterial;
            virtualBonds[i].SetActive(false);
        }
    }

    private void StartStretch()
    {
        if (bondList != null)
        {
            ToggleDefaultBonds(false);
            StretchVirtualBonds();
        }
    }

    private void StopStretch()
    {
        HideVirtualBonds();
        ToggleDefaultBonds(true);
    }

    private void PopulateReferences()
    {
        Transform[] children = CurrentSceneManager.Instance.Grabbable.GetComponentsInChildren<Transform>(); //<-- 4

        if (bondListPopulated == false)
        {
            foreach (Transform child in children)
            {
                BondInfo[] bonds = child.gameObject.GetComponentsInChildren<BondInfo>();
                if (bonds != null)
                {
                    foreach (var bond in bonds)
                    {
                        bondList.Add(bond);
                    }
                }
            }
            bondListPopulated = true;
        }
    }

    private void ToggleDefaultBonds(bool state)
    {
        ///Either hides or shows default bonds depending on state.
        ///If state is true, shows default bonds.
        
        if(bondsAreHidden == state)
        {
            foreach (var bond in bondList)
                bond.gameObject.GetComponent<MeshRenderer>().enabled = state;

            bondsAreHidden = !state;
        }
    }

    private void StretchVirtualBonds()
    {
        for (int i = 0; i < bondList.Count; i++)
        {
            Vector3 refVector = bondList[i].atom2.position - bondList[i].atom1.position;
            Vector3 midPoint = (bondList[i].atom2.position + bondList[i].atom1.position) / 2;
            float distance = refVector.magnitude;
            Vector3 refDir = refVector / distance;

            //manage bond width -- Currently not working, requires tweaking of parameters.
            //float maxDistance = Mathf.Clamp(distance / 2f, 0.001f, maxBondDistance);
            //float normalizedDistance = (maxDistance / maxBondDistance) * 0.02f;
            //float normalizedDistance = 0.019f * distance / maxBondDistance;
            //float width = 0.02f - normalizedDistance;

            virtualBonds[i].SetActive(true);

            virtualBonds[i].transform.up = refDir;
            virtualBonds[i].transform.localScale = new Vector3(0.02f, distance / 2f, 0.02f); //new Vector3(width, distance/2f, width);
            virtualBonds[i].transform.position = midPoint;

            if(bondList[i].name.Contains('=') && !bondList[i].name.Contains('.'))
                virtualBonds[i].transform.position = virtualBonds[i].transform.position + (virtualBonds[i].transform.right * 0.02f);
            else if(bondList[i].name.Contains('=') && bondList[i].name.Contains('.'))
                virtualBonds[i].transform.position = virtualBonds[i].transform.position - (virtualBonds[i].transform.right * 0.02f);


        }
    }

    private void HideVirtualBonds()
    {
        foreach (var virtualBond in virtualBonds)
        {
            if(virtualBond.gameObject.activeSelf == true)
            {
                virtualBond.transform.localScale = new Vector3(0.02f, 1f, 0.02f);
                virtualBond.SetActive(false);
            }
        }
    }
}
