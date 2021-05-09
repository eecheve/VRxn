using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EraseButton : MonoBehaviour
{
    [SerializeField] private SelectElement selectElement = null;
    [SerializeField] private CurrentSelected currentSelected = null;
    [SerializeField] private Button button = null;

    private void OnEnable()
    {
        button.onClick.AddListener(Erase);
    }

    private void Erase()
    {
        if(selectElement.CurrentSelected != null)
        {
            GameObject.DestroyImmediate(selectElement.CurrentSelected);
            currentSelected.ResetImages();
            selectElement.ResetSelected();
        }
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(Erase);
    }
}
