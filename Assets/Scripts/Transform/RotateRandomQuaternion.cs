using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRandomQuaternion : MonoBehaviour
{
    [SerializeField] private Transform m_object = null;
    [SerializeField] private float m_time = 1.0f;

    public void RotateRandom()
    {
        StartCoroutine(Rotate(m_time));
    }

    private IEnumerator Rotate(float time)
    {
        Quaternion startRot = m_object.rotation;
        Quaternion endRot = Random.rotation;

        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            m_object.rotation = Quaternion.Slerp(startRot, endRot, (elapsedTime / time));
            yield return null;
        }
    }

}
