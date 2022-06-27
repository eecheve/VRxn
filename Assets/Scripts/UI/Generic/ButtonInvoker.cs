using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInvoker : MonoBehaviour
{
    [SerializeField] private Button button = null;

    private void OnEnable()
    {
        button.onClick.AddListener(InvokeButton);
    }

    public void InvokeButton()
    {
        Debug.Log($"ButtonInvoker in {name} is Invoking button {button.name}");
        button.onClick.Invoke();
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(InvokeButton);
    }
}
