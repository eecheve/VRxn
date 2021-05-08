using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshParentTransform : MonoBehaviour
{
    private Transform currentGrab;
    private Transform currentGrabParent;

    private List<Transform> currentGrabSiblings = new List<Transform>();
    private Vector3 refVector;


    private void OnEnable()
    {
        GameManager.OnLeftFirstGrab += ManageLeftGrab;
        GameManager.OnRightFirstGrab += ManageRightGrab;

        GameManager.OnLeftHasSwapped += ManageLeftGrab;
        GameManager.OnRightHasSwapped += ManageRightGrab;

        //GameManager.OnLeftHasDropped += ClearTransformReferences;
        //GameManager.OnRightHasDropped += ClearTransformReferences;

        GameManager.OnLeftHasDropped += RefreshTransformLeft;
        GameManager.OnRightHasDropped += RefreshTransformRight;

        GripButtonWatcher.Instance.onLeftGripHold.AddListener(RefreshPositionLeft);
        GripButtonWatcher.Instance.onRightGripHold.AddListener(RefreshPositionRight);
    }

    private void ManageLeftGrab()
    {
        if (currentGrab == null && currentGrabParent == null)
        {
            Debug.Log("RefreshParentTransform: assigning the current grab (left)");
            currentGrab = GameManager.Instance.LeftGrabbed;
            currentGrabParent = GameManager.Instance.LeftGrabbedParent;
            
            UpdateSiblingsList();
        }
        else if (currentGrab != null && currentGrab != GameManager.Instance.LeftGrabbed)
        {
            currentGrab = GameManager.Instance.LeftGrabbed;

            if(currentGrabParent != GameManager.Instance.LeftGrabbedParent)
                currentGrabParent = GameManager.Instance.LeftGrabbedParent;

            UpdateSiblingsList();
        }
    }

    private void UpdateSiblingsList()
    {
        currentGrabSiblings.Clear();

        foreach (Transform child in currentGrabParent)
        {
            if (child.name.Equals(currentGrabParent.name))
                continue;
            else
            {
                if(!currentGrabSiblings.Contains(child))
                    currentGrabSiblings.Add(child);
            }
        }
    }

    private void ManageRightGrab()
    {
        if (currentGrab == null)
        {
            Debug.Log("RefreshParentTransform: assigning the current grab");
            currentGrab = GameManager.Instance.RightGrabbed;
            currentGrabParent = GameManager.Instance.RightGrabbedParent;

            UpdateSiblingsList();
        }
        else if (currentGrab != null && currentGrab != GameManager.Instance.RightGrabbed)
        {
            currentGrab = GameManager.Instance.RightGrabbed;

            if (currentGrabParent != GameManager.Instance.RightGrabbedParent)
                currentGrabParent = GameManager.Instance.RightGrabbedParent;

            UpdateSiblingsList();
        }
    }

    private void ClearTransformReferences()
    {
        Debug.Log("RefreshParentTransform: clearing transform references");
        
        if (currentGrab != null)
            currentGrab = null;

        if (currentGrabParent != null)
            currentGrabParent = null;

        refVector = Vector3.zero;
        currentGrabSiblings.Clear();
    }

    private void RefreshPositionLeft(bool held)
    {
        if(held)
        {
            if(currentGrab != null && currentGrabParent != null)
            {
                refVector = currentGrabParent.position - currentGrab.position;
                Debug.Log("RefreshParentTransform: calculating refVector (left)");
            }
        }
    }

    private void RefreshPositionRight(bool held)
    {
        if (held)
        {
            if (currentGrab != null && currentGrabParent != null)
            {
                refVector = currentGrabParent.position - currentGrab.position;
                Debug.Log("RefreshParentTransform: calculating refVector");
            }
        }
        
    }

    private void RefreshTransformLeft()
    {
        if(currentGrab != null && currentGrabParent != null)
        {
            //removing parent-child relations
            foreach (Transform child in currentGrabParent)
            {
                if (child.name.Equals(currentGrabParent.name))
                    continue;
                else
                {
                    if (child.parent != null)
                        child.parent = null;
                }
            }

            Debug.Log("RefreshParentTransform: previous parent pos: (" +
                currentGrabParent.position.x.ToString() + "," +
                currentGrabParent.position.y.ToString() + "," +
                currentGrabParent.position.z.ToString() + ")");
            
            //reassigning parent position
            currentGrabParent.position = currentGrab.position + refVector;

            Debug.Log("RefreshParentTransform: new parent pos: (" +
                currentGrabParent.position.x.ToString() + "," +
                currentGrabParent.position.y.ToString() + "," +
                currentGrabParent.position.z.ToString() + ")");

            //restoring parent-child relations
            foreach (var sibling in currentGrabSiblings)
            {
                sibling.parent = currentGrabParent;
            }
        }
    }

    private void RefreshTransformRight()
    {
        if (currentGrab != null && currentGrabParent != null)
        {
            //removing parent-child relations
            foreach (Transform child in currentGrabParent)
            {
                if (child.name.Equals(currentGrabParent.name))
                    continue;
                else
                {
                    if (child.parent != null)
                        child.parent = null;
                }
            }

            //reassigning parent position
            currentGrabParent.position = currentGrab.position + refVector;

            //restoring parent-child relations
            foreach (var sibling in currentGrabSiblings)
            {
                sibling.parent = currentGrabParent;
            }
        }
    }

    private void OnDisable()
    {
        GameManager.OnLeftFirstGrab -= ManageLeftGrab;
        GameManager.OnRightFirstGrab -= ManageRightGrab;

        GameManager.OnLeftHasSwapped -= ManageLeftGrab;
        GameManager.OnRightHasSwapped -= ManageRightGrab;

        //GameManager.OnLeftHasDropped -= ClearTransformReferences;
        //GameManager.OnRightHasDropped -= ClearTransformReferences;

        GameManager.OnLeftHasDropped -= RefreshTransformLeft;
        GameManager.OnRightHasDropped -= RefreshTransformRight;

        GripButtonWatcher.Instance.onLeftGripHold.RemoveListener(RefreshPositionLeft);
        GripButtonWatcher.Instance.onRightGripHold.RemoveListener(RefreshPositionRight);
    }
}
