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
    public List<Tutorial> Tutorials { get { return tutorials; } set { tutorials = value; } }

    private Tutorial currentTutorial;

    public delegate void TutorialCompleted();
    public event TutorialCompleted OnTutorialCompleted;

    private void Start()
    {
        SetNextTutorial(0);
    }

    private void Update()
    {
        if (currentTutorial)
        {
            Debug.Log("TutorialManager: current tutorial is: " + currentTutorial.name);
            currentTutorial.CheckIfHappening();
        }
    }

    public void CompletedTutorial()
    {
        Debug.Log("TutorialManager: one tutorial was completed");
        SetNextTutorial(currentTutorial.Order + 1);
        OnTutorialCompleted?.Invoke();
    }

    public void CompletedAllTutorials()
    {
        //load scene
    }
    
    public void SetNextTutorial(int currentOrder)
    {
        currentTutorial = GetTutorialByOrder(currentOrder);

        if (!currentTutorial)
        {
            CompletedAllTutorials();
            return;
        }
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
}
