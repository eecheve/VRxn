using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionaireTimer : MonoBehaviour
{
    [SerializeField] private Timer timer = null;
    [SerializeField] private GameObject QuestionPanel = null;
    [SerializeField] private GameObject FeedbackPanel = null;

    private void OnEnable()
    {
        timer.OnTimeRanOut += MoveToFeedbackPanel;
    }

    private void MoveToFeedbackPanel()
    {
        QuestionPanel.SetActive(false);
        FeedbackPanel.SetActive(true);
    }

    private void OnDisable()
    {
        timer.OnTimeRanOut -= MoveToFeedbackPanel;
    }
}
