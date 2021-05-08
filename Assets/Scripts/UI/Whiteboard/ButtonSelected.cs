using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelected : MonoBehaviour
{
    [SerializeField] private Color selectedColor = Color.white;
        
    private Image image;
    private Color originalColor;
    private Button button;

    private void Awake()
    {
        image = GetComponent<Image>();
        originalColor = image.color;
        button = GetComponent<Button>();
    }

}
