using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable] public class ButtonPressEvent : UnityEvent<bool> { }
[System.Serializable] public class ButtonHoldEvent : UnityEvent<bool> { }
public abstract class ButtonWatcher : MonoBehaviour
{
    protected InputDevice leftDevice;
    protected InputDevice rightDevice;

    protected virtual void OnEnable()
    {
        //leftDevice = GameManager.Instance.LeftControllerRef.inputDevice;
        //rightDevice = GameManager.Instance.RightControllerRef.inputDevice;
    }

    protected virtual void ManageSustainedPress(InputDevice device, 
        InputFeatureUsage<float> heldInput, ButtonHoldEvent onButtonHold)
    {
        bool tempState = device.TryGetFeatureValue(heldInput, out float buttonState) && buttonState > 0.6f;

        if (tempState == true)
        {
            onButtonHold.Invoke(tempState);
            Debug.Log("ButtonWatcher_ManageSustainedPress: Button is held, override 1");
        }
    }

    protected virtual void ManageSustainedPress(InputDevice device,
        InputFeatureUsage<bool> heldInput, ButtonHoldEvent onButtonHold)
    {
        bool tempState = device.TryGetFeatureValue(heldInput, out bool buttonState) && buttonState;

        if (tempState == true)
        {
            onButtonHold.Invoke(tempState);
            Debug.Log("ButtonWatcher_ManageSustainedPress: Button is held, override 2");
        }
    }

    public Vector2 GetInput(InputDevice device, InputFeatureUsage<Vector2> input)
    {
        device.TryGetFeatureValue(input, out Vector2 value);
        return value;
    }

    public float GetInput(InputDevice device, InputFeatureUsage<float> input)
    {
        device.TryGetFeatureValue(input, out float value);
        return value;
    }

    public bool GetInput(InputDevice device, InputFeatureUsage<bool> input)
    {
        device.TryGetFeatureValue(input, out bool value);
        return value;
    }

    protected virtual void ManageSinglePress(InputDevice device, bool lastButtonState, 
        InputFeatureUsage<bool> pressedInput, ButtonPressEvent onButtonPress)
    {
        bool tempState = device.TryGetFeatureValue(pressedInput, out bool buttonState) && buttonState;

        if (tempState != lastButtonState)
        {
            onButtonPress.Invoke(tempState);
            //lastButtonState = tempState;
            Debug.Log("ButtonWatcher_ManageSustainedPress: Button is pressed");
        }
    }
}
