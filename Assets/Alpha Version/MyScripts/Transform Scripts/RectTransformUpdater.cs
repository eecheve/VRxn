using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectTransformUpdater : MonoBehaviour
{
    [SerializeField] private RectTransform toModify = null;
    [SerializeField] private RectTransform previusPoint = null;
    [SerializeField] private RectTransform currentPoint = null;
    [SerializeField] private RectTransform nextPoint = null;

    public void MoveToPreviousPosition()
    {
        toModify.position = previusPoint.position;
        toModify.rotation = previusPoint.rotation;
    }

    public void MoveToCurrentPosition()
    {
        toModify.position = currentPoint.position;
        toModify.rotation = currentPoint.rotation;
    }

    public void MoveToNextPosition()
    {
        toModify.position = nextPoint.position;
        toModify.rotation = nextPoint.rotation;
    }
}
