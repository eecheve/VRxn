using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConditionTutorial))]
public class TwoOptionsCondition : Condition
{
    [SerializeField] private Condition condition1 = null;
    [SerializeField] private Condition condition2 = null;
    
    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        condition.OnConditionSetCompleted += DeactivateSecondCondition;
    }

    private void DeactivateSecondCondition()
    {
        condition1.enabled = false;
        condition2.enabled = false;
    }
}
