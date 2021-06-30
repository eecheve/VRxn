using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ConditionTutorial))]
public class ButtonPressedTutorial : MonoBehaviour
{
    [SerializeField] private Button button = null;
    
    private ConditionTutorial tutorial;
    private Animator animator;

    private void Awake()
    {
        tutorial = GetComponent<ConditionTutorial>();
        animator = button.gameObject.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(FulfillCondition);
        
        if(animator != null)
            animator.enabled = true;
    }

    private void FulfillCondition()
    {
        tutorial.FulfillCondition();
        this.enabled = false;
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(FulfillCondition);
        
        if (animator != null)
            animator.enabled = false;
    }
}
