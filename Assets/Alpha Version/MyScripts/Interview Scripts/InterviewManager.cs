using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InterviewManager : MonoBehaviour
{
    [Header("Button Attributes")]
    [SerializeField] private Button nextButton = null;
    [SerializeField] private Button previousButton = null;
    [SerializeField] private Button[] optionButtons = null;

    [Header("Text Attributes")]
    [SerializeField] private TextMeshProUGUI title = null;
    [SerializeField] private TextMeshProUGUI description = null;
    [SerializeField] private TextMeshProUGUI feedbackText = null;

    [Header("Image Attributes")]
    [SerializeField] private Image prompt = null;
    [SerializeField] private ProgressBar progressBar = null;

    private InterviewQuestionDataList interviewQuestions;
    private InterviewQuestionData currentQuestion;

    private List<UnityAction> buttonActions = new List<UnityAction>(); 

    private void OnEnable()
    {
        if ( nextButton != null)
        {
            nextButton.onClick.AddListener(GoToNext);
        }

        if(previousButton != null)
        {
            previousButton.onClick.AddListener(GoToPrevious);
        }

        if (!buttonActions.Contains(ManageButton0))
            buttonActions.Add(ManageButton0);

        if (!buttonActions.Contains(ManageButton1))
            buttonActions.Add(ManageButton1);

        if (!buttonActions.Contains(ManageButton2))
            buttonActions.Add(ManageButton2);

        if (!buttonActions.Contains(ManageButton3))
            buttonActions.Add(ManageButton3);

        if (optionButtons != null && optionButtons.Length > 0)
        {
            for (int i = 0; i < optionButtons.Length; i++)
            {
                optionButtons[i].onClick.AddListener(buttonActions[i]);
            }
        }

    }

    private void Awake()
    {
        interviewQuestions = InterviewQuestionDataList.Instance;

        if (interviewQuestions != null)
        {
            Debug.Log("Awake: current counter is: " + interviewQuestions.QCounter.ToString());
            SetUIElements(0);
        }
    }

    private void ManageButton0()
    {
        feedbackText.text = currentQuestion.Options[0].feedback;
    }

    private void ManageButton1()
    {
        feedbackText.text = currentQuestion.Options[1].feedback;
    }

    private void ManageButton2()
    {
        feedbackText.text = currentQuestion.Options[2].feedback;
    }

    private void ManageButton3()
    {
        feedbackText.text = currentQuestion.Options[3].feedback;
    }

    private void GoToNext()
    {
        if(interviewQuestions.QCounter < interviewQuestions.InterviewQuestions.Count)
        {
            interviewQuestions.AddToCounter();
            SetUIElements(interviewQuestions.QCounter);
            Debug.Log("GoToNext: counter became: " + interviewQuestions.QCounter.ToString());
        }
        else
        {
            return;
        }
    }

    private void GoToPrevious()
    {       
       if(interviewQuestions.QCounter > 0)
        {
            interviewQuestions.RemoveFromCounter();
            SetUIElements(interviewQuestions.QCounter);
            Debug.Log("GoToPrevious: counter became: " + interviewQuestions.QCounter.ToString());
        }
        else
        {
            return;
        }
    }

    private void SetUIElements(int index)
    {
        Debug.Log("Setting UI according to pos " + index.ToString());
        currentQuestion = interviewQuestions.InterviewQuestions[index];

        title.text = currentQuestion.Title;
        description.text = currentQuestion.Description;
        prompt.sprite = currentQuestion.Prompt;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            ResetFeedback();
            optionButtons[i].image.sprite = currentQuestion.Options[i].sprite;
        }

        progressBar.UpdateSlider(index);
    }

    private void ResetFeedback()
    {
        feedbackText.text = "";
    }

    private void OnDisable()
    {
        if (nextButton != null)
        {
            nextButton.onClick.RemoveListener(GoToNext);
        }

        if (previousButton != null)
        {
            previousButton.onClick.RemoveListener(GoToPrevious);
        }

        if (optionButtons != null && optionButtons.Length > 0)
        {
            if (optionButtons != null && optionButtons.Length > 0)
            {
                for (int i = 0; i < optionButtons.Length; i++)
                {
                    optionButtons[i].onClick.RemoveListener(buttonActions[i]);
                }
            }
        }
    }


}
