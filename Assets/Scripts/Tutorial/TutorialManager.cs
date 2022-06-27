using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<TutorialManager>();

            if (instance == null)
                Debug.Log("TutorialManager: There is no TutorialManager in scene");

            return instance;
        }
    }
    private static TutorialManager instance;

    [SerializeField] private List<Tutorial> tutorials = null;
    [SerializeField] private int beginFromIndex = 0;
    public List<Tutorial> Tutorials { get { return tutorials; } set { tutorials = value; } }

    public Tutorial CurrentTutorial { get; private set; }

    public delegate void SectionCompleted();
    public event SectionCompleted OnSectionCompleted;
    public event SectionCompleted OnTutorialCompleted;

    private void Start()
    {
        SetNextTutorial(beginFromIndex);
    }

    private void Update()
    {
        if (CurrentTutorial)
        {
            Debug.Log("TutorialManager: current tutorial is: " + CurrentTutorial.name);
            CurrentTutorial.CheckIfHappening();
        }
    }

    public void CompletedTutorial()
    {
        Debug.Log("TutorialManager: one tutorial was completed");
        SetNextTutorial(CurrentTutorial.Order + 1);
        OnTutorialCompleted?.Invoke();
    }

    public void CompletedAllTutorials()
    {
        OnTutorialCompleted?.Invoke();
        //load scene
    }
    
    public void SetNextTutorial(int currentOrder)
    {
        CurrentTutorial = GetTutorialByOrder(currentOrder);

        if (!CurrentTutorial)
        {
            CompletedAllTutorials();
            return;
        }
        else
        {
            OnSectionCompleted?.Invoke();
        }
    }

    public void SetTutorialByForce(int index)
    {
        if(index < tutorials.Count)
            CurrentTutorial = GetTutorialByOrder(index);
    }

    public void SetTutorialByForce(Tutorial tutorial)
    {
        CurrentTutorial = tutorial;
    }
    
    public Tutorial GetTutorialByOrder(int order)
    {
        for (int i = 0; i < Tutorials.Count; i++)
        {
            if (Tutorials[i].Order == order)
                return Tutorials[i];
        }

        return null;
    }

    public void ResetConditions()
    {
        foreach (var tutorial in Tutorials)
        {
            //Debug.Log("TutorialManager: reseting tutotial " + tutorial.name);
            tutorial.ResetCondition();
            //ConditionTutorialEvents conditionEvents = tutorial.GetComponent<ConditionTutorialEvents>();
            //if(conditionEvents != null)
            //{
            //    Debug.Log("Tutorial Manager: enabling condition event in " + conditionEvents.name);
            //    conditionEvents.enabled = true;
            //}
        }
    }
}
