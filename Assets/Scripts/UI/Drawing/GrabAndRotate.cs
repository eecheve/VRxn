using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(ActionBasedController))]
public class GrabAndRotate : MonoBehaviour
{
    [SerializeField] private InputActionReference selectAction = null;
    [SerializeField] private InputActionReference grabAction = null;
    [SerializeField] private SelectElement selector = null;
    [SerializeField] private LayerMask grabLayerMask = 0;

    private ActionBasedController m_controller;
    private Vector3 m_contrLocalEuler;

    private Transform object2D;
    private Vector3 obj2DLocalEuler;

    public bool RotationEnabled { get; set; } = false;
    public bool RaycastingToSprite { get; set; }

    public delegate void SpriteRotated();
    public SpriteRotated OnSpriteRotated;

    private void OnEnable()
    {
        m_controller = GetComponent<ActionBasedController>();
        
        RotationEnabled = false;
        RaycastingToSprite = false;

        selectAction.action.performed += SelectSprite;
        grabAction.action.performed += RotateSprite;
    }

    private void SelectSprite(InputAction.CallbackContext obj)
    {
        object2D = selector.CurrentSelected.transform;
        
        if(object2D != null)
        {
            Debug.Log("GrabAndRotate: There is an object to grab");
            m_contrLocalEuler = m_controller.transform.localEulerAngles;
            obj2DLocalEuler = object2D.localEulerAngles;
        }
    }

    private void RotateSprite(InputAction.CallbackContext obj)
    {
        if(obj.ReadValue<float>() > 0.5f && RotationEnabled && object2D != null)
        {
            Debug.Log("MoveAndRotate2D: Button is held & object to rotate is detected in raycast");
            float deltaAngle = m_contrLocalEuler.z - m_controller.transform.localEulerAngles.z;
            object2D.localEulerAngles = new Vector3(obj2DLocalEuler.x, obj2DLocalEuler.y, (deltaAngle + obj2DLocalEuler.z) * -1);

            //translating the sprite on the left controller
            Vector3 refVector = object2D.position - m_controller.transform.position;
            float spriteDistance = refVector.magnitude;
            Vector3 lineEnd = m_controller.transform.position + (m_controller.transform.forward * spriteDistance);
            Vector3 corrected = new Vector3(lineEnd.x, lineEnd.y, 0);

            object2D.transform.position = corrected;

            OnSpriteRotated?.Invoke();
        }
    }

    public void ToggleMoveAndRotate(bool state)
    {
        RotationEnabled = state;
        Debug.Log("MoveAndRotate2D: RotationEnabled is " + state.ToString());
    }

    private void OnDisable()
    {
        selectAction.action.performed -= SelectSprite;
        grabAction.action.performed -= RotateSprite;
    }
}
