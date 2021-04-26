using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.XR.Interaction.Toolkit;

public class InputHandler : MonoSingleton<InputHandler>
{
    /*[Serializable] public struct ControllerReference
    {
        public InternedString input;
        public InputActionReference reference;
    }*/

    [SerializeField] InputActionReference[] triggerReferences = null; //0 left bool, 1 right bool, 2 left float, 3 right float 
    [SerializeField] InputActionReference[] gripReferences = null; //0 left bool, 1 right bool, 2 left float, 3 right float
    //[SerializeField] InputActionReference[] primaryButtonReferences = null; //0 left bool, 1 right bool
    //[SerializeField] InputActionReference[] secondaryButtonReferences = null; //0 left bool, 1 right bool
    //[SerializeField] InputActionReference[] thumbstickReferences = null; //0 left bool, 1 right bool, 2 left vector2, 3 right vector2

    public InputActionReference LeftTriggerPress { get { return triggerReferences[0]; } private set { triggerReferences[0] = value; } }
    public InputActionReference RightTriggerPress { get { return triggerReferences[1]; } private set { triggerReferences[1] = value; } }
    public InputActionReference LeftTriggerHold { get { return triggerReferences[2]; } private set { triggerReferences[2] = value; } }
    public InputActionReference RightTriggerHold { get { return triggerReferences[3]; } private set { triggerReferences[3] = value; } }
    public InputActionReference LeftGripPress { get { return gripReferences[0]; } private set { gripReferences[0] = value; } }
    public InputActionReference RightGripPress { get { return gripReferences[1]; } private set { gripReferences[1] = value; } }
    public InputActionReference LeftGripHold { get { return gripReferences[2]; } private set { gripReferences[2] = value; } }
    public InputActionReference RightGripHold { get { return gripReferences[3]; } private set { gripReferences[3] = value; } }

    private void OnEnable()
    {
        LeftTriggerPress.action.performed += LeftTriggerPressed;
        LeftTriggerHold.action.performed += LeftTriggerHeld;
    }

    private void LeftTriggerHeld(InputAction.CallbackContext obj)
    {
        Debug.Log("LeftTriggerHeld is " + obj.ReadValue<float>().ToString());
    }

    private void LeftTriggerPressed(InputAction.CallbackContext obj)
    {
        Debug.Log("LeftTriggerPressed is " + obj.ReadValue<bool>().ToString());
    }
}
