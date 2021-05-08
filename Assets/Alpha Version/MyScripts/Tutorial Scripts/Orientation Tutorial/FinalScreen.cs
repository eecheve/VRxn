using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMesh = null;
    
    private void OnEnable()
    {
        PrimaryButtonWatcher.Instance.onRightPrimaryPress.AddListener(TurnOfHelper);
    }

    private void TurnOfHelper(bool pressed)
    {
        if (pressed)
        {
            ButtonHelperManager.Instance.ToggleHelpers(ButtonHelperManager.Instance.HelpHelper, false);
            PrimaryButtonWatcher.Instance.onRightPrimaryPress.RemoveListener(TurnOfHelper);

            textMesh.enabled = true;
        }
    }

    private void OnDisable()
    {
        PrimaryButtonWatcher.Instance.onRightPrimaryPress.RemoveListener(TurnOfHelper);
    }
}
