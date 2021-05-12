using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ChangeTextAfterClick : MonoBehaviour
{
    [SerializeField] private Color textColor = Color.white;
    [SerializeField] private TextMeshProUGUI tmesh = null;
    [SerializeField] private string newText = "";

    private Button button;

    private void OnEnable()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ChangeText);
    }

    private void ChangeText()
    {
        tmesh.text = newText;
        tmesh.color = textColor;
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(ChangeText);
    }
}
