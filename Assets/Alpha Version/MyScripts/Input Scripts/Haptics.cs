using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Haptics : MonoBehaviour
{
    [SerializeField] private float m_strength = 1.0f;
    [SerializeField] private float m_length = 0.1f;

    private XRController controller;
    private HapticCapabilities hapCap = new HapticCapabilities();

    private bool canVibrate = false;

    private void Awake()
    {
        controller = GetComponent<XRController>();

        CheckIfCanVibrate();
    }


    private void CheckIfCanVibrate()
    {
        controller.inputDevice.TryGetHapticCapabilities(out hapCap);

        if (hapCap.supportsImpulse)
        {
            canVibrate = true;
        }
        else
            canVibrate = false;
    }

    public void Vibrate()
    {
        if (canVibrate == true)
        {
            controller.inputDevice.SendHapticImpulse(0, m_strength, m_length);
        }
    }

    public void Vibrate(float strength, float length)
    {
        if (canVibrate == true)
        {
            controller.inputDevice.SendHapticImpulse(0, strength, length);
        }
    }
}
