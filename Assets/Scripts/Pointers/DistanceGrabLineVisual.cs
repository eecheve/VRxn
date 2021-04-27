using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(LineRenderer))]
public class DistanceGrabLineVisual : MonoBehaviour
{
    [SerializeField] private ActionBasedSnapTurnProvider snapTurnProvider = null;
    [SerializeField] private Gradient grabAllowedGradient = null;
    [SerializeField] private Gradient grabbedGradient = null;
    [SerializeField] private LayerMask grabLayerMask = 0;
    [SerializeField] private InputActionReference triggerAction = null;
    [SerializeField] private InputActionReference dGrabAction = null;

    private LineRenderer lineRenderer;
    private bool raycastDetected = false;
    private bool distanceGrabbed = false;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.colorGradient = grabAllowedGradient;
        lineRenderer.enabled = false;
    }

    private void OnEnable()
    {
        //dGrabAction.action.performed += UpdateTransformFromInput;
        triggerAction.action.performed += DistanceGrab;
    }

    private void DistanceGrab(InputAction.CallbackContext obj)
    {
        if (raycastDetected == true && obj.ReadValue<float>() > 0)
        {
            Debug.Log("DistanceGrabbedLineVisual: trigger held");
            if (snapTurnProvider.enabled == true)
                snapTurnProvider.enabled = false;
            
            if(obj.ReadValue<float>() > 0.5f && distanceGrabbed == false)
            {
                Debug.Log("DistanceGrabbedLineVisual: should change visual color");
                if(lineRenderer.colorGradient != grabbedGradient)
                    lineRenderer.colorGradient = grabbedGradient;

                distanceGrabbed = true;
            }
            else if(obj.ReadValue<float>() < 0.1f && distanceGrabbed == true)
            {
                Debug.Log("DistanceGrabbedLineVisual: should revert to original color");
                if (lineRenderer.colorGradient != grabAllowedGradient)
                    lineRenderer.colorGradient = grabAllowedGradient;

                if (snapTurnProvider.enabled == false)
                    snapTurnProvider.enabled = true;

                distanceGrabbed = false;
            }
        }
    }

    private void UpdateTransformFromInput(InputAction.CallbackContext obj)
    {
        if(raycastDetected == true)
        {
            Debug.Log(obj.ReadValue<Vector2>().y.ToString());
        }
    }

    private void Update()
    {
        ManageColorLine();
    }

    private void ManageColorLine()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if(Physics.Raycast(ray, out RaycastHit hit, 100f, grabLayerMask))
        {
            Vector3 refVector = hit.transform.position - transform.position;
            float sqrMagnitude = refVector.sqrMagnitude;

            if (sqrMagnitude > 1.0f)
            {
                if (lineRenderer.enabled == false)
                {
                    lineRenderer.enabled = true;
                }

                raycastDetected = true;
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, hit.transform.position);
            }
            else
            {
                if (lineRenderer.enabled == true)
                {
                    lineRenderer.enabled = false;
                }
            }
        }
        else
        {
            if (lineRenderer.enabled == true)
            {
                lineRenderer.enabled = false;
                raycastDetected = false;
            }
        }
    }

    private void OnDisable()
    {
        //dGrabAction.action.performed -= UpdateTransformFromInput;
        triggerAction.action.performed -= DistanceGrab;
    }
}
