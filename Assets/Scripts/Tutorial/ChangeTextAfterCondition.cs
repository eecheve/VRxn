using UnityEngine;
using TMPro;

public class ChangeTextAfterCondition : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tMesh = null;
    [TextArea] [SerializeField] private string newText = "";

    public void ChangeText()
    {
        tMesh.text = newText;
    }
}
