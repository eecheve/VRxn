using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformFollower : MonoBehaviour
{
    [SerializeField] private Transform diene = null;
    [SerializeField] private Transform transitionState = null;

    public Transform follower; 
    public Transform followed; 

    private void OnEnable()
    {
        follower = transitionState;
        followed = diene;
    }

    private void Update()
    {
        //transform.position = Vector3.Lerp(transform.position, targetTransform.position, Time.deltaTime);
        follower.position = followed.position;
        follower.rotation = followed.rotation;
    }

    public void SetFollower(Transform newFollower)
    {
        follower = newFollower;
    }

    public void SetFollowed(Transform newFollowed)
    {
        followed = newFollowed;
    }
}
