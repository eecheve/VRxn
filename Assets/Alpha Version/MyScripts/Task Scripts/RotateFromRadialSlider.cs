using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enumerators;

public class RotateFromRadialSlider : MonoBehaviour
{
    [Header("To Rotate")]
    [SerializeField] private Transform toRotate = null;

    [Header("Axis of Rotation")]
    [SerializeField] private CartesianAxis rotationAxis;

    private RadialFill radialFill;
    
    private Vector3 euler;

    private void Awake()
    {
        euler = toRotate.eulerAngles;
        radialFill = GetComponent<RadialFill>();
    }

    private void OnEnable()
    {
        radialFill.OnValueChange += RotateOnChange;
    }

    private void RotateOnChange(float value)
    {
        if(rotationAxis == CartesianAxis.x)
        {
            Vector3 newVector = new Vector3(value * 360f, 0, 0);
            toRotate.localEulerAngles = newVector + euler;
        }
        else if(rotationAxis == CartesianAxis.y)
        {
            Debug.Log("RotateFronRadialSlider_RotateOnChange: value is " + value.ToString());
            Vector3 newVector = new Vector3(0, value * 360f, 0);
            toRotate.localEulerAngles = newVector + euler;
        }
        else
        {
            Vector3 newVector = new Vector3(0, 0, value * 360f);
            toRotate.localEulerAngles = newVector + euler;
        }
    }

    private void OnDisable()
    {
        radialFill.OnValueChange -= RotateOnChange;
    }
}
