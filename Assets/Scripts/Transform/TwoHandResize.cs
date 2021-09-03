using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TwoHandResize : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] private InputActionReference leftGrab = null;
    [SerializeField] private InputActionReference rightGrab = null;

    [Header("Scalable Objects")]
    [SerializeField] private Transform[] targets = null;

    private Dictionary<string, Transform> targetsDict = new Dictionary<string, Transform>();

    private Transform leftController;
    private Transform rightControler;
    
    private Transform leftObject;
    private Transform rightObject;

    private void Awake()
    {
        leftController = GameManager.Instance.LeftUIController.transform;
        rightControler = GameManager.Instance.RightUIController.transform;

        foreach (var target in targets)
        {
            targetsDict.Add(target.name, target);
        }
    }

    private void OnEnable()
    {
        rightGrab.action.performed += CheckForRightGrab;
        leftGrab.action.performed += CheckForLeftGrab;
    }

    private void CheckForRightGrab(InputAction.CallbackContext obj)
    {
        if (obj.ReadValue<float>() > 0.1)
            leftObject = GameManager.Instance.LeftGrabbed;
        else
            leftObject = null;
    }

    private void CheckForLeftGrab(InputAction.CallbackContext obj)
    {
        if (obj.ReadValue<float>() > 0.1)
            rightObject = GameManager.Instance.RightGrabbed;
        else
            rightObject = null;
    }

    private void Update()
    {
        if(leftObject != null && rightObject != null)
        {
            if(leftObject == rightObject && leftObject.name == rightObject.name) //both hands make reference to the same object
            {
                Vector3 refVector = leftController.position - rightControler.position;
                float magnitude = refVector.magnitude;
                targetsDict.TryGetValue(leftObject.name, out Transform target);
                
                if (target != null)
                {
                    Vector3 newScale = target.localScale * magnitude;
                    target.localScale = newScale;
                }
            }
        }
    }
}
