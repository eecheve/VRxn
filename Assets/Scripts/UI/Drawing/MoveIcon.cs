using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveIcon : MonoBehaviour
{
    private GrabAndRotate leftGrab;
    private GrabAndRotate rightGrab;

    private void Awake()
    {
        leftGrab = ControllerReferences.Instance.LUIController.GetComponent<GrabAndRotate>();
        rightGrab = ControllerReferences.Instance.RUIController.GetComponent<GrabAndRotate>();
    }

    public void EnableMoveAndRotate()
    {
        leftGrab.RotationEnabled = true;
        rightGrab.RotationEnabled = true;
    }

    public void DisableMoveAndRotate()
    {
        leftGrab.RotationEnabled = false;
        rightGrab.RotationEnabled = false;
    }
}
