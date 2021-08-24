using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrentSelected : MonoBehaviour
{
    [SerializeField] private SelectElement selectElement = null;
    [SerializeField] private Image currentElement = null;
    [SerializeField] private Image lastElement = null;
    [SerializeField] private TextMeshProUGUI currentTMesh = null;
    [SerializeField] private TextMeshProUGUI lastTMesh = null;

    private Sprite emptySprite;
    private Color emptyColor;

    private Image currentSelected;
    private TextMeshProUGUI tmesh;

    private void OnEnable()
    {
        selectElement.OnElementSelected += UpdateCurrent;
        selectElement.OnElementDeselected += ResetImages;
        selectElement.OnPreviousElementSelected += ResetCurrent;

        emptySprite = currentElement.sprite;
        emptyColor = currentElement.color;
    }

    private void ResetCurrent()
    {
        currentSelected = selectElement.CurrentSelected.GetComponent<Image>();
        tmesh = selectElement.CurrentSelected.GetComponentInChildren<TextMeshProUGUI>();

        currentElement.sprite = currentSelected.sprite;
        currentElement.color = currentSelected.color;
        currentTMesh.text = tmesh.text;

        lastElement.sprite = emptySprite;
        lastElement.color = emptyColor;
        lastTMesh.text = "";
    }

    private void UpdateCurrent()
    {
        if(currentSelected == null)
        {
            Debug.Log("CurrentSelected: first time is called");
            currentSelected = selectElement.CurrentSelected.GetComponent<Image>();
            tmesh = selectElement.CurrentSelected.GetComponentInChildren<TextMeshProUGUI>();

            if (currentSelected == null)
                Debug.Log("CurrentSelected: problem is the image");

            if (tmesh == null)
                Debug.Log("CurrentSelceted: problem is text");

            currentElement.sprite = currentSelected.sprite;
            currentElement.color = currentSelected.color;

            string label = Regex.Match(tmesh.text, @"\d+").Value;
            currentTMesh.text = tmesh.text + label;
        }
        else
        {
            Debug.Log("CurrentSelected: all the other times is called");

            lastElement.sprite = currentSelected.sprite;
            lastElement.color = currentSelected.color;

            string lastLabel = Regex.Match(tmesh.text, @"\d+").Value;
            lastTMesh.text = tmesh.text + lastLabel;

            currentSelected = selectElement.CurrentSelected.GetComponent<Image>();
            tmesh = selectElement.CurrentSelected.GetComponentInChildren<TextMeshProUGUI>();

            currentElement.sprite = currentSelected.sprite;
            currentElement.color = currentSelected.color;

            string currentLabel = Regex.Match(tmesh.text, @"\d+").Value;
            currentTMesh.text = tmesh.text + currentLabel;
        }
    }

    private void OnDisable()
    {
        selectElement.OnElementSelected -= UpdateCurrent;
        selectElement.OnElementDeselected -= ResetImages;
    }

    public void ResetImages()
    {
        currentElement.sprite = emptySprite;
        currentElement.color = emptyColor;
        currentTMesh.text = "";

        lastElement.sprite = emptySprite;
        lastElement.color = emptyColor;
        lastTMesh.text = "";

        currentSelected = null;
    }
}
