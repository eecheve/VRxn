using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Question : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private InterviewQuestionData questionData = null;

    [Header("Question Attributes")]
    [SerializeField] private TextMeshProUGUI description = null;
    [SerializeField] private TextMeshProUGUI qCounter = null;
    [SerializeField] private Image qPrompt = null;
    [SerializeField] private Image[] qOptions = null;

    [Header("Strategy Attributes")]
    [SerializeField] private TextMeshProUGUI sCounter = null;
    [SerializeField] private Image sPrompt = null;
    [SerializeField] private Image[] sOptions = null;

    [Header("Follow-up Attributes")]
    [SerializeField] private TextMeshProUGUI fCounter = null;
    [SerializeField] private Image fPrompt = null;
    [SerializeField] private Image[] fOptions = null;

    private Sprite[] sprites = new Sprite[4];

    public void PopulateQuestion()
    {
        Debug.Log($"Trying to populate {name}");

        questionData.ShuffleOptions();

        description.text = questionData.Description;
        
        qCounter.text = questionData.Index;
        sCounter.text = questionData.Index;
        fCounter.text = questionData.Index;

        qPrompt.sprite = questionData.Prompt;
        sPrompt.sprite = questionData.Prompt;
        fPrompt.sprite = questionData.Prompt;

        int j = 1;
        for (int i = 0; i < questionData.Options.Count; i++)
        {
            qOptions[i].sprite = questionData.Options[i].sprite;
            if (questionData.Options[i].isCorrect == true)
                sprites.SetValue(questionData.Options[i].sprite, 0);
            else
            {
                sprites.SetValue(questionData.Options[i].sprite, j);
                j++;
            }
        }

        for (int i = 0; i < sprites.Length; i++)
        {
            sOptions[i].sprite = sprites[i];
            fOptions[i].sprite = sprites[i];
        }
    }

    public void ResetToDefaults()
    {
        description.text = "";

        qCounter.text = "";
        sCounter.text = "";
        fCounter.text = "";

        qPrompt.sprite = null;
        sPrompt.sprite = null;
        fPrompt.sprite = null;

        for (int i = 0; i < sprites.Length; i++)
        {
            qOptions[i].sprite = null;
            sOptions[i].sprite = null;
            fOptions[i].sprite = null;
            sprites.SetValue(null, i);
        }
    }
}
