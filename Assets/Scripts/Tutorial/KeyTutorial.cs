using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTutorial : Tutorial
{
    public List<bool> conditions = new List<bool>();

    private List<bool> Conditions { get { return conditions; } set { conditions = value; } }

    public override void CheckIfHappening()
    {
        for (int i = 0; i < Conditions.Count; i++)
        {
            if(Conditions[i] == true)
            {
                Conditions.RemoveAt(i);
                break;
            }
        }

        if(Conditions.Count == 0)
        {
            TutorialManager.Instance.CompletedTutorial();
        }
    }
}
