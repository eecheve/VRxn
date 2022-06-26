using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetModule : MonoBehaviour
{
    [SerializeField] private ConditionTutorial tutorial = null;
    [SerializeField] private Condition condition = null;
    [SerializeField] private Button button = null;

    private TutorialManager tutorialManager;

    private void Awake()
    {
        tutorialManager = TutorialManager.Instance;
    }

    private void OnEnable()
    {
        if(button!=null)
            button.onClick.AddListener(ModuleReset);
    }

    public void ModuleReset()
    {
        if (tutorial != null)
            tutorial.ResetCondition();

        if (condition != null)
            condition.enabled = false;

        tutorialManager.ResetConditions();
        tutorialManager.SetTutorialByForce(tutorial);
    }

    public void ModuleStart()
    {
        if (condition != null)
            condition.enabled = true;
    }

    private void OnDisable()
    {
        if (button != null)
            button.onClick.RemoveListener(ModuleReset);
    }
}
