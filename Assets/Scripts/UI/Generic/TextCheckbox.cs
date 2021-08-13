using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Toggle))]
public class TextCheckbox : MonoBehaviour
{
    [SerializeField] private Color activeColor = Color.white;
    [SerializeField] private Color inactiveColor = Color.white;
    [SerializeField] private TextMeshProUGUI tmesh = null;

    private Toggle toggle;
    
    private void OnEnable()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(ToggleText);
    }

    private void ToggleText(bool state)
    {
        if (state == true)
        {
            tmesh.color = activeColor;
        }
        else
        {
            tmesh.color = inactiveColor;
        }
    }

    private void OnDisable()
    {
        toggle.onValueChanged.RemoveListener(ToggleText);
    }
}
