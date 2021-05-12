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
    [SerializeField] private LayerMask iconLayer = 0;
    [SerializeField] private LayerMask rotatableLayer = 0;

    [Header("Highlighters")]
    [SerializeField] private GameObject currentHl = null;
    [SerializeField] private GameObject lastHl = null;
    [SerializeField] private GameObject bigHl = null;

    public delegate void ElementSelected();
    public event ElementSelected OnElementSelected;
    public event ElementSelected OnElementDeselected;

    public GameObject CurrentSelected { get; private set; }
    public GameObject LastSelected { get; private set; }

    private bool selectionEnabled = false;

    private void OnEnable()
    {
        leftSelect.action.performed += LeftSelectIcon;
        rightSelect.action.performed += RightSelectIcon;

        currentHl.GetComponent<SpriteRenderer>().enabled = false;
        lastHl.GetComponent<SpriteRenderer>().enabled = false;
        bigHl.GetComponent<SpriteRenderer>().enabled = false;

        selectionEnabled = true;
    }

    private void RightSelectIcon(InputAction.CallbackContext obj)
    {
        Ray ray = new Ray(rightController.position, rightController.forward);

        RaycastLogic(ray);
    }

    private void RaycastLogic(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, iconLayer) && selectionEnabled)
        {
            Debug.Log("SelectElement: raycast has detected one icon to select");
            if (CurrentSelected == null)
            {
                Debug.Log("SelectElement: assigning current for the first time");
                HideHighlighters();
                CurrentSelected = hit.collider.gameObject;
                MoveHighlighterToHit(currentHl, hit);
                OnElementSelected?.Invoke();
            }
            else
            {
                if (hit.collider.gameObject != CurrentSelected && CurrentSelected.CompareTag("Whiteboard") == false)
                {
                    Debug.Log("SelectElement: reassigning current and last selected");
                    HideHighlighters();
                    LastSelected = CurrentSelected;
                    MoveHighlighterToHit(lastHl, LastSelected.transform);
                    CurrentSelected = hit.collider.gameObject;
                    MoveHighlighterToHit(currentHl, hit);
                    OnElementSelected?.Invoke();
                }
                else
                {
                    Debug.Log("SelectElement: deselecting things");
                    CurrentSelected = null;
                    HideHighlighters();
                    OnElementDeselected?.Invoke();
                }
            }
        }
        else if(Physics.Raycast(ray, out RaycastHit hit2, 100f, rotatableLayer) && selectionEnabled)
        {
            Debug.Log("SelectElement: raycast has detected scaffolding to select");
            if (CurrentSelected != hit2.collider.gameObject)
            {
                HideHighlighters();
                Debug.Log("SelectElement: selecting " + hit2.collider.gameObject.name);
                CurrentSelected = hit2.collider.gameObject;
                LastSelected = null;
                bigHl.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                Debug.Log("SelectElement: deselecting things 2");
                HideHighlighters();
                CurrentSelected = null;
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
        bigHl.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void SetSelectionState(bool state)
    {
        Debug.Log("SelectElement: setting selection state to " + state.ToString());
        selectionEnabled = state;
    }

    private void OnDisable()
    {
        leftSelect.action.performed -= LeftSelectIcon;
        rightSelect.action.performed -= RightSelectIcon;

        selectionEnabled = false;
    }
}
