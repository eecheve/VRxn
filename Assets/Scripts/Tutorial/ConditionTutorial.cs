using UnityEngine;

public class ConditionTutorial : Tutorial
{  
    public event TutorialCompleted OnConditionSetCompleted;

    public override void CheckIfHappening()
    {
        if(fulfillCondition == true)
        {
            TutorialManager.Instance.CompletedTutorial();
            OnConditionSetCompleted?.Invoke();
            Debug.Log("ConditionTutorial: condition set completed");
        }
    }
}
