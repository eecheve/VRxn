using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateScaffolding : MonoBehaviour
{
    [SerializeField] private GameObject referenceFrame = null;

    private Transform objectToModify;
    private List<Transform> primaryAtoms;
    private GameObject refObj;
    
    public void InstantiateReferenceFrame()
    {
        objectToModify = ChildrenTransformGetter.Instance.objectToModify.transform;
        primaryAtoms = AssignAtomicOrbitals.Instance.primaryAtomList;

        refObj = Instantiate(referenceFrame, objectToModify.position, objectToModify.rotation, objectToModify);
        refObj.name = "ref_Frame";
    }

    public void AssignReferenceAnchors()
    {
        if (refObj != null)
        {
            FixedJoint[] fixedJoints = refObj.GetComponents<FixedJoint>();

            for (int i = 0; i < fixedJoints.Length; i++)
            {
                fixedJoints[i].connectedBody = primaryAtoms[i].gameObject.GetComponent<Rigidbody>();
                fixedJoints[i].connectedAnchor = fixedJoints[i].connectedBody.gameObject.transform.position;
            }
        }
    }

    public void UnAssignRefObj()
    {
        refObj = null;
    }
}
