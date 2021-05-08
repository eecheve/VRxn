using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(LineRenderer))]
public class DistanceGrabLineVisual : MonoBehaviour
{ 
    [Header("Player Attributes")]
    [SerializeField] private DataManager dataManager = null;
    [SerializeField] private float attractionSpeed = 2.0f;

    [Header("Color Attributes")]
    [SerializeField] private Material outlinedMat = null;
    [SerializeField] private Gradient grabAllowedGradient = null;
    [SerializeField] private Gradient grabbedGradient = null;

    [Header("Action Attributes")]
    [SerializeField] private LayerMask grabLayerMask = 0;
    [SerializeField] private InputActionReference triggerAction = null;
    [SerializeField] private InputActionReference dGrabAction = null;

    private LineRenderer lineRenderer;
    private RaycastHit hit;
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
        triggerAction.action.performed += DistanceGrab;
        dGrabAction.action.performed += UpdateTransformFromInput;
    }

    private void DistanceGrab(InputAction.CallbackContext obj)
    {
        if (raycastDetected == true && obj.ReadValue<float>() > 0)
        {           
            if(obj.ReadValue<float>() > 0.6f)
            {
                if (lineRenderer.colorGradient != grabbedGradient)
                {
                    lineRenderer.colorGradient = grabbedGradient;
                    //hit.collider.gameObject.GetComponent<MeshRenderer>().material = outlinedMat;
                    distanceGrabbed = true;
                }
            }
            else
            {
                if (lineRenderer.colorGradient != grabAllowedGradient)
                {
                    lineRenderer.colorGradient = grabAllowedGradient;

                    string objName = hit.collider.gameObject.name;
                    string elem = objName.RemoveDigits();
                    elem = elem.RemoveSpecialChars();

                    foreach (var entry in dataManager.MaterialsDict)
                    {
                        if (elem.Equals(entry.Key))
                        {
                            Debug.Log("DistanceGrabLineVisual_DistanceGrab: found material with same name");
                            //hit.collider.gameObject.GetComponent<MeshRenderer>().material = entry.Value;
                            break;
                        }
                    }
                    distanceGrabbed = false;
                }
            }
        }
    }

    private void UpdateTransformFromInput(InputAction.CallbackContext obj)
    {
        if(raycastDetected == true && distanceGrabbed == true)
        {
            GameObject objectHit = hit.collider.gameObject;

            if(objectHit != null)
            {
                Vector2 input = obj.ReadValue<Vector2>();
                if(input.y < -0.8f)
                {
                    objectHit.transform.position -= (transform.forward * attractionSpeed * Time.deltaTime);
                }
            }
            
            string yValue = obj.ReadValue<Vector2>().y.ToString();
            Debug.Log("DistanceGrabLineVisual_DistanceGrab: yValue is " + yValue);
        }
    }

    private void Update()
    {
        ManageColorLine();
    }

    private void ManageColorLine()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if(Physics.Raycast(ray, out hit, 100f, grabLayerMask))
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
                    raycastDetected = false;
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
        triggerAction.action.performed -= DistanceGrab;
        dGrabAction.action.performed -= UpdateTransformFromInput;
    }
}
