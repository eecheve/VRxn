using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TsDrawingTask : MonoBehaviour
{
    [Header("Whiteboard")]
    [SerializeField] private Button resetButton = null;
    
    [Header("Feedback")]
    [SerializeField] private TextMeshProUGUI tMesh = null;
    [TextArea] [SerializeField] private string feedback = "";

    private string initialText;

    private void Awake()
    {
        initialText = tMesh.text;
    }

    private void OnEnable()
    {
        resetButton.onClick.AddListener(ResetFeedbackText);
        
        TopOutsideManager.OnDrawingTaskCompleted += ProvideTaskFeedback;
    }

    private void ResetFeedbackText()
    {
        tMesh.text = initialText;
    }

    private void ProvideTaskFeedback()
    {
        tMesh.text = feedback;
    }

    private void OnDisable()
    {
        resetButton.onClick.RemoveListener(ResetFeedbackText);

        TopOutsideManager.OnDrawingTaskCompleted -= ProvideTaskFeedback;
    }
}
