using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ConditionTutorial))]
public class MultipleChoiceCondition : MonoBehaviour
{
    [SerializeField] private Button correct = null;
    [SerializeField] private Button[] distractors = null;
    [SerializeField] private bool continueIfIncorrect = false;

    private ConditionTutorial condition;
    private UpdateTextAfterCondition updateText;

    private void Awake()
    {
        condition = GetComponent<ConditionTutorial>();
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

    private void FulfillCondition()
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
