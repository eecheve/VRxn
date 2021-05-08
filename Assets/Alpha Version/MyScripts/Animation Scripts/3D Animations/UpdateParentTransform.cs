using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateParentTransform : MonoBehaviour
{
    private Dictionary<Transform, Vector3> referenceToChildren = new Dictionary<Transform, Vector3>();
    
    private Vector3 refVector = new Vector3(); //<--- is this necessary?????
    private Transform grabbed;
    
    private void Awake()
    {
        foreach (Transform child in transform)
        {
            if (!referenceToChildren.ContainsKey(child))
            {
                referenceToChildren.Add(child, transform.position - child.position);
            }
        }
    }

    private void OnEnable()
    {
        GripButtonWatcher.Instance.onLeftGripHold.AddListener(UpdateTransformLeft);
        GameManager.OnLeftHasDropped += RevertParentRelations;

        GripButtonWatcher.Instance.onRightGripHold.AddListener(UpdateTransformRight);
        GameManager.OnRightHasDropped += RevertParentRelations;
    }



    private void UpdateTransformLeft(bool held)
    {
        if (held)
        {
            ToggleParentChildRelationship(false);
            grabbed = GameManager.Instance.LeftGrabbed;
            if (grabbed != null && referenceToChildren.TryGetValue(grabbed, out refVector))
            {
                transform.position = grabbed.position - refVector;
            }
        }
    }

    private void ToggleParentChildRelationship(bool state)
    {
        if (state == true)
        {
            foreach (var entry in referenceToChildren)
            {
                entry.Key.parent = transform;
            }
        }
        else
        {
            foreach (var entry in referenceToChildren)
            {
                entry.Key.parent = null;
            }
        }
    }

    private void RevertParentRelations()
    {
        ToggleParentChildRelationship(true);
        grabbed = null;
    }

    private void UpdateTransformRight(bool held)
    {
        if (held)
        {
            ToggleParentChildRelationship(false);
            grabbed = GameManager.Instance.RightGrabbed;
            if (grabbed != null && referenceToChildren.TryGetValue(grabbed, out refVector))
            {
                transform.position = grabbed.position + refVector;
            }
        }
    }

    private void OnDisable()
    {
        GripButtonWatcher.Instance.onLeftGripHold.RemoveListener(UpdateTransformLeft);
        GameManager.OnLeftHasDropped -= RevertParentRelations;

        GripButtonWatcher.Instance.onRightGripHold.RemoveListener(UpdateTransformRight);
        GameManager.OnRightHasDropped -= RevertParentRelations;
    }
}
