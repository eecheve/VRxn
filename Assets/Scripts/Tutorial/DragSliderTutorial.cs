using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ConditionTutorial))]
public class DragSliderTutorial : MonoBehaviour
{
    [SerializeField] private Slider slider = null;
    [SerializeField] private Transform handle = null;
    [SerializeField] private SpriteRenderer arrow = null;
    [SerializeField] private Vector3 positionOffset = Vector3.zero;
    
    private ConditionTutorial tutorial;

    private void Awake()
    {
        tutorial = GetComponent<ConditionTutorial>();
    }

    private void OnEnable()
    {
        slider.onValueChanged.AddListener(CheckForSliderDragged);

        arrow.transform.position = handle.position + positionOffset;
        arrow.enabled = true;
    }

    private void CheckForSliderDragged(float value)
    {
        if (value > 0)
        {
            arrow.enabled = false;
            this.enabled = false;
        }
    }

    private void OnDisable()
    {
        tutorial.FulfillCondition();
        slider.onValueChanged.RemoveListener(CheckForSliderDragged);
    }
}
