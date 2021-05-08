using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoreReactionPathButton : MonoBehaviour
{
    [SerializeField] private CanvasRenderer textCanvas = null;
    
    private TextMeshProUGUI tmpText;
    private bool state = false;

    private void Awake()
    {
        tmpText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ToggleMoreButton()
    {
        if(state == false)
        {
            textCanvas.gameObject.SetActive(true);
            tmpText.text = "Less";
            state = true;
        }
        else
        {
            textCanvas.gameObject.SetActive(false);
            tmpText.text = "More";
            state = false;
        }
    }
}
