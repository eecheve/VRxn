using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckboxValuesHandler : MonoBehaviour
{
    [Serializable] public struct ChkbxFeedback
    {
        public Toggle checkbox;
        public GameObject feedback;
    }

    [SerializeField] private ChkbxFeedback[] checkboxes = null;

    private void OnEnable()
    {
        foreach (var checkbox in checkboxes)
        {
            Toggle chkbx = checkbox.checkbox;
            GameObject feedback = checkbox.feedback;
            chkbx.onValueChanged.AddListener(delegate
            {
                ToggleFeedback(chkbx, feedback);
            });
        }
    }

    private void ToggleFeedback(Toggle chkbx, GameObject feedback)
    {
        bool value = chkbx.isOn;
        feedback.SetActive(value);
    }

    private void OnDisable()
    {
        foreach (var checkbox in checkboxes)
        {
            Toggle chkbx = checkbox.checkbox;
            GameObject feedback = checkbox.feedback;
            chkbx.onValueChanged.RemoveListener(delegate
            {
                ToggleFeedback(chkbx, feedback);
            });
        }
    }
}
