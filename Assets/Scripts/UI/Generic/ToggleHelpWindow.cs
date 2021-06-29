using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleHelpWindow : MonoBehaviour
{
    [SerializeField] private InputActionReference toggleHelp = null;
    [SerializeField] private PageHandler pageHandler = null;

    private void OnEnable()
    {
        toggleHelp.action.performed += ToggleHelp;
    }

    private void ToggleHelp(InputAction.CallbackContext obj)
    {
        Debug.Log("ToggleHelpWindow: " + obj.ReadValue<float>().ToString());
        if(obj.ReadValue<float>() > 0.5)
        {
            pageHandler.GoToNextOrFirst();
        }
    }

    private void OnDisable()
    {
        toggleHelp.action.performed -= ToggleHelp;
    }
}
