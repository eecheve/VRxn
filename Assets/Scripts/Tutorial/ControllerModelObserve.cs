using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControllerModelObserve : MonoBehaviour
{
    public UnityEvent observeEvents;

    private ConditionTutorial tutorial;

    private void OnEnable()
    {
        tutorial = GetComponent<ConditionTutorial>();
        tutorial.OnConditionSetCompleted += ControllerObserved;
    }

    private void ControllerObserved()
    {
        observeEvents?.Invoke();
        this.enabled = false;
    }

    private void OnDisable()
    {
        tutorial.OnConditionSetCompleted -= ControllerObserved;
    }
}
