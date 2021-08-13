using TMPro;
using UnityEngine;

[RequireComponent(typeof(ConditionTutorial))]
public class RefreshTextAfterCondition : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmesh = null;
    [SerializeField] [TextArea] private string newText = "";
    
    private ConditionTutorial tutorial;

    private void Awake()
    {
        tutorial = GetComponent<ConditionTutorial>();
    }

    private void OnEnable()
    {
        tutorial.OnConditionSetCompleted += RefreshText;
        tmesh.text = "";
    }

    private void RefreshText()
    {
        tmesh.text = newText;
    }

    public void ResetText()
    {
        tmesh.text = "";
    }

    private void OnDisable()
    {
        tutorial.OnConditionSetCompleted -= RefreshText;
    }
}
