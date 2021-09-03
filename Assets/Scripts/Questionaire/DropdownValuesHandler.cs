using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DropdownValuesHandler : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown[] dropdowns = null;
    
    public TMP_Dropdown[] Dropdowns { get { return dropdowns; } set { dropdowns = value; } }

    private int order = 1;

    private void OnEnable()
    {
        for (int i = 0; i < dropdowns.Length; i++)
        {
            int j = i;
            TMP_Dropdown dropdown = dropdowns[i];
            dropdowns[i].onValueChanged.AddListener(delegate
            {
                UpdateValues(dropdown, j);
            });
        }
    }

    private void UpdateValues(TMP_Dropdown dropdown, int index)
    {
        if(dropdown.value > 0)
        {
            order++;
            Debug.Log($"{name} increasing dropdown values to {order}");
            if (order > 8)
                order = 8;
            ManageDropdownCaption(index, order);
            
        }
        else
        {
            order--;
            Debug.Log($"{name} decreasing dropdown values to {order}");
            if (order < 1)
                order = 1;
            ManageDropdownCaption(index, order);
        }
        
        
    }

    private void ManageDropdownCaption(int index, int order)
    {
        for (int i = 0; i < dropdowns.Length; i++)
        {
            if (i == index)
                continue;
            else
            {
                dropdowns[i].options[1].text = order.ToString();
            }
        }
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
            });
        }
    }
}
