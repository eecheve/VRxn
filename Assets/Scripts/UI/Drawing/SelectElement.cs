using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SelectElement : MonoBehaviour
{
    [Header("Action Attributes")]
    [SerializeField] private InputActionReference leftSelect = null;
    [SerializeField] private InputActionReference rightSelect = null;

    [Header("Controller Attributes")]
    [SerializeField] private Transform leftController = null;
    [SerializeField] private Transform rightController = null;
    
    [Header("Physics Attributes")]
    [SerializeField] private LayerMask layerMask = 0;

    [Header("Highlighters")]
    [SerializeField] private GameObject currentHl = null;
    [SerializeField] private GameObject lastHl = null;

    public delegate void ElementSelected();
    public event ElementSelected OnElementSelected;
    public event ElementSelected OnElementDeselected;

    public GameObject CurrentSelected { get; private set; }
    public GameObject LastSelected { get; private set; }

    private void OnEnable()
    {
        leftSelect.action.performed += LeftSelectIcon;
        rightSelect.action.performed += RightSelectIcon;

        if(currentHl != null && currentHl.GetComponent<SpriteRenderer>() != null)
        {
            currentHl.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (lastHl != null && lastHl.GetComponent<SpriteRenderer>() != null)
        {
            lastHl.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void RightSelectIcon(InputAction.CallbackContext obj)
    {
        Ray ray = new Ray(rightController.position, rightController.forward);

        RaycastLogic(ray);
    }

    private void RaycastLogic(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, layerMask))
        {
            if (CurrentSelected == null)
            {
                CurrentSelected = hit.collider.gameObject;
                MoveHighlighterToHit(currentHl, hit);
                OnElementSelected?.Invoke();
            }
            else
            {
                if (hit.collider.gameObject != CurrentSelected)
                {
                    LastSelected = CurrentSelected;
                    MoveHighlighterToHit(lastHl, LastSelected.transform);
                    CurrentSelected = hit.collider.gameObject;
                    MoveHighlighterToHit(currentHl, hit);
                    OnElementSelected?.Invoke();
                }
                else
                {
                    CurrentSelected = null;
                    HideHighlighters();
                    OnElementDeselected?.Invoke();
                }
            }

            
        }
    }

    private void MoveHighlighterToHit(GameObject highlighter, RaycastHit hit)
    {
        if (highlighter != null)
        {
            highlighter.GetComponent<SpriteRenderer>().enabled = true;
            highlighter.transform.position = hit.point + (hit.transform.forward * -0.075f);
        }
    }

    private void MoveHighlighterToHit(GameObject highlighter, Transform t)
    {
        if (highlighter != null)
        {
            highlighter.GetComponent<SpriteRenderer>().enabled = true;
            highlighter.transform.position = t.position + (t.forward * -0.075f);
        }
    }

    private void LeftSelectIcon(InputAction.CallbackContext obj)
    {
        Ray ray = new Ray(leftController.position, leftController.forward);

        RaycastLogic(ray);
    }

    public void ResetSelected()
    {
        CurrentSelected = null;
        LastSelected = null;
        HideHighlighters();
    }

    private void HideHighlighters()
    {
        currentHl.GetComponent<SpriteRenderer>().enabled = false;
        lastHl.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnDisable()
    {
        leftSelect.action.performed -= LeftSelectIcon;
        rightSelect.action.performed -= RightSelectIcon;
    }
}
