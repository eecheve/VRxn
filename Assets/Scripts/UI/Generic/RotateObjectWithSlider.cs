using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Enumerators;

public class RotateObjectWithSlider : MonoBehaviour
{
    [SerializeField] private Slider slider = null;
    [SerializeField] private bool mirror = false;
    [SerializeField] private Transform target = null;
    [SerializeField] private CartesianAxis axis = CartesianAxis.y;

    private Vector3 initialEuler;

    private void Awake()
    {
        initialEuler = target.eulerAngles;
    }

    private void OnEnable()
    {
        slider.onValueChanged.AddListener(RotateObject);
    }

    private void RotateObject(float value)
    {
        float angle = GetAngleByValue(mirror, value);
        if (axis == CartesianAxis.x)
        {
            Vector3 rotation = initialEuler + new Vector3(angle, 0, 0);
            target.eulerAngles = rotation;
        }
        else if (axis == CartesianAxis.y)
        {
            Vector3 rotation = initialEuler + new Vector3(0, angle, 0);
            target.eulerAngles = rotation;
        }
        else
        {
            Vector3 rotation = initialEuler + new Vector3(0, 0, angle);
            target.eulerAngles = rotation;
        }
    }

    private float GetAngleByValue(bool mirror, float value)
    {
        if(mirror == false)
            return value * 360f;
        else
            return value * -360;
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(RotateObject);
    }
}
