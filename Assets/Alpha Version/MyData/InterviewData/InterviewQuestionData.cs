using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Enumerators;

[CreateAssetMenu()]
public class InterviewQuestionData : ScriptableObject
{
    [Serializable] public struct OptionInfo
    {
        public string feedback;
        public Sprite sprite;
        public bool isCorrect;
    }

    [SerializeField] private QuestionGoal goal = 0;
    [SerializeField] private string title = "";
    [SerializeField] private string index = "";
    [SerializeField] [TextArea(5,10)] private string description = "";
    [SerializeField] private Sprite prompt = null;
    [SerializeField] private OptionInfo[] options = null;

    public QuestionGoal Goal { get { return goal; } private set { goal = value; } }
    public string Title { get { return title; } private set { title = value; } }
    public string Index { get { return index; } private set { index = value; } }
    public string Description { get { return description; } private set { description = value; } }
    public Sprite Prompt { get { return prompt; } private set { prompt = value; } }
    public List<OptionInfo> Options { get; private set; } = new List<OptionInfo>();

    public void ShuffleOptions()
    {
        if (!Options.Any() && Options != null)
        {
            foreach (var option in options)
            {
                Options.Add(option);
            }

            Options.Shuffle(new System.Random());
        }
    }
}
