using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateTextWthSlide : MonoBehaviour
{
    [SerializeField] private Slider slider = null;

    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        slider.onValueChanged.AddListener(ManageText);
    }

    private void ManageText(float value)
    {
        text.text = value.ToString("#.##");
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(ManageText);
    }
}
