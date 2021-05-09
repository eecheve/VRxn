using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SelectElement : MonoBehaviour
{
    [SerializeField] private InputActionReference leftSelect = null;
    [SerializeField] private InputActionReference rightSelect = null;
    [SerializeField] private Transform leftController = null;
    [SerializeField] private Transform rightController = null;
    [SerializeField] private LayerMask layerMask = 0;

    public delegate void ElementSelected();
    public event ElementSelected OnElementSelected;

    public GameObject CurrentSelected { get; private set; }
    public GameObject LastSelected { get; private set; }

    private void OnEnable()
    {
        leftSelect.action.performed += LeftSelectIcon;
        rightSelect.action.performed += RightSelectIcon;
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
            }
            else
            {
                LastSelected = CurrentSelected;
                CurrentSelected = hit.collider.gameObject;
            }

            OnElementSelected?.Invoke();
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
    }

    private void OnDisable()
    {
        leftSelect.action.performed -= LeftSelectIcon;
        rightSelect.action.performed -= RightSelectIcon;
    }
}
