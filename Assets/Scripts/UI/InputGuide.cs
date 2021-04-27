using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Canvas))]
public class InputGuide : MonoBehaviour
{
    [SerializeField] private InputActionReference inputButton = null;
    [SerializeField] private Transform headsetOffset = null;
    [SerializeField] private Transform headset = null;

    private bool activeCanvas = false;
    private Canvas canvas;

    private void OnEnable()
    {
        inputButton.action.performed += ActivateCanvas;
        canvas = GetComponent<Canvas>();
    }

    private void ActivateCanvas(InputAction.CallbackContext obj)
    {
        if(activeCanvas == false)
        {
            activeCanvas = true;
            canvas.enabled = true;
        }
        else
        {
            activeCanvas = false;
            canvas.enabled = false;
        }
    }

    private void Update()
    {
        if (activeCanvas == true)
        {
            //Vector3 refPos = headsetOffset.position;
            //Vector3 refRot = new Vector3(refPos.x, 0f, refPos.z);
            transform.position = headsetOffset.position;
            transform.rotation = Quaternion.LookRotation(headsetOffset.position);
        }
    }

    private void OnDisable()
    {
        inputButton.action.performed -= ActivateCanvas;
    }
}
