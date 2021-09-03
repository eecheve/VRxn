using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FollowupTransformHandler : MonoBehaviour
{
    [SerializeField] private DropdownValuesHandler valuesHandler = null;

    [Header("Possible prompts")]
    [SerializeField] TextMeshProUGUI[] tmeshes = null;

    [Header("Questions")]
    [SerializeField] GameObject[] questions = null;
    [SerializeField] RectTransform[] positions = null;

    private Dictionary<int, GameObject> priorities = new Dictionary<int, GameObject>();
    
    
    public void ArrangeFollowups()
    {
        ResetFollowups();
        
        for (int i = 0; i < valuesHandler.Dropdowns.Length; i++)
        {
            string text = valuesHandler.Dropdowns[i].captionText.text;
            if (int.TryParse(text, out int value))
            {
                priorities.Add(value, questions[i]);
            }
        }

        if (priorities.Count == 0)
            tmeshes[0].gameObject.SetActive(true);
        else if (priorities.Count == 1)
        {
            tmeshes[1].gameObject.SetActive(true);

            priorities[1].SetActive(true);
            priorities[1].transform.position = positions[0].position;
        }
        else if (priorities.Count == 2)
        {
            tmeshes[2].gameObject.SetActive(true);

            priorities[1].SetActive(true);
            priorities[1].transform.position = positions[0].position;

            priorities[2].SetActive(true);
            priorities[2].transform.position = positions[1].position;
        }
        else
        {
            tmeshes[3].gameObject.SetActive(true);

            priorities[1].SetActive(true);
            priorities[1].transform.position = positions[0].position;

            priorities[2].SetActive(true);
            priorities[2].transform.position = positions[1].position;

            priorities[3].SetActive(true);
            priorities[3].transform.position = positions[2].position;
        }
    }

    private void ResetFollowups()
    {
        priorities.Clear();

        foreach (var question in questions)
        {
            question.SetActive(false);
        }
        foreach (var tmesh in tmeshes)
        {
            tmesh.gameObject.SetActive(false);
        }
    }
}
