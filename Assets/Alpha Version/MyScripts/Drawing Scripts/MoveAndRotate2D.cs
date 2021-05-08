using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRController))]
public class MoveAndRotate2D : InputChecker
{
    //[SerializeField] private LayerMask drawLayerMask = 0;
    //[SerializeField] private LayerMask moveLayerMask = 0;
    
    private XRController m_controller;
    private Vector3 m_contrLocalEuler;

    private Transform object2D;
    private Vector3 obj2DLocalEuler;
    public bool RotationEnabled { get; set; }
    public bool RaycastingToSprite { get; set; }

    public delegate void SpriteRotated();
    public SpriteRotated OnSpriteRotated;

    private void OnEnable()
    {
        
        m_controller = GetComponent<XRController>();

        if (m_controller.inputDevice == GameManager.Instance.LeftControllerRef.inputDevice)
        {
            TriggerButtonWatcher.Instance.onLeftTriggerPress.AddListener(AssignObject2D);
            TriggerButtonWatcher.Instance.onLeftTriggerHold.AddListener(RotateSpriteIfExists);
        }

        else if (m_controller.inputDevice == GameManager.Instance.RightControllerRef.inputDevice)
        {
            TriggerButtonWatcher.Instance.onRightTriggerPress.AddListener(AssignObject2D);
            TriggerButtonWatcher.Instance.onRightTriggerHold.AddListener(RotateSpriteIfExists);
        }

        RotationEnabled = false;
        RaycastingToSprite = false;
    }

    private void AssignObject2D(bool pressed)
    {
        if (pressed)
        {
            if (object2D != null)
            {
                m_contrLocalEuler = m_controller.transform.localEulerAngles;
                obj2DLocalEuler = object2D.localEulerAngles;

                /*if (RotationEnabled == false && object2D.gameObject.layer != drawLayerMask)
                {
                    object2D.gameObject.layer = drawLayerMask;
                }
                else if (RotationEnabled == false && object2D.gameObject.layer != moveLayerMask)
                {
                    object2D.gameObject.layer = moveLayerMask;
                }*/
            }
        }
    }

    private void RotateSpriteIfExists(bool held)
    {
        if (held && RaycastingToSprite && RotationEnabled && object2D != null)
        {
            Debug.Log("MoveAndRotate2D: Button is held & object to rotate is detected in raycast");
            float deltaAngle = m_contrLocalEuler.z - m_controller.transform.localEulerAngles.z;
            object2D.localEulerAngles = new Vector3(obj2DLocalEuler.x, obj2DLocalEuler.y, (deltaAngle + obj2DLocalEuler.z) * -1);

            //translating the sprite on the left controller
            Vector3 refVector = object2D.position - m_controller.transform.position;
            float spriteDistance = refVector.magnitude;
            Vector3 lineEnd = m_controller.transform.position + (m_controller.transform.forward * spriteDistance);
            object2D.transform.position = lineEnd;

            if (OnSpriteRotated != null)
                OnSpriteRotated();
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
        if (m_controller.inputDevice == GameManager.Instance.LeftControllerRef.inputDevice)
        {
            TriggerButtonWatcher.Instance.onLeftTriggerPress.RemoveListener(AssignObject2D);
            TriggerButtonWatcher.Instance.onLeftTriggerHold.RemoveListener(RotateSpriteIfExists);
        }

        else if (m_controller.inputDevice == GameManager.Instance.RightControllerRef.inputDevice)
        {
            TriggerButtonWatcher.Instance.onRightTriggerPress.RemoveListener(AssignObject2D);
            TriggerButtonWatcher.Instance.onRightTriggerHold.RemoveListener(RotateSpriteIfExists);
        }
    }

}
