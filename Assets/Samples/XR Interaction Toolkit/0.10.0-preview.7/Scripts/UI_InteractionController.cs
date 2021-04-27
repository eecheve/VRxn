using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;

public class UI_InteractionController : MonoBehaviour
{
    [SerializeField] private GameObject UIController = null;
    [SerializeField] private GameObject baseController = null;
    [SerializeField] private LayerMask UILayerMask = 0;
    [SerializeField] private InputActionReference inputActionReference_UISwitcher = null;

    bool isUICanvasActive = false;
    private bool uiPointerActive = false;

    [SerializeField] private GameObject UICanvasGameobject = null;
    [SerializeField] private Vector3 positionOffsetForUICanvasGameobject = Vector3.zero;

    private void OnEnable()
    {
        inputActionReference_UISwitcher.action.performed += ActivateUIMode;
    }
    
    private void Start()
    {
        //Deactivating UI Canvas Gameobject by default
        UICanvasGameobject.SetActive(false);

        //Deactivating UI Controller by default
        UIController.GetComponent<XRRayInteractor>().enabled = false;
        UIController.GetComponent<XRInteractorLineVisual>().enabled = false;
    }

    private void Update()
    {
        DetectUILayerMask();
    }

    private void DetectUILayerMask()
    {
        Debug.Log("Controller position is " + baseController.transform.position.ToString());
        Ray ray = new Ray(baseController.transform.position, baseController.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, UILayerMask))
        {
            Debug.Log("UIInteractionController: raycast detected");
            if (uiPointerActive == false)
            {
                uiPointerActive = true;
                ActivateUIPointer(true);
            }
        }
        else
        {
            if(uiPointerActive == true)
            {
                uiPointerActive = false;
                ActivateUIPointer(false);
            }
        }
    }

    private void ActivateUIPointer(bool state)
    {
        //Activating UI Controller by enabling its XR Ray Interactor and XR Interactor Line Visual
        UIController.SetActive(state);
        
        //Deactivating Base Controller by disabling its XR Direct Interactor
        baseController.GetComponent<XRDirectInteractor>().enabled = !state;
    }

    /// <summary>
    /// This method is called when the player presses UI Switcher Button which is the input action defined in Default Input Actions.
    /// When it is called, UI interaction mode is switched on and off according to the previous state of the UI Canvas.
    /// </summary>
    /// <param name="obj"></param>
    private void ActivateUIMode(InputAction.CallbackContext obj)
    {
        if (!isUICanvasActive)
        {
            isUICanvasActive = true;

            //Activating UI Controller by enabling its XR Ray Interactor and XR Interactor Line Visual
            //UIController.GetComponent<XRRayInteractor>().enabled = true;
            //UIController.GetComponent<XRInteractorLineVisual>().enabled = true;

            //Deactivating Base Controller by disabling its XR Direct Interactor
            //baseController.GetComponent<XRDirectInteractor>().enabled = false;

            //Adjusting the transform of the UI Canvas Gameobject according to the VR Player transform
            Vector3 positionVec = new Vector3(UIController.transform.position.x, positionOffsetForUICanvasGameobject.y, UIController.transform.position.z);
            Vector3 directionVec = UIController.transform.forward;
            directionVec.y = 0f;
            UICanvasGameobject.transform.position = positionVec + positionOffsetForUICanvasGameobject.magnitude * directionVec;
            UICanvasGameobject.transform.rotation = Quaternion.LookRotation(directionVec);

            //Activating the UI Canvas Gameobject
            UICanvasGameobject.SetActive(true);
        }
        else
        {
            isUICanvasActive = false;

            //De-Activating UI Controller by enabling its XR Ray Interactor and XR Interactor Line Visual
            //UIController.GetComponent<XRRayInteractor>().enabled = false;
            //UIController.GetComponent<XRInteractorLineVisual>().enabled = false;

            //Activating Base Controller by disabling its XR Direct Interactor
            //baseController.GetComponent<XRDirectInteractor>().enabled = true;

            //De-Activating the UI Canvas Gameobject
            UICanvasGameobject.SetActive(false);
        }

    }

    private void OnDisable()
    {
        inputActionReference_UISwitcher.action.performed -= ActivateUIMode;
    }
}
