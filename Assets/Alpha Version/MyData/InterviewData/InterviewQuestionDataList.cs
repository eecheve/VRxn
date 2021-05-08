using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InterviewQuestionDataList : MonoSingleton<InterviewQuestionDataList>
{
    [SerializeField] private InterviewQuestionData[] interviewQuestions = null;

    public List<InterviewQuestionData> InterviewQuestions { get; private set; } = new List<InterviewQuestionData>();
    public int QCounter { get; private set; } = 0;
    private void OnEnable()
    {
        if (!InterviewQuestions.Any())
            InitiallizeList();
    }

    private void InitiallizeList()
    {
        foreach (var question in interviewQuestions)
        {
            InterviewQuestions.Add(question);
        }
    }

    public void AddToCounter()
    {
        QCounter++;
        Debug.Log("Adding to counter to obtain: " + QCounter.ToString());
    }

    public void RemoveFromCounter()
    {
        QCounter--;
        Debug.Log("Removing to counter to obtain: " + QCounter.ToString());
    }
}
