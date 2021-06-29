using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ConditionTutorial))]
public class GrabMoleculeTutorial : MonoBehaviour
{
    [SerializeField] private GameObject molecule = null;
    
    //[SerializeField] private InputActionReference grabAction = null;
    private ConditionTutorial condition = null;
    private Transform[] elements;

    private void Awake()
    {
        elements = molecule.GetComponentsInChildren<Transform>();
    }

    private void OnEnable()
    {
        //grabAction.action.performed += CheckForMoleculeGrabbed;
        condition = GetComponent<ConditionTutorial>();
        
        GameManager.OnLeftFirstGrab += LeftMoleculeGrabbed;
        GameManager.OnRightFirstGrab += RightMoleculeGrabbed;

        GameManager.OnLeftHasSwapped += LeftMoleculeGrabbed;
        GameManager.OnRightHasSwapped += RightMoleculeGrabbed;
    }

    private void LeftMoleculeGrabbed()
    {
        if (GameManager.Instance.LeftGrabbed != null)
        {
            foreach (var element in elements)
            {
                if (element.name.Equals(GameManager.Instance.LeftGrabbed.name))
                {
                    condition.FulfillCondition();
                    this.enabled = false;
                }
            }
        }
    }

    private void RightMoleculeGrabbed()
    {
        if (GameManager.Instance.RightGrabbed != null)
        {
            foreach (var element in elements)
            {
                if (element.name.Equals(GameManager.Instance.RightGrabbed.name))
                {
                    condition.FulfillCondition();
                    this.enabled = false;
                }
            }
        }
    }


    private void OnDisable()
    {
        GameManager.OnLeftFirstGrab -= LeftMoleculeGrabbed;
        GameManager.OnRightFirstGrab -= RightMoleculeGrabbed;

        GameManager.OnLeftHasSwapped += LeftMoleculeGrabbed;
        GameManager.OnRightHasSwapped += RightMoleculeGrabbed;
    }
}
