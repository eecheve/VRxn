using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UpdateTextWithButton : MonoBehaviour
{
    [SerializeField] [TextArea] private string newText = "";
    [SerializeField] private TextMeshProUGUI text = null;
    [SerializeField] private Color color = Color.black;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(UpdateText);
    }

    private void UpdateText()
    {
        text.text = newText;
        text.color = color;
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(UpdateText);
    }
}
