using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ConditionTutorial))]
public class FloatChoiceCondition : MonoBehaviour
{
    [SerializeField] private RadialFill radialSlider = null;
    [SerializeField] [Range(0, 360)] private float targetAngle = 0;
    [SerializeField] private Button button = null;

    private float targetMin;
    private float targetMax;
    private float angle;

    private ConditionTutorial tutorial;
    private UpdateTextAfterCondition updateText;

    private void Awake()
    {
        tutorial = GetComponent<ConditionTutorial>();
        updateText = GetComponent<UpdateTextAfterCondition>();
        
        targetMin = targetAngle - 15f;
        targetMax = targetAngle + 15f;
    }

    private void OnEnable()
    {
        radialSlider.OnValueChange += ListenForValueChange;
        button.onClick.AddListener(CheckForTargetAngle);
    }

    private void ListenForValueChange(float value)
    {
        angle = value * 360f;
        //Debug.Log("FloatChoiceCondition angle is " + angle.ToString("0.00"));
    }

    private void CheckForTargetAngle()
    {
        Debug.Log("FloatChoiceCondition checking for angle: " + angle.ToString("0.00"));
        
        if(angle >= targetMin && angle <= targetMax)
        {
            Debug.Log("FloatChoiceCondition condition passed");
            
            if (updateText != null)
                updateText.UpdateText(true);

            tutorial.FulfillCondition();
            this.enabled = false;
        }
        else
        {
            if (updateText != null)
                updateText.UpdateText(false);
        }
    }

    private void OnDisable()
    {
        radialSlider.OnValueChange -= ListenForValueChange;
        button.onClick.RemoveListener(CheckForTargetAngle);
    }
}
