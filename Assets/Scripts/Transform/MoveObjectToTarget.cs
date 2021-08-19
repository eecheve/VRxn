using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectToTarget : MonoBehaviour
{
    [SerializeField] private Transform m_object = null;
    [SerializeField] private Transform m_target = null;
    [SerializeField] private float m_time = 1.0f;

    public void MoveToTarget()
    {
        if (m_object != null && m_target != null)
        {
            StartCoroutine(SmoothMovement(m_time));
        }
    }

    private IEnumerator SmoothMovement(float time) //https://answers.unity.com/questions/1501234/smooth-forward-movement-with-a-coroutine.html
    {
        Vector3 startPos = m_object.position;
        Vector3 endPos = m_target.position;

        Quaternion startRot = m_object.rotation; //https://answers.unity.com/questions/195895/quaternionslerp-in-a-coroutine.html
        Quaternion endRot = m_target.rotation;

        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            m_object.SetPositionAndRotation(Vector3.Lerp(startPos, endPos, (elapsedTime / time)),
                                          Quaternion.Slerp(startRot, endRot, (elapsedTime / time)));
            yield return null;
        }
    }
}
