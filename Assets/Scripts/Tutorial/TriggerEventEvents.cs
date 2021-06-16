using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TriggerEventTutorial))]
public class TriggerEventEvents : MonoBehaviour
{
    private TriggerEventTutorial tutorial;

    public UnityEvent OnTriggerEnterEvents;

    private void OnEnable()
    {
        tutorial = GetComponent<TriggerEventTutorial>();

        tutorial.OnTriggerEntered += ConditionCompleted;
    }

    private void ConditionCompleted()
    {
        OnTriggerEnterEvents?.Invoke();
        this.enabled = false;
    }

    private void OnDisable()
    {
        tutorial.OnTriggerEntered -= ConditionCompleted;
    }
}
