using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ChangeTextWithSlider : MonoBehaviour
{
    [SerializeField] private Slider slider = null;

    private TextMeshProUGUI tmesh;

    private void OnEnable()
    {
        tmesh = GetComponent<TextMeshProUGUI>();
        slider.onValueChanged.AddListener(ChangeText);
    }

    private void ChangeText(float value)
    {
        double formatted = Math.Round(value, 2);
        string text = formatted.ToString();
        tmesh.text = text;
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(ChangeText);
    }
}
