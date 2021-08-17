using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapMeshToTransform : MonoBehaviour
{
    [SerializeField] private Transform target = null;

    public Transform Object { get; set; } = null;

    public void SnapMesh()
    {
        if (Object != null && target != null)
        {
            Debug.Log("SnapMeshToTransform: should call the function");
            StartCoroutine(SmoothMovement(3.0f));
        }
    }

    private IEnumerator SmoothMovement(float time) //https://answers.unity.com/questions/1501234/smooth-forward-movement-with-a-coroutine.html
    {
        Vector3 startPos = Object.position;
        Vector3 endPos = target.position;

        Quaternion startRot = Object.rotation; //https://answers.unity.com/questions/195895/quaternionslerp-in-a-coroutine.html
        Quaternion endRot = target.rotation;

        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            Object.SetPositionAndRotation(Vector3.Lerp(startPos, endPos, (elapsedTime / time)), 
                                          Quaternion.Slerp(startRot, endRot, (elapsedTime / time)));
            yield return null;
        }

        Object.parent = target;
    }
}
