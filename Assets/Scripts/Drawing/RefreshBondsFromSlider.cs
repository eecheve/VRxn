using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class RefreshBondsFromSlider : MonoBehaviour
{
    [SerializeField] private DrawElement drawSystem = null;
    
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        slider.onValueChanged.AddListener(RefreshBonds);
    }

    private void RefreshBonds(float value)
    {
        if (value > 0)
        {
            drawSystem.RefreshLineAngles();
        }
    }

    public void RefreshBonds()
    {
        drawSystem.RefreshLineAngles();
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(RefreshBonds);
    }
}
