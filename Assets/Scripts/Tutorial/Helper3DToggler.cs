using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper3DToggler : MonoBehaviour
{
    [SerializeField] private Button3DHelper[] helpersToActivate = null;
    [SerializeField] private Button3DHelper[] helpersToDeactivate = null;

    private ConditionTutorial conditionTutorial;
    private ActionTutorial actionTutorial;

    private void OnEnable()
    {
        conditionTutorial = GetComponent<ConditionTutorial>();
        actionTutorial = GetComponent<ActionTutorial>();
        
        if(conditionTutorial != null)
            conditionTutorial.OnConditionSetCompleted += ToggleHelper;

        if (actionTutorial != null)
            actionTutorial.OnAnyActionPerformed += ToggleHelper;
    }

    private void ToggleHelper()
    {
        if (helpersToActivate != null)
        {
            Debug.Log("Helper3DToggler: activating 3D helpers");
            foreach (var helper in helpersToActivate)
            {
                helper.ToggleButtonHelper(true);
            }
        }

        if(helpersToDeactivate != null)
        {
            foreach (var helper in helpersToDeactivate)
            {
                helper.ToggleButtonHelper(false);
            }
        }
    }

    private void OnDisable()
    {
        if (conditionTutorial != null)
            conditionTutorial.OnConditionSetCompleted -= ToggleHelper;

        if (actionTutorial != null)
            actionTutorial.OnAnyActionPerformed -= ToggleHelper;
    }
}
