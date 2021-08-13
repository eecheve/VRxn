using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ConditionTutorial))]
public class CheckboxPressedTutorial : MonoBehaviour
{
    [SerializeField] private Toggle checkbox = null;
    [SerializeField] private SpriteRenderer highlightArrow = null;
    [SerializeField] private Vector3 arrowOffset = Vector3.zero;

    private ConditionTutorial tutorial;

    private void Awake()
    {
        tutorial = GetComponent<ConditionTutorial>();
    }

    private void OnEnable()
    {
        checkbox.onValueChanged.AddListener(FulfillCondition);
        ManageArrow(true);
    }

    private void ManageArrow(bool state)
    {
        if (highlightArrow != null)
        {
            highlightArrow.transform.position = checkbox.transform.position + arrowOffset;
            highlightArrow.enabled = state;
        }
    }

    private void FulfillCondition(bool value)
    {
        tutorial.FulfillCondition();
        this.enabled = false;
    }

    private void OnDisable()
    {
        ManageArrow(false);
        checkbox.onValueChanged.RemoveListener(FulfillCondition);
    }
}
