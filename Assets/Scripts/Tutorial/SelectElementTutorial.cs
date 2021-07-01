using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConditionTutorial))]
public class SelectElementTutorial : MonoBehaviour
{
    [SerializeField] private SpriteRenderer arrow = null;
    [SerializeField] private Vector3 positionOffset = Vector3.zero;
    [SerializeField] private Vertex vertex = null;
    [SerializeField] private SelectElement selector = null;
    
    private ConditionTutorial tutorial;

    private void Awake()
    {
        tutorial = GetComponent<ConditionTutorial>();
    }

    private void OnEnable()
    {
        selector.OnElementSelected += CheckForElementSelected;

        arrow.enabled = true;
        arrow.transform.position = vertex.transform.position + positionOffset;
    }

    private void CheckForElementSelected()
    {
        Icon2D icon = vertex.Icon;
        if(icon != null)
        {
            if (selector.CurrentSelected.name.Contains(icon.name))
            {
                Debug.Log("SelectElementTutorial: element in vertex has been selected");
                this.enabled = false;
            }
        }
    }

    private void OnDisable()
    {
        selector.OnElementSelected -= CheckForElementSelected;
        arrow.enabled = false;
        
        tutorial.FulfillCondition();
    }
}
