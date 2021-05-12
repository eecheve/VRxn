using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using System;

public class MoveAndRotate2D : MonoBehaviour
{
    [SerializeField] private InputActionReference selectAction = null;
    [SerializeField] private InputActionReference grabAction = null;
    [SerializeField] private LayerMask whiteboardLayer = 0;
    [SerializeField] private LayerMask backgroundLayer = 0;
    [SerializeField] private SelectElement selector = null;

    private Vector3 m_contrLocalEuler;

    private Transform object2D;
    private Vector3 obj2DLocalEuler;
    public bool GrabIconEnabled { get; set; }
    public bool GrabScaffoldingEnabled { get; set; }

    public delegate void SpriteRotated();
    public SpriteRotated OnSpriteRotated;
    public SpriteRotated OnSpriteNotAssigned;

    private void OnEnable()
    {
        selectAction.action.performed += AssignObject2D;
        grabAction.action.performed += RotateSpriteIfExists;
        
        GrabIconEnabled = false;
    }

    private void AssignObject2D(InputAction.CallbackContext obj)
    {
        GameObject go = selector.CurrentSelected;
        
        if (go != null)
        {
            Debug.Log("MoveAndRotate2D: 2D object is assigned");
            object2D = selector.CurrentSelected.transform;
            m_contrLocalEuler = transform.localEulerAngles;
            obj2DLocalEuler = object2D.localEulerAngles;
        }
        else
        {
            OnSpriteNotAssigned?.Invoke();
        }
    }

    private void RotateSpriteIfExists(InputAction.CallbackContext obj)
    {
        if (obj.ReadValue<float>() > 0.1f && object2D != null)
        {
            if (object2D.CompareTag("Whiteboard") && GrabScaffoldingEnabled)
            {
                Debug.Log("MoveAndRotate2D: moving and rotating the scaffolding");
                RotateObjectWithMaths();
                MoveWithRaycastAgainstLayer(backgroundLayer);
            }
            else if (object2D.CompareTag("Element2D") && GrabIconEnabled)
            {
                Debug.Log("MoveAndRotate2D: moving and rotating one icon");
                MoveWithRaycastAgainstLayer(whiteboardLayer);
            }
            
        }
    }

    private void RotateObjectWithMaths()
    {
        float deltaAngle = m_contrLocalEuler.z - transform.localEulerAngles.z;
        object2D.localEulerAngles = new Vector3(obj2DLocalEuler.x, obj2DLocalEuler.y, (deltaAngle + obj2DLocalEuler.z) * -1);
    }

    private void MoveWithRaycastAgainstLayer(LayerMask layer)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, layer))
        {
            Debug.Log("MoveAndRotate: raycast is detecting something and we can move");
            Vector3 pos = hit.point + (hit.transform.forward * 0.01f);
            object2D.position = pos;
            OnSpriteRotated?.Invoke();
        }
    }

    public void EnableGrabIcons(bool state)
    {
        GrabIconEnabled = state;
        Debug.Log("MoveAndRotate: grabIconEnabled is " + state.ToString());
    }

    public void EnableGrabScaffolding(bool state)
    {
        GrabScaffoldingEnabled = state;
        Debug.Log("MoveAndRotate: grabScaffoldingEnabled is " + state.ToString());
    }

    private void OnDisable()
    {
        selectAction.action.performed -= AssignObject2D;
        grabAction.action.performed -= RotateSpriteIfExists;
    }

}
