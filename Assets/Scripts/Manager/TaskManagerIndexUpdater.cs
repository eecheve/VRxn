using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class TaskManagerIndexUpdater : MonoBehaviour
{
    public int index;
    public Tutorial[] tutorials;

    public void SetSingleTutorialIndex(Tutorial tutorial, int index)
    {
        //https://stackoverflow.com/questions/9080492/get-first-numbers-from-string
        //gets the current index, the first int in the name
        string currentIndex = new string(tutorial.name.TakeWhile(char.IsDigit).ToArray());

        //https://stackoverflow.com/questions/8809354/replace-first-occurrence-of-pattern-in-a-string
        //replaces the current index substring with the new index
        Regex regex = new Regex(Regex.Escape(currentIndex));
        string newName = regex.Replace(tutorial.name, index.ToString(), 1);

        tutorial.name = newName;
        tutorial.SetOrder(index);
    }

    public void ChangeTutorialIndices()
    {
        foreach (var tutorial in tutorials)
        {
            SetSingleTutorialIndex(tutorial, index);
            index++;
        }
    }
}
