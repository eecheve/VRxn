using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ControllerAnimation : MonoBehaviour
{
    [SerializeField] private Animator m_animator = null;

    [SerializeField] private InputActionReference trigger = null;
    [SerializeField] private InputActionReference grip = null;
    [SerializeField] private InputActionReference primaryButton = null;
    [SerializeField] private InputActionReference secondaryButton = null;
    [SerializeField] private InputActionReference thumbstick = null;

    private void OnEnable()
    {
        trigger.action.performed += AnimateTrigger;
        grip.action.performed += AnimateGrip;
        primaryButton.action.performed += AnimatePrimaryButton;
        secondaryButton.action.performed += AnimateSecondaryButton;
        thumbstick.action.performed += AnimateThumbstick;
    }

    private void AnimateThumbstick(InputAction.CallbackContext obj)
    {
        m_animator.SetFloat("Joy X", obj.ReadValue<Vector2>().x);
        m_animator.SetFloat("Joy Y", obj.ReadValue<Vector2>().y);
    }

    private void AnimateSecondaryButton(InputAction.CallbackContext obj)
    {
        m_animator.SetFloat("Button 2", obj.ReadValue<float>());
    }

    private void AnimatePrimaryButton(InputAction.CallbackContext obj)
    {
        m_animator.SetFloat("Button 1", obj.ReadValue<float>());
    }

    private void AnimateGrip(InputAction.CallbackContext obj)
    {
        m_animator.SetFloat("Grip", obj.ReadValue<float>());
    }

    private void AnimateTrigger(InputAction.CallbackContext obj)
    {
        m_animator.SetFloat("Trigger", obj.ReadValue<float>());
    }
}
