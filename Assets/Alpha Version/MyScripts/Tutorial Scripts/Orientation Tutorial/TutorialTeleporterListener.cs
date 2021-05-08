using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialTeleporterListener : MonoBehaviour
{
    [TextArea] [SerializeField] private string[] instructions = null;

    private int index = 0;

    public void ChangeTextOnSelect(TextMeshProUGUI textMesh)
    {
        if (index < instructions.Length)
        {
            textMesh.text = instructions[index];
            index++;
        }
    }

    public void MoveUIOnExit(Transform newTransform)
    {
        transform.position = newTransform.position;
        transform.rotation = newTransform.rotation;
    }
}
