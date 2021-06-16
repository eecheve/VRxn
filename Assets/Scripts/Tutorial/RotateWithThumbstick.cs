using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RotateWithThumbstick : MonoBehaviour
{
    public UnityEvent OnRotatedWithThumbstick;
    
    private ActionTutorial tutorial;

    private void OnEnable()
    {
        tutorial = GetComponent<ActionTutorial>();
        tutorial.OnAnyActionPerformed += RotatedWithThumbstick;
    }

    private void RotatedWithThumbstick()
    {
        OnRotatedWithThumbstick?.Invoke();
    }

    private void OnDisable()
    {
        tutorial.OnAnyActionPerformed -= RotatedWithThumbstick;
    }
}
