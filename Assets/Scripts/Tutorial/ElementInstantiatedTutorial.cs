using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(ConditionTutorial))]
public class ElementInstantiatedTutorial : MonoBehaviour
{
    [SerializeField] private GameObject prefab = null;
    [SerializeField] private Vertex vertex = null;

    [Header("Optional")]
    [SerializeField] private TextMeshProUGUI tmesh = null;
    [SerializeField] [TextArea] private string correctText = "";
    [SerializeField] [TextArea] private string incorrectText = "";

    private ConditionTutorial tutorial;
    private HexagonalVertex hexVert;
    private TrigonalVertex trigVert;

    private void Awake()
    {
        tutorial = GetComponent<ConditionTutorial>();
    }

    private void OnEnable()
    {
        if(vertex.GetType() == typeof(HexagonalVertex))
        {
            hexVert = (HexagonalVertex)vertex;
            hexVert.OnHexVertOccupied += CheckForInstantiated;
        }
        else if(vertex.GetType() == typeof(TrigonalVertex))
        {
            trigVert = (TrigonalVertex)vertex;
            trigVert.OnTrigVertOccupied += CheckForInstantiated;
        }

        vertex.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void CheckForInstantiated()
    {
        if (vertex.Icon.name.Contains(prefab.name))
        {
            tmesh.text = correctText;
            tutorial.FulfillCondition();
            this.enabled = false;
        }
        else
        {
            tmesh.text = incorrectText;
        }
    }

    private void OnDisable()
    {
        if (vertex.GetType() == typeof(HexagonalVertex))
        {
            hexVert = (HexagonalVertex)vertex;
            hexVert.OnHexVertOccupied -= CheckForInstantiated;
        }
        else if (vertex.GetType() == typeof(TrigonalVertex))
        {
            trigVert = (TrigonalVertex)vertex;
            trigVert.OnTrigVertOccupied -= CheckForInstantiated;
        }

        vertex.GetComponent<SpriteRenderer>().enabled = false;
    }
}
