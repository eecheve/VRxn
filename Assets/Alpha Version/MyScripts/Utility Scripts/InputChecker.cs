using UnityEngine;
using UnityEngine.XR;

public abstract class InputChecker : MonoBehaviour
{
    public InputFeatureUsage<Vector2> TouchPad { get; } = CommonUsages.primary2DAxis;
    public InputFeatureUsage<float> Trigger { get; } = CommonUsages.trigger;
    public InputFeatureUsage<float> Grip { get; } = CommonUsages.grip;
    public InputFeatureUsage<bool> PrimaryButton { get; } = CommonUsages.primaryButton;
    public InputFeatureUsage<bool> PrimaryTouch { get; } = CommonUsages.primaryTouch;
    public InputFeatureUsage<bool> SecondaryButton { get; } = CommonUsages.secondaryButton;
    public InputFeatureUsage<bool> SecondaryTouch { get; } = CommonUsages.secondaryTouch;
    public InputFeatureUsage<bool> GripPress { get; } = CommonUsages.gripButton;
    public InputFeatureUsage<bool> TriggerPress { get; } = CommonUsages.triggerButton;
    public InputFeatureUsage<bool> TouchpadButton { get; } = CommonUsages.primary2DAxisClick;
    public InputFeatureUsage<bool> TouchpadTouch { get; } = CommonUsages.primary2DAxisTouch;
   
    public virtual bool CheckForInputPressed(InputDevice device, InputFeatureUsage<bool> input)
    {
        device.TryGetFeatureValue(input, out bool value);
        return value;
    }

    public virtual float CheckForInputPressed(InputDevice device, InputFeatureUsage<float> input)
    {
        device.TryGetFeatureValue(input, out float value);
        return value;
    }

    public virtual Vector2 CheckForInputPressed(InputDevice device, InputFeatureUsage<Vector2> input)
    {
        device.TryGetFeatureValue(input, out Vector2 value);
        return value;
    }
}
