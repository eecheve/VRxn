using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
using System.Linq;

public class StrategyDropdowns : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown[] dropdowns = null;
    [SerializeField] private TextMeshProUGUI feedback = null;
    [SerializeField] private Button nextButton = null;


    public int[] ValueList { get; private set; }

    private void OnEnable()
    {
        ValueList = new int[dropdowns.Length];

        for (int i = 0; i < dropdowns.Length; i++)
        {
            int j = i;
            TMP_Dropdown dropdown = dropdowns[i];
            dropdowns[i].onValueChanged.AddListener(delegate
            {
                UpdateValues(dropdown, j);
                CheckValueList();
            });
        }
    }

    private void CheckValueList()
    {
        List<int> values = ValueList.ToList();
        bool allZeroes = values.All(entry => entry == 0);


        int min = values.Min();
        if (min == 1)
        {
            nextButton.enabled = true;
            feedback.text = "";
        }
        else if (allZeroes == true)
        {
            nextButton.enabled = true;
            feedback.text = "";
        }
        else
        {
            feedback.text = $"The minimum value cannot be {min}, please select either nothing or 1 as your first value";
            nextButton.enabled = false;
        }
    }

    private void UpdateValues(TMP_Dropdown dropdown, int index)//, int index)
    {
        int value = dropdown.value;
        Debug.Log($"will update {index} with the value {value}");
        ValueList.SetValue(value, index);
    }

    private void OnDisable()
    {
        for (int i = 0; i < dropdowns.Length; i++)
        {
            int j = i;
            TMP_Dropdown dropdown = dropdowns[i];
            dropdowns[i].onValueChanged.RemoveListener(delegate
            {
                UpdateValues(dropdown, j);
                CheckValueList();
            });
        }
    }
}
