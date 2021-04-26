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
    [SerializeField] InputActionReference[] primaryButtonReferences = null; //0 left bool, 1 right bool
    [SerializeField] InputActionReference[] secondaryButtonReferences = null; //0 left bool, 1 right bool
    [SerializeField] InputActionReference[] thumbstickReferences = null; //0 left bool, 1 right bool, 2 left vector2, 3 right vector2

    #region trigger and grip
    public InputActionReference LeftTriggerPress { get { return triggerReferences[0]; } private set { triggerReferences[0] = value; } }
    public InputActionReference RightTriggerPress { get { return triggerReferences[1]; } private set { triggerReferences[1] = value; } }
    public InputActionReference LeftTriggerHold { get { return triggerReferences[2]; } private set { triggerReferences[2] = value; } }
    public InputActionReference RightTriggerHold { get { return triggerReferences[3]; } private set { triggerReferences[3] = value; } }
    public InputActionReference LeftGripPress { get { return gripReferences[0]; } private set { gripReferences[0] = value; } }
    public InputActionReference RightGripPress { get { return gripReferences[1]; } private set { gripReferences[1] = value; } }
    public InputActionReference LeftGripHold { get { return gripReferences[2]; } private set { gripReferences[2] = value; } }
    public InputActionReference RightGripHold { get { return gripReferences[3]; } private set { gripReferences[3] = value; } }
    #endregion
    #region primary and secondary
    public InputActionReference LeftPrimaryButton { get { return primaryButtonReferences[0]; } private set { primaryButtonReferences[0] = value; } }
    public InputActionReference RightPrimaryButton { get { return primaryButtonReferences[1]; } private set { primaryButtonReferences[1] = value; } }
    #endregion
    #region thumbstick
    public InputActionReference LeftThumbstickPress { get { return thumbstickReferences[0]; } private set { thumbstickReferences[0] = value; } }
    public InputActionReference RightThumbstickPress { get { return thumbstickReferences[1]; } private set { thumbstickReferences[1] = value; } }
    public InputActionReference LeftThumbstickMove { get { return thumbstickReferences[2]; } private set { thumbstickReferences[2] = value; } }
    public InputActionReference RightThumbstickMove { get { return thumbstickReferences[3]; } private set { thumbstickReferences[3] = value; } }
    #endregion

    private void OnEnable()
    {
        //LeftTriggerPress.action.performed += LeftTriggerPressedTest;
        //LeftTriggerHold.action.performed += LeftTriggerHeld;
        //LeftThumbstickMove.action.performed += LeftThumbstickTest;
    }

    private void LeftTriggerTest(InputAction.CallbackContext obj)
    {
        Debug.Log("InputHandler: LeftTriggerHeld is " + obj.ReadValue<float>().ToString());
    }

    private void LeftTriggerPressedTest(InputAction.CallbackContext obj)
    {
        Debug.Log("LeftTriggerPressed is pressed");
    }

    private void LeftThumbstickTest(InputAction.CallbackContext obj)
    {
        Debug.Log("InputHandler: Left test is (" + obj.ReadValue<Vector2>().x.ToString() + "," + obj.ReadValue<Vector2>().y.ToString());
    }
}
