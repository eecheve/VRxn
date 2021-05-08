using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeValueFromRadialSlider : MonoBehaviour
{
    [SerializeField] private RadialFill radialFill = null;

    private TextMeshProUGUI textMesh;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();    
    }

    private void OnEnable()
    {
        radialFill.OnValueChange += ChangeTextValue;
    }

    private void ChangeTextValue(float value)
    {
        float angle = value * 360f;
        textMesh.text = angle.ToString("0.00");
    }

    private void OnDisable()
    {
        radialFill.OnValueChange -= ChangeTextValue;
    }
}
