using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimationHelper : MonoBehaviour
{
    private Transform animatable;

    private Vector3 initialPos;
    private Quaternion initialRot;

    private Coroutine rotatingOnX;
    private Coroutine rotatingOnY;
    private Coroutine rotatingOnZ;

    private void Awake()
    {
        animatable = CurrentSceneManager.Instance.Animatable;

        initialPos = animatable.position;
        initialRot = animatable.rotation;
    }

    public void RotateOnX(float duration)
    {
        rotatingOnX = StartCoroutine(RotateX(duration));
    }

    public void RotateOnY(float duration)
    {
        rotatingOnY = StartCoroutine(RotateY(duration));
    }

    public void RotateOnZ(float duration)
    {
        rotatingOnZ = StartCoroutine(RotateZ(duration));
    }

    public void StopRotateOnX() //https://answers.unity.com/questions/300864/how-to-stop-a-co-routine-in-c-instantly.html
    {
        StopCoroutine(rotatingOnX);
    }

    public void StopRotateOnY()
    {
        StopCoroutine(rotatingOnY);
    }

    public void StopRotateOnZ()
    {
        StopCoroutine(rotatingOnZ);
    }

    private IEnumerator RotateX(float duration)
    {
        float startRotation = animatable.eulerAngles.x;
        float endRotation = startRotation + 360.0f;
        float t = 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float xRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
            animatable.eulerAngles = new Vector3(xRotation, animatable.eulerAngles.y, animatable.eulerAngles.z);
            yield return null;
        }
    }

    private IEnumerator RotateY(float duration)
    {
        float startRotation = animatable.eulerAngles.y;
        float endRotation = startRotation + 360.0f;
        float t = 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
            animatable.eulerAngles = new Vector3(animatable.eulerAngles.x, yRotation, animatable.eulerAngles.z);
            yield return null;
        }
    }

    private IEnumerator RotateZ(float duration)
    {
        float startRotation = animatable.eulerAngles.z;
        float endRotation = startRotation + 360.0f;
        float t = 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float zRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
            animatable.eulerAngles = new Vector3(animatable.eulerAngles.x, animatable.eulerAngles.y, zRotation);
            yield return null;
        }
    }

    public void ResetTransform()
    {
        animatable.position = initialPos;
        animatable.rotation = initialRot;
    }
}
