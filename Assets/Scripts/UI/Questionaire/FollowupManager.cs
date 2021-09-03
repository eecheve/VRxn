using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;

public class FollowupManager : MonoBehaviour
{
    [SerializeField] private StrategyDropdowns strategies = null;

    [Header("Possible prompts")]
    [SerializeField] TextMeshProUGUI tmesh0 = null;
    [SerializeField] TextMeshProUGUI tmesh1 = null;
    [SerializeField] TextMeshProUGUI tmesh2 = null;
    [SerializeField] TextMeshProUGUI tmesh3 = null;

    [Header("Questions")]
    [SerializeField] GameObject[] questions = null;
    [SerializeField] RectTransform[] positions = null;

    private Dictionary<int, GameObject> priorities = new Dictionary<int, GameObject>();
    private int[] values;

    public void ArrangeFollowups()
    {
        values = strategies.ValueList;

        for (int i = 0; i < values.Length; i++)
        {
            if (values[i] != 0) // the value corresponds to the priority for the dropdown option
            {
                if (priorities.ContainsKey(values[i]) == false)
                    priorities.Add(values[i], questions[i]);
            }
        }

        if (priorities.Count == 0)
            tmesh0.gameObject.SetActive(true);
        else if (priorities.Count == 1)
        {
            tmesh1.gameObject.SetActive(true);

            priorities[1].SetActive(true);
            priorities[1].transform.position = positions[0].position;
        }
        else if (priorities.Count == 2)
        {
            tmesh2.gameObject.SetActive(true);

            priorities[1].SetActive(true);
            priorities[1].transform.position = positions[0].position;

            priorities[2].SetActive(true);
            priorities[2].transform.position = positions[1].position;
        }
        else
        {
            tmesh3.gameObject.SetActive(true);

            priorities[1].SetActive(true);
            priorities[1].transform.position = positions[0].position;

            priorities[2].SetActive(true);
            priorities[2].transform.position = positions[1].position;

            priorities[3].SetActive(true);
            priorities[3].transform.position = positions[2].position;
        }
    }

     public void ResetFollowps()
    {
        priorities.Clear();
    }
}
