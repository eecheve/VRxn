using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ChangeTextWithDropdown : MonoBehaviour
{
    [SerializeField] private int correctValue = 0;
    [SerializeField] private TextMeshProUGUI tmesh = null;

    private TMP_Dropdown dropdown;

    private void OnEnable()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        dropdown.onValueChanged.AddListener(delegate
        {
            UpdateTextMesh(dropdown);
        });
    }

    private void UpdateTextMesh(TMP_Dropdown dropdown)
    {
        Debug.Log("ChangeTextWithDropdown: value is " + dropdown.value);
        
        if(dropdown.value == correctValue)
        {
            tmesh.text = "<color=green>Correct!</color> Click on next to continue";
        }
        else
        {
            tmesh.text = "<color=red>Incorrect</color> Try another value";
        }
    }

    private void OnDisable()
    {
        dropdown.onValueChanged.RemoveListener(delegate
        {
            UpdateTextMesh(dropdown);
        });
    }
}