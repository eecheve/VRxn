using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OnSliderEvents : MonoBehaviour
{
    [Range(0, 1)] [SerializeField] private float upperLimit = 0;
    [Range(0, 1)] [SerializeField] private float lowerLimit = 0;

    public UnityEvent OnUpperLimitSurpassed;
    public UnityEvent OnLowerLimitSurpassed;
    public UnityEvent OnSliderReset;

    private Slider slider;


    private void OnEnable()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(ReadSliderValue);
    }

    private void ReadSliderValue(float value)
    {
        if (value > upperLimit)
            OnUpperLimitSurpassed?.Invoke();

        else if (value < lowerLimit)
            OnLowerLimitSurpassed?.Invoke();

        else if (value == 0)
            OnSliderReset?.Invoke();
    }
}
