using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRInteractorLineVisual))]
public class DistanceGrabLineVisual : MonoBehaviour
{
    [SerializeField] private Gradient grabAllowedGradient = null;
    [SerializeField] private Gradient grabbedGradient = null;
    [SerializeField] private LayerMask grabLayerMask = 0;
    [SerializeField] private InputActionReference triggerAction = null;
    [SerializeField] private InputActionReference dGrabAction = null;

    private XRInteractorLineVisual lineVisual;
    private Gradient initialGradient;

    private bool raycastDetected = false;
    private bool distanceGrabbed = false;

    private void Awake()
    {
        lineVisual = GetComponent<XRInteractorLineVisual>();
        initialGradient = lineVisual.invalidColorGradient;
    }

    /*private void OnEnable()
    {
        dGrabAction.action.performed += UpdateTransformFromInput;
        triggerAction.action.performed += DistanceGrab;
    }*/

    private void DistanceGrab(InputAction.CallbackContext obj)
    {
        if (raycastDetected == true && obj.ReadValue<float>() > 0)
        {
            Debug.Log("DistanceGrabbedLineVisual: trigger held");
            
            if(obj.ReadValue<float>() > 0.5f && distanceGrabbed == false)
            {
                Debug.Log("DistanceGrabbedLineVisual: should change visual color");
                distanceGrabbed = true;
                lineVisual.invalidColorGradient = grabbedGradient;
            }
            else if(obj.ReadValue<float>() < 0.1f && distanceGrabbed == true)
            {
                Debug.Log("DistanceGrabbedLineVisual: should revert to original color");
                distanceGrabbed = false;
                lineVisual.invalidColorGradient = grabAllowedGradient;
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

            if(lineVisual.invalidColorGradient != grabAllowedGradient && sqrMagnitude > 1.0f)
            {
                lineVisual.invalidColorGradient = grabAllowedGradient;
                raycastDetected = true;
            }
        }
        else
        {
            if (lineVisual.invalidColorGradient != initialGradient)
            {
                lineVisual.invalidColorGradient = initialGradient;
                raycastDetected = false;
            }
        }
    }

    /*private void OnDisable()
    {
        dGrabAction.action.performed -= UpdateTransformFromInput;
        triggerAction.action.performed -= DistanceGrab;
    }*/
}
