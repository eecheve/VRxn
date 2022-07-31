using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandPrefabAnimator : MonoBehaviour
{
    [SerializeField] private Animator m_animator = null;

    [SerializeField] private InputActionReference trigger = null;
    [SerializeField] private InputActionReference grip = null;

    private void OnEnable()
    {
        trigger.action.performed += UpdateGrip;
        grip.action.performed += UpdateTrigger;
    }

    private void UpdateTrigger(InputAction.CallbackContext obj)
    {
        SetAnimatorFloat("Trigger", obj.ReadValue<float>());
    }

    private void UpdateGrip(InputAction.CallbackContext obj)
    {
       SetAnimatorFloat("Grip", obj.ReadValue<float>());
    }

    private void SetAnimatorFloat(string parameter, float value)
    {
        if (value > 0.1f)
            m_animator.SetFloat(parameter, value);
    }

    private void OnDisable()
    {
        trigger.action.performed -= UpdateGrip;
        grip.action.performed -= UpdateTrigger;
    }
}
