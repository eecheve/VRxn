using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class InputHelp : MonoBehaviour
{
    [SerializeField] private HintsButton hintsPanel = null;

    private void OnEnable()
    {
        //PrimaryButtonWatcher.Instance.onRightPrimaryPress.AddListener(ToggleHints);
        //PrimaryButtonWatcher.Instance.onLeftPrimaryPress.AddListener(ToggleAllUI);

        //SecondaryButtonWatcher.Instance.onLeftSecondaryPress.AddListener(ToggleAllUI);
        SecondaryButtonWatcher.Instance.onRightSecondaryPress.AddListener(ToggleHints);
    }

    private void ToggleHints(bool pressed)
    {
        if (pressed)
        {
            if (hintsPanel.gameObject.activeSelf == false)
                hintsPanel.gameObject.SetActive(true);

            else
                hintsPanel.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        //PrimaryButtonWatcher.Instance.onRightPrimaryPress.RemoveListener(ToggleHints);
        //PrimaryButtonWatcher.Instance.onLeftPrimaryPress.RemoveListener(ToggleAllUI);

        //SecondaryButtonWatcher.Instance.onLeftSecondaryPress.RemoveListener(ToggleAllUI);
        SecondaryButtonWatcher.Instance.onRightSecondaryPress.RemoveListener(ToggleHints);
    }
}
