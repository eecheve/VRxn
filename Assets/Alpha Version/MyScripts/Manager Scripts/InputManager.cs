using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class InputManager : MonoSingleton<InputManager>
{
    [SerializeField] private GameObject leftHandControllerObj = null;
    [SerializeField] private GameObject rightHandControllerObj = null;

    public XRController LeftController { get; private set; }
    public XRController RightController { get; private set; }
    public InputDevice LeftDevice { get; private set; }
    public InputDevice RightDevice { get; private set; }
    public InputFeatureUsage<Vector2> TouchPad { get; } = CommonUsages.primary2DAxis;
    public InputFeatureUsage<float> Trigger { get; } = CommonUsages.trigger;
    public InputFeatureUsage<float> Grip { get; } = CommonUsages.grip;
    public InputFeatureUsage<bool> PrimaryButton { get; } = CommonUsages.primaryButton;
    public InputFeatureUsage<bool> PrimaryTouch { get; } = CommonUsages.primaryTouch;
    public InputFeatureUsage<bool> SecondaryButton { get; } = CommonUsages.secondaryButton;
    public InputFeatureUsage<bool> SecondaryTouch { get; } = CommonUsages.secondaryTouch;
    public InputFeatureUsage<bool> GripButton { get; } = CommonUsages.gripButton;
    public InputFeatureUsage<bool> TriggerButton { get; } = CommonUsages.triggerButton;
    public InputFeatureUsage<bool> TouchpadButton { get; } = CommonUsages.primary2DAxisClick;
    public InputFeatureUsage<bool> TouchpadTouch { get; } = CommonUsages.primary2DAxisTouch;

    private void OnEnable()
    {
        LeftController = leftHandControllerObj.GetComponent<XRController>();
        RightController = rightHandControllerObj.GetComponent<XRController>();

        LeftDevice = LeftController.inputDevice;
        RightDevice = RightController.inputDevice;
    }

    public bool CheckForInputPressed(InputDevice device, InputFeatureUsage<bool> input)
    {
        device.TryGetFeatureValue(input, out bool value);
        return value;
    }

    public bool CheckForInputPressedOnce(InputDevice device, InputFeatureUsage<bool> input)
    {
        bool lastButtonState = false;

        bool tempState = device.TryGetFeatureValue(input, out bool buttonState) && buttonState;

        if (tempState != lastButtonState)
        {
            lastButtonState = tempState;
            return true;
        }
        else
            return false;
    }

    public float CheckForInputPressed(InputDevice device, InputFeatureUsage<float> input)
    {
        device.TryGetFeatureValue(input, out float value);
        return value;
    }

    public Vector2 CheckForInputPressed(InputDevice device, InputFeatureUsage<Vector2> input)
    {
        device.TryGetFeatureValue(input, out Vector2 value);
        return value;
    }
}
