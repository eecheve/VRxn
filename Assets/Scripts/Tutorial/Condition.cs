using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConditionTutorial))]
public abstract class Condition : MonoBehaviour
{
    protected ConditionTutorial condition;

    protected virtual void Awake()
    {
        condition = GetComponent<ConditionTutorial>();
    }

    protected virtual void FulfillCondition()
    {
        condition.FulfillCondition();
        this.enabled = false;
    }
}
