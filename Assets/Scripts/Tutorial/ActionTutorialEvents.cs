using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ActionTutorial))]
public class ActionTutorialEvents : MonoBehaviour
{
    public UnityEvent ActionEvents;

    private ActionTutorial tutorial;

    private void OnEnable()
    {
        tutorial = GetComponent<ActionTutorial>();
        tutorial.OnAnyActionPerformed += ConditionCompleted;
    }

    private void ConditionCompleted()
    {
        Debug.Log(name + " ActionTutorialEvent: condition completed");
        ActionEvents?.Invoke();
        this.enabled = false;
    }

    private void OnDisable()
    {
        tutorial.OnAnyActionPerformed -= ConditionCompleted;
    }
}
