using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enumerators;

public class CheckingOptionsState : MonoBehaviour
{
    [SerializeField] private ToggleOptionsMenu optionsMenu = null;
    [SerializeField] private Direction direction = Direction.D_up;

    private ConditionTutorial tutorial;

    private void Awake()
    {
        tutorial = GetComponent<ConditionTutorial>();
    }

    private void OnEnable()
    {
        if(direction == Direction.D_up)
        {
            optionsMenu.OnOptionUp += FulfillCondition;
        }
        else if(direction == Direction.D_down)
        {
            optionsMenu.OnOptionDown += FulfillCondition;
        }
        else
        {
            Debug.LogError(name + "CheckingOptionState: error assigning the direction of the menu");
        }
    }

    private void FulfillCondition()
    {
        Debug.Log(name + "CheckingOptionsState: fulfilling required condition");
        tutorial.FulfillCondition();
        this.enabled = false;
    }

    private void OnDisable()
    {
        if (direction == Direction.D_up)
        {
            optionsMenu.OnOptionUp -= FulfillCondition;
        }
        else if (direction == Direction.D_down)
        {
            optionsMenu.OnOptionDown -= FulfillCondition;
        }
        else
        {
            Debug.LogError(name + "CheckingOptionState: error assigning the direction of the menu");
        }
    }
}
