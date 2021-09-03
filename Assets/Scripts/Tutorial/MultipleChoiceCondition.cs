using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ConditionTutorial))]
public class MultipleChoiceCondition : Condition
{
    [SerializeField] private Button correct = null;
    [SerializeField] private Button[] distractors = null;
    [SerializeField] private bool continueIfIncorrect = false;

    private UpdateTextAfterCondition updateText;

    protected override void Awake()
    {
        base.Awake();
        updateText = GetComponent<UpdateTextAfterCondition>();
    }

    private void OnEnable()
    {
        correct.onClick.AddListener(FulfillCondition);
        foreach (var button in distractors)
        {
            button.onClick.AddListener(DistractorFeedback);
        }
    }

    private void DistractorFeedback()
    {
        if(updateText != null)
            updateText.UpdateText(false);

        if (continueIfIncorrect == true)
        {
            condition.FulfillCondition();
            this.enabled = false;
        }
            
    }

    protected override void FulfillCondition()
    {
        if (updateText != null)
            updateText.UpdateText(true);

        condition.FulfillCondition();
        this.enabled = false;
    }

    private void OnDisable()
    {
        correct.onClick.RemoveListener(FulfillCondition);
        foreach (var button in distractors)
        {
            button.onClick.RemoveListener(DistractorFeedback);
        }
    }
}
