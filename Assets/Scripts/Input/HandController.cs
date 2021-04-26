using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using static Enumerators;

public class HandController : MonoBehaviour
{
    [SerializeField] private InputActionReference triggerRef = null;
    [SerializeField] private InputActionReference gripRef = null;

    private Animator animator;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        triggerRef.action.performed += TriggerPressed;
        gripRef.action.performed += GripPressed;
    }

    private void GripPressed(InputAction.CallbackContext obj)
    {
        animator.SetFloat("Grip", obj.ReadValue<float>());
    }

    private void TriggerPressed(InputAction.CallbackContext obj)
    {
        animator.SetFloat("Trigger", obj.ReadValue<float>());
    }

    private void OnDisable()
    {
        triggerRef.action.performed -= TriggerPressed;
        gripRef.action.performed -= GripPressed;
    }
}
