using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UpdateTextWithSlider : MonoBehaviour
{
    [SerializeField] private Slider slider = null;
    [SerializeField] private Image sliderHandle = null;
    [SerializeField] private ChartDataManager dataManager = null;

    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        if(slider != null && dataManager != null)
        {
            slider.onValueChanged.AddListener(ManageText);

            text.text = dataManager.ChartData.Points[0].y.ToString("#.##");
        }
    }

    private void ManageText(float value)
    {
        int index = (int)(value * dataManager.ChartData.Points.Count);
        float energy = dataManager.ChartData.Points[index].y;
        if (energy != dataManager.MaxEnergy)
        {
            if (sliderHandle != null && sliderHandle.color != Color.white)
                sliderHandle.color = Color.white;
            
            text.color = Color.white;
            text.text = dataManager.ChartData.Points[index].y.ToString("#.##");
        }
        else
        {
            if (sliderHandle != null && sliderHandle.color != Color.yellow)
                sliderHandle.color = Color.yellow;

            text.color = Color.yellow;
            text.text = dataManager.ChartData.Points[index].y.ToString("#.##");
        }

    }

    private void OnDisable()
    {
        if (slider != null && dataManager != null)
            slider.onValueChanged.RemoveListener(ManageText);
    }
}
