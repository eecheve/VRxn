using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuestControllerAnimation : MonoBehaviour
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
        m_animator.SetFloat("thumbstick_horizontal", obj.ReadValue<Vector2>().x);
        m_animator.SetFloat("thumbstick_vertical", obj.ReadValue<Vector2>().y);
    }

    private void AnimateSecondaryButton(InputAction.CallbackContext obj)
    {
        m_animator.SetFloat("secondaryButton", obj.ReadValue<float>());
    }

    private void AnimatePrimaryButton(InputAction.CallbackContext obj)
    {
        m_animator.SetFloat("primaryButton", obj.ReadValue<float>());
    }

    private void AnimateGrip(InputAction.CallbackContext obj)
    {
        m_animator.SetFloat("sideTrigger", obj.ReadValue<float>());
    }

    private void AnimateTrigger(InputAction.CallbackContext obj)
    {
        m_animator.SetFloat("indexTrigger", obj.ReadValue<float>());
    }

    private void OnDisable()
    {
        trigger.action.performed -= AnimateTrigger;
        grip.action.performed -= AnimateGrip;
        primaryButton.action.performed -= AnimatePrimaryButton;
        secondaryButton.action.performed -= AnimateSecondaryButton;
        thumbstick.action.performed -= AnimateThumbstick;
    }
}
