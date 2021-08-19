using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTransforms : MonoBehaviour
{
    private Vector3 startPos;
    private Quaternion startRot;

    private void Awake()
    {
        startPos = transform.position;
        startRot = transform.rotation;
    }

    public void ResetPosition()
    {
        transform.position = startPos;
    }

    public void ResetRotation()
    {
        transform.rotation = startRot;
    }

    public void ResetPositionAndRotation()
    {
        transform.SetPositionAndRotation(startPos, startRot);
    }

    public void ZeroRotation()
    {
        transform.eulerAngles = Vector3.zero;
    }
}
