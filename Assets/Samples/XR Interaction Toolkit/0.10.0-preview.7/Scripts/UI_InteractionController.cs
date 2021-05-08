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

    bool isUICanvasActive = false;
    private bool uiPointerActive = false;

    private void Start()
    {
        UIController.GetComponent<XRRayInteractor>().enabled = false;
        UIController.GetComponent<XRInteractorLineVisual>().enabled = false;
    }

    private void Update()
    {
        DetectUILayerMask();
    }

    private void DetectUILayerMask()
    {
        Ray ray = new Ray(baseController.transform.position, baseController.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, UILayerMask))
        {
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
}
