using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ChangeTextAfterEvent : MonoBehaviour
{
    [SerializeField] [TextArea] private string newText = null;
    [SerializeField] private Color newColor = Color.white;

    private TextMeshProUGUI tmesh;
    private string oText;
    private Color oColor;

    private void Awake()
    {
        tmesh = GetComponent<TextMeshProUGUI>();
        oText = tmesh.text;
        oColor = tmesh.color;
    }

    public void ChangeText()
    {
        tmesh.text = newText;
        tmesh.color = newColor;
    }

    public void RevertText()
    {
        tmesh.text = oText;
        tmesh.color = oColor;
    }
}
