using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class DrawElement : MonoBehaviour
{
    [Header("Input Attributes")]
    [SerializeField] private InputActionReference leftAction = null;
    [SerializeField] private InputActionReference rightAction = null;

    [Header("Controller References")]
    [SerializeField] private XRRayInteractor leftUIController = null;
    [SerializeField] private XRRayInteractor rightUIController = null;

    [Header("Drawing Attributes")]
    [SerializeField] private LayerMask layerMask = 0;
    [SerializeField] private SelectElement selector = null;
    [SerializeField] private DrawBond drawBond = null;

    private GameObject elementIcon = null; //icon to instantiate, modified externally by pressing buttons
    private int iconCount = 0;

    private List<GameObject> drawnElements = new List<GameObject>(); //these lists will hold the instantiated objects to implement the undo functionality

    private void OnEnable()
    {
        leftAction.action.performed += ManageDrawInput;
        rightAction.action.performed += ManageDrawInput;
    }

    private void ManageDrawInput(InputAction.CallbackContext obj)
    {
        DrawInput(leftUIController.transform);
        DrawInput(rightUIController.transform);
    }

    private void DrawInput(Transform controller)
    {
        Debug.Log("DrawElement: draw input is being called");
        Ray ray = new Ray(controller.position, controller.forward);
        
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, layerMask))
        {
            Debug.Log("DrawElement: raycast detected background to draw");
            if (hit.collider.gameObject.CompareTag("Whiteboard"))
            {
                InstantiateIconOnHit(hit);
            }
        }
        else
        {
            Debug.Log("DrawElement: Raycast does not detect anything!!");
        }
    }

    private void InstantiateIconOnHit(RaycastHit hit)
    {
        if (elementIcon != null)
        {
            Debug.Log("DrawElement: element icon is " + elementIcon.name);
            //var go = Instantiate(elementIcon, hit.point + (hit.transform.forward * -0.01f), hit.transform.rotation, hit.transform);
            var go = Instantiate(elementIcon, hit.point, hit.transform.rotation, hit.transform);
            go.transform.localScale = new Vector3(0.7f, 0.5f, 0.7f);
            iconCount++;
            go.name += iconCount.ToString(); //names necessary because from this it depends the draw bond function.
            drawnElements.Add(go);
        }
        else
        {
            Debug.Log("DrawElement: element icon not detected");
        }
    }

    public void Erase()
    {
        GameObject go = selector.CurrentSelected;
        if (go != null && !go.CompareTag("Whiteboard"))
        {
            if (go.transform.childCount > 0)
            {
                for (int i = 0; i < go.transform.childCount; i++)
                {
                    GameObject child = go.transform.GetChild(i).gameObject;
                    if(child != null)
                    {
                        Debug.Log("DrawElement: object to erase has children");
                        drawBond.SearchAndDestroy(child);
                        SearchAndDestroy(child);
                    }
                }
            }
            SearchAndDestroy(go);
        }
    }

    public void Clear()
    {
        foreach (var element in drawnElements)
        {
            if (element.transform.childCount > 0)
            {
                for (int i = 0; i < element.transform.childCount; i++)
                {
                    GameObject child = element.transform.GetChild(i).gameObject;
                    if (child != null)
                    {
                        Debug.Log("DrawElement: object to erase has children");
                        drawBond.SearchAndDestroy(child);
                        SearchAndDestroy(child);
                    }
                }
            }
            SearchAndDestroy(element);
        }
    }

    private void SearchAndDestroy(GameObject item)
    {
        if (item != null)
        {
            RemoveItemFromList(item, drawnElements);
            Destroy(item);
        }
    }

    private void RemoveItemFromList(GameObject item, List<GameObject> list)
    {
        if (list.Contains(item))
        {
            list.Remove(item);
            list.RemoveAll(GameObject => GameObject == null);
        }
    }

    public void SetElement(GameObject element)
    {
        Debug.Log("DrawElement: setting element to " + element.name);
        elementIcon = element;
    }

    private void OnDisable()
    {
        leftAction.action.performed -= ManageDrawInput;
        rightAction.action.performed -= ManageDrawInput;
    }
}
