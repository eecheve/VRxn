using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDebug : MonoBehaviour
{
    [SerializeField] private Button button = null;

    private void OnEnable()
    {
        button.onClick.AddListener(DebugButton);
    }

    public void DebugButton()
    {
        Debug.Log(name + "Button has been clicked");
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(DebugButton);
    }
}
