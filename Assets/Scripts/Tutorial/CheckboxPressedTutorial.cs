using UnityEngine;
using UnityEngine.UI;

public class CheckboxPressedTutorial : Condition
{
    [SerializeField] private Toggle checkbox = null;
    [SerializeField] private SpriteRenderer highlightArrow = null;
    [SerializeField] private Vector3 arrowOffset = Vector3.zero;

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
        condition.FulfillCondition();
        this.enabled = false;
    }

    private void OnDisable()
    {
        ManageArrow(false);
        checkbox.onValueChanged.RemoveListener(FulfillCondition);
    }
}
