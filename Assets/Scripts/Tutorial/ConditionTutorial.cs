using UnityEngine;

public class ConditionTutorial : Tutorial
{
    private bool fulfillCondition = false;
    
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

    public void FulfillCondition()
    {
        fulfillCondition = true;
    }
    
    public void ResetCondition()
    {
        fulfillCondition = false;
    }
}
