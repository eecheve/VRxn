using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    [SerializeField] private Transform toFollow = null;
    [SerializeField] private bool followPosition = false;
    [SerializeField] private bool followRefPoint = false;
    [SerializeField] private bool followRotation = false;
    [SerializeField] private bool followForward = false;

    private Vector3 refVector;
    private float distance;

    private void Awake()
    {
        refVector = transform.position - toFollow.position;
        distance = refVector.magnitude;
    }

    private void Update()
    {
        FollowPosition();
        FollowRotation();
    }

    private void FollowPosition()
    {
        if(followPosition == true)
            transform.position = toFollow.position + refVector;

        else if(followRefPoint == true)
        {
            transform.position = toFollow.position + (toFollow.forward * distance) + (Vector3.up * 0.25f) ;
        }
    }

    private void FollowRotation()
    {
        if (followRotation == true)
            transform.rotation = toFollow.rotation;
        
        else if (followForward == true)
            transform.forward = toFollow.forward;
    }
}
