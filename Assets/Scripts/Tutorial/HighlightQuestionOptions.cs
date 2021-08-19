using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MultipleChoiceCondition))]
public class HighlightQuestionOptions : MonoBehaviour
{
    [SerializeField] private Transform highlighter = null;
    [SerializeField] private Transform target = null;
    [SerializeField] private Vector3 arrowOffset = Vector3.zero;

    private SpriteRenderer arrow;

    private void Awake()
    {
        arrow = highlighter.GetComponentInChildren<SpriteRenderer>();
        if (arrow == null)
            Debug.LogError("HighlightQuestionOptions in " + name + " is not used correctly");
    }

    public void HighlightQuestions()
    {
        highlighter.position = target.position + arrowOffset;
        if (arrow != null)
            arrow.enabled = true;
    }

    public void StopHighlight()
    {
        if (arrow != null)
            arrow.enabled = false;
    }
}
