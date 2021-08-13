using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

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
        Debug.Log(name + " Animating thumbstick");

        SetAnimatorFloat("thumbstick_horizontal", obj.ReadValue<Vector2>().x);
        SetAnimatorFloat("thumbstick_vertical", obj.ReadValue<Vector2>().y);
        //m_animator.Play()
    }

    private void AnimateSecondaryButton(InputAction.CallbackContext obj)
    {
        Debug.Log(name + " Animating secondary button");

        SetAnimatorFloat("secondaryButton", obj.ReadValue<float>());
    }

    private void AnimatePrimaryButton(InputAction.CallbackContext obj)
    {
        Debug.Log(name + " Animating primary button");

        SetAnimatorFloat("primaryButton", obj.ReadValue<float>());
    }

    private void AnimateGrip(InputAction.CallbackContext obj)
    {
        Debug.Log(name + " Animating grip button");

        SetAnimatorFloat("primaryButton", obj.ReadValue<float>());
    }

    private void AnimateTrigger(InputAction.CallbackContext obj)
    {
        //if (obj.ReadValue<float>() > 0)
        //{
        //    Debug.Log(name + "Trying to animate trigger with " + obj.ReadValue<float>());
        //}
        Debug.Log(name + " Animating trigger button");
        try
        {
            SetAnimatorFloat("indexTrigger", obj.ReadValue<float>());
        }
        catch (System.Exception)
        {
            Debug.Log("QuestControllerAnimation: index trigger parameter not found");
            throw;
        }
    }

    private void SetAnimatorFloat(string parameter, float value)
    {
        if(value > 0.1f)
            m_animator.SetFloat(parameter, value);
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
