using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class grabUIHelper : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] private InputActionReference toggleAction = null;
    [SerializeField] private InputActionReference thumbstick = null;

    [Header("UI Attributes")]
    [SerializeField] private Color buttonHighlightColor = Color.white;
    [SerializeField] private Color textHighlightColor = Color.white;
    [SerializeField] private Color inactiveColor = Color.white;
    [SerializeField] private Button distanceButton = null;
    [SerializeField] private TextMeshProUGUI distanceTMesh = null;
    [SerializeField] private Button regularButton = null;
    [SerializeField] private TextMeshProUGUI regularTMesh = null;

    private Canvas canvas;
    private bool canvasState = false;

    private bool distanceGrabState = false;

    private Color distanceTextColor;
    private Color regularTextColor;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();

        distanceTextColor = distanceTMesh.color;
        regularTextColor = regularTMesh.color;
    }

    private void OnEnable()
    {
        toggleAction.action.performed += ToggleUI;
        thumbstick.action.performed += ToggleBetweenGrabSystems;
    }

    private void ToggleUI(InputAction.CallbackContext obj)
    {
        canvasState = !canvasState;
        canvas.enabled = canvasState;
    }

    private void ToggleBetweenGrabSystems(InputAction.CallbackContext obj)
    {
        if(canvasState == true)
        {
            if(obj.ReadValue<Vector2>().y > 0.5)
            {
                distanceButton.image.color = buttonHighlightColor;
                distanceTMesh.color = textHighlightColor;

                regularButton.image.color = inactiveColor;
                regularTMesh.color = regularTextColor;

                if(distanceGrabState == false)
                {
                    Debug.Log("grabUIHelper: should toggle distance grab");
                    distanceGrabState = true;
                    ToggleDistanceGrab();
                }
            }
            else if(obj.ReadValue<Vector2>().y < -0.5)
            {
                distanceButton.image.color = inactiveColor;
                distanceTMesh.color = distanceTextColor;

                regularButton.image.color = buttonHighlightColor;
                regularTMesh.color = textHighlightColor;

                if(distanceGrabState == true)
                {
                    Debug.Log("grabUIHelper: should toggle grab");
                    distanceGrabState = false;
                    ToggleRegularGrab();
                }
            }
        }
    }

    private void ToggleRegularGrab()
    {
        ControllerReferences.Instance.RRayInteractor.gameObject.SetActive(false);
        ControllerReferences.Instance.RDirectInteractor.gameObject.SetActive(true);

        ControllerReferences.Instance.LRayInteractor.gameObject.SetActive(false);
        ControllerReferences.Instance.LDirectInteractor.gameObject.SetActive(true);
    }

    private void ToggleDistanceGrab()
    {
        ControllerReferences.Instance.RRayInteractor.gameObject.SetActive(true);
        ControllerReferences.Instance.RDirectInteractor.gameObject.SetActive(false);

        ControllerReferences.Instance.LRayInteractor.gameObject.SetActive(true);
        ControllerReferences.Instance.LDirectInteractor.gameObject.SetActive(false);
    }



    private void OnDisable()
    {
        toggleAction.action.performed -= ToggleUI;
    }
}
