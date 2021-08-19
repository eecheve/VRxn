using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateTextAfterCondition : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmesh = null;
    [SerializeField] private Color textColor = Color.white;
    [SerializeField] [TextArea] private string textIfCorrect = "";
    [SerializeField] [TextArea] private string textIfIncorrect = "";
    
    public void UpdateText(bool state)
    {
        Debug.Log("UpdateMesh in " + name + " called");
        tmesh.color = textColor;

        if (state == true)
            tmesh.text = textIfCorrect;
        else
            tmesh.text = textIfIncorrect;
    }
}
