using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrientationTask : MonoBehaviour
{
    [Header("Rotatable")]
    [SerializeField] private Transform rotatable = null;
    
    [Header("Radial Sliders")]
    [SerializeField] private RadialFill radialSliderY = null;
    [SerializeField] private RadialFill radialSliderZ = null;

    [Header("Sequence")]
    [SerializeField] private GameObject[] pages = null;
   
    [Header("Page 4")]
    [SerializeField] private GameObject yRotator = null;
    [SerializeField] private Button button4 = null;
    [SerializeField] private TextMeshProUGUI tMesh4 = null;

    [Header("Page 5")]
    [SerializeField] private GameObject zRotator = null;
    [SerializeField] private Button button5 = null;
    [SerializeField] private TextMeshProUGUI tMesh5 = null;

    private Vector3 initialEulerAngles;
    
    private float yValue;
    private float zValue;

    private bool page1Turned = false;

    private void Awake()
    {
        initialEulerAngles = rotatable.localEulerAngles;
    }

    private void OnEnable()
    {
        radialSliderY.OnValueChange += UpdateYValue;
        radialSliderZ.OnValueChange += UpdateZValue;

        button4.onClick.AddListener(CheckForYValue);
        button5.onClick.AddListener(CheckForZValue);
    }

    private void UpdateYValue(float value)
    {
        yValue = value * 360f;
    }

    private void UpdateZValue(float value)
    {
        zValue = value * 360f;
    }

    private void CheckForYValue()
    {
        Debug.Log("OrientationTask_YValue: " + yValue.ToString());
        
        if(yValue.IsBetweenNumbers(180f, 200f))
        {
            tMesh4.enabled = true;
            tMesh4.text = "<color=green>correct!</color>, please go to the next page";
        }
        else
        {
            tMesh4.enabled = true;
            tMesh4.text = "<color=red>incorrect</color>, please try again";
        }
    }

    public void SetMoleculeForNextTask()
    {
        rotatable.localEulerAngles = initialEulerAngles;
    }
    
    private void CheckForZValue()
    {
        if(zValue.IsBetweenNumbers(130f, 150f))
        {
            tMesh5.enabled = true;
            tMesh5.text = "<color=green>correct!</color>, please advance to the next task";
        }
        else
        {
            tMesh5.enabled = true;
            tMesh5.text = "<color=red>incorrect</color>, please try again";
        }
    }

    public void TurnPage1()
    {
        if(page1Turned == false)
        {
            pages[0].SetActive(false);
            pages[1].SetActive(true);

            page1Turned = true;
        }
    }

    /*public void ResetTask()
    {
        foreach (var page in pages)
        {
            page.SetActive(false);
        }
        pages[1].SetActive(true);

        yRotator.SetActive(true);
        zRotator.SetActive(false);

        rotatable.localEulerAngles = initialEulerAngles;
    }*/
    
    private void OnDisable()
    {
        radialSliderY.OnValueChange -= UpdateYValue;
        radialSliderZ.OnValueChange -= UpdateZValue;

        button4.onClick.RemoveListener(CheckForYValue);
        button5.onClick.RemoveListener(CheckForZValue);
    }
}
