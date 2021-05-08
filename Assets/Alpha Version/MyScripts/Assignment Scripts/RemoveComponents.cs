using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveComponents : MonoBehaviour
{
    [SerializeField] private Transform toRemove = null;
    [SerializeField] private Material carbonMat = null;
    [SerializeField] private Material altCarbonMat = null;

    public void RemoveAll()
    {
        foreach (Transform child in toRemove)
        {
            RemoveFixedJoints(child);
            RemoveConnectivity(child);
            RemoveXROffsetGrabbable(child);
            RemoveColliders(child);
            RemoveBondInfo(child);
            RemoveRigidBodies(child);
            ReassignCarbonMaterial(child);
        }
    }

    public void ReassignCarbonMaterial(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.GetComponent<MeshRenderer>() != null)
            {
                Material[] materials = child.GetComponent<MeshRenderer>().sharedMaterials;

                if (materials != null)
                {
                    if (materials[0].Equals(carbonMat))
                    {
                        //Debug.Log("ConnectAssign: " + child.name + " has carbon material");
                        if (materials.Length > 1)
                        {
                            Debug.Log("ConnectAssign: " + child.name + " has two materials");
                            Material[] newMats = new Material[2];
                            newMats[0] = altCarbonMat;
                            newMats[1] = materials[1];
                            child.GetComponent<MeshRenderer>().sharedMaterials = newMats;
                        }
                        else
                        {
                            Debug.Log("ConnectAssign: " + child.name + " only has one material");
                            Material[] newMats = new Material[1];
                            newMats[0] = altCarbonMat;
                            child.GetComponent<MeshRenderer>().sharedMaterials = newMats;
                        }
                    }
                }
            }
        }
    }

    private void RemoveFixedJoints(Transform parent)
    {
        foreach (Transform child in parent)
        {
            FixedJoint[] x = child.gameObject.GetComponents<FixedJoint>();
            if (x != null)
            {
                foreach (var i in x)
                {
                    DestroyImmediate(i);
                }
            }
        }
    }

    private void RemoveConnectivity(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Connectivity x = child.gameObject.GetComponent<Connectivity>();
            if (x != null)
            {
                DestroyImmediate(x);
            }
        }
    }

    private void RemoveXROffsetGrabbable(Transform parent)
    {
        foreach (Transform child in parent)
        {
            XROffsetGrabbable x = child.gameObject.GetComponent<XROffsetGrabbable>();
            if (x != null)
            {
                DestroyImmediate(x);
            }
        }
    }

    private void RemoveColliders(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Collider x = child.gameObject.GetComponent<Collider>();
            if (x != null)
            {
                DestroyImmediate(x);
            }
        }
    }

    private void RemoveBondInfo(Transform parent)
    {
        foreach (Transform child in parent)
        {
            BondInfo x = child.gameObject.GetComponent<BondInfo>();
            if (x != null)
            {
                DestroyImmediate(x);
            }
        }
    }

    private void RemoveRigidBodies(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Rigidbody rb = child.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                DestroyImmediate(rb);
            }
        }
    }
}
