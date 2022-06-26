using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ConditionTutorial))]
public class ConditionTutorialEvents : MonoBehaviour
{
    public UnityEvent ConditionEvents;
    
    private ConditionTutorial tutorial;

    private void OnEnable()
    {
        tutorial = GetComponent<ConditionTutorial>();
        tutorial.OnConditionSetCompleted += ConditionCompleted;
    }

    private void ConditionCompleted()
    {
        ConditionEvents?.Invoke();
        //this.enabled = false;
    }

    private void OnDisable()
    {
        tutorial.OnConditionSetCompleted -= ConditionCompleted;
    }
}
