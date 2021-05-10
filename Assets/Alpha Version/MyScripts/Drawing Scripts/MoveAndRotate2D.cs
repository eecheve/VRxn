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

    private Vector3 m_contrLocalEuler;

    private Transform object2D;
    private Vector3 obj2DLocalEuler;
    public bool RotationEnabled { get; set; }
    public bool RaycastingToSprite { get; set; }

    public delegate void SpriteRotated();
    public SpriteRotated OnSpriteRotated;

    private void OnEnable()
    {
        selectAction.action.performed += AssignObject2D;
        grabAction.action.performed += RotateSpriteIfExists;
        
        RotationEnabled = false;
        RaycastingToSprite = false;
    }

    private void AssignObject2D(InputAction.CallbackContext obj)
    {
        if (object2D != null)
        {
            m_contrLocalEuler = transform.localEulerAngles;
            obj2DLocalEuler = object2D.localEulerAngles;
        }
    }

    private void RotateSpriteIfExists(InputAction.CallbackContext obj)
    {
        if (obj.ReadValue<float>() > 0.1f && RotationEnabled && object2D != null)
        {
            Debug.Log("MoveAndRotate2D: Button is held & object to rotate is detected in raycast");
            float deltaAngle = m_contrLocalEuler.z - transform.localEulerAngles.z;
            object2D.localEulerAngles = new Vector3(obj2DLocalEuler.x, obj2DLocalEuler.y, (deltaAngle + obj2DLocalEuler.z) * -1);

            //translating the sprite on the left controller
            Vector3 refVector = object2D.position - transform.position;
            float spriteDistance = refVector.magnitude;
            Vector3 lineEnd = transform.position + (transform.forward * spriteDistance);
            Vector3 corrected = new Vector3(lineEnd.x, lineEnd.y, object2D.position.z);

            object2D.transform.position = corrected;

            OnSpriteRotated?.Invoke();
        }
    }
    
    public void SelectSprite(Transform selection)
    {
        if (object2D != selection)
            object2D = selection;
    }

    private void EmptySprite()
    {
        object2D = null;
    }

    public void ToggleMoveAndRotate(bool state)
    {
        RotationEnabled = state;
        Debug.Log("MoveAndRotate: RotationEnabled is " + state.ToString());
    }

    private void OnDisable()
    {
        selectAction.action.performed -= AssignObject2D;
        grabAction.action.performed -= RotateSpriteIfExists;
    }

}
