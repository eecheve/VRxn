using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Enumerators;

public class ActionTutorial : Tutorial
{
    [SerializeField] private List<InputActionReference> actions = new List<InputActionReference>();
    [SerializeField] private ActionOutput actionOutput = ActionOutput.Bool;

    [Header("Thresholds")]
    [SerializeField] private float floatReference = 0;
    [SerializeField] private float vectorHorizontalRef = 0;
    [SerializeField] private float vectorVerticalRef = 0;
    [SerializeField] private float vectorMagnitudeRef = 0;

    public event TutorialCompleted OnAnyActionPerformed;

    private bool performed = false;

    private void OnEnable()
    {
        foreach (var action in actions)
        {
            action.action.performed += CheckForButtonPressed;
        }
    }

    private void CheckForButtonPressed(InputAction.CallbackContext obj)
    {
        if (actionOutput == ActionOutput.Bool)
        {
            if (obj.ReadValue<bool>())
            {
                Debug.Log("ActionTutorial: Action is type bool and was performed");
                performed = true;
            }
        }
        else if (actionOutput == ActionOutput.Float)
        {
            if (obj.ReadValue<float>() > floatReference)
            {
                Debug.Log("ActionTutorial: Action is type float and was performed");
                performed = true;
            }
        }
        else if (actionOutput == ActionOutput.AxisHorizontalRight)
        {
            if (obj.ReadValue<Vector2>().x > vectorHorizontalRef)
            {
                Debug.Log("ActionTutorial: Action is type Vector2.x and was performed");
                performed = true;
            }
        }
        else if(actionOutput == ActionOutput.AxisHorizontalLeft)
        {
            if (obj.ReadValue<Vector2>().x < vectorHorizontalRef)
            {
                Debug.Log("ActionTutorial: Action is type Vector2.x and was performed");
                performed = true;
            }
        }
        else if (actionOutput == ActionOutput.AxisVerticalUp)
        {
            if (obj.ReadValue<Vector2>().y > vectorVerticalRef)
            {
                Debug.Log("ActionTutorial: Action is type Vector2.y and was performed");
                performed = true;
            }
        }
        else if (actionOutput == ActionOutput.AxisVerticalDown)
        {
            if (obj.ReadValue<Vector2>().y < vectorVerticalRef)
            {
                Debug.Log("ActionTutorial: Action is type Vector2.y and was performed");
                performed = true;
            }
        }
        else 
        {
            if (obj.ReadValue<Vector2>().magnitude > vectorMagnitudeRef)
            {
                Debug.Log("ActionTutorial: Action is type Vector2 and was performed");
                performed = true;
            }
        }
    }

    public override void CheckIfHappening()
    {
        if (performed == true)
        {
            TutorialManager.Instance.CompletedTutorial();
            Debug.Log("ActionTutorial: an action tutorial was completed");

            OnAnyActionPerformed?.Invoke();
        }
    }

    private void OnDisable()
    {
        foreach (var action in actions)
        {
            action.action.performed -= CheckForButtonPressed;
        }
    }
}
