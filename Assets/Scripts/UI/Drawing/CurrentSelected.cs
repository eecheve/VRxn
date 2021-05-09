using System;
using System.Collections;
using System.Collections.Generic;
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

        emptySprite = currentElement.sprite;
        emptyColor = currentElement.color;
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
            currentTMesh.text = tmesh.text;
        }
        else
        {
            Debug.Log("CurrentSelected: all the other times time is called");

            lastElement.sprite = currentSelected.sprite;
            lastElement.color = currentSelected.color;
            lastTMesh.text = tmesh.text;

            currentSelected = selectElement.CurrentSelected.GetComponent<Image>();
            tmesh = selectElement.CurrentSelected.GetComponentInChildren<TextMeshProUGUI>();

            currentElement.sprite = currentSelected.sprite;
            currentElement.color = currentSelected.color;
            currentTMesh.text = tmesh.text;
        }
    }

    private void OnDisable()
    {
        selectElement.OnElementSelected -= UpdateCurrent;
    }

    public void ResetImages()
    {
        currentElement.sprite = emptySprite;
        lastElement.sprite = emptySprite;
        currentTMesh.text = "";

        currentElement.color = emptyColor;
        lastElement.color = emptyColor;
        lastTMesh.text = "";

        currentSelected = null;
    }
}
