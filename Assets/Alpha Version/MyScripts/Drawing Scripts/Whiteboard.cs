using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Whiteboard : InputChecker
{
    private XRController leftController;
    private XRController rightController;
    private Vector3 lContrLocalEuler;
    private Vector3 rContrLocalEuler;

    private Transform leftSprite;
    private Transform rightSprite;
    private Vector3 lSprLocalEuler;
    private Vector3 rSprLocalEuler;
    
    private void OnEnable()
    {
        //TriggerButtonWatcher.Instance.onLeftTriggerPress.AddListener(OnLeftTriggerUpdate);
        //TriggerButtonWatcher.Instance.onLeftTriggerPress.AddListener(OnRightTriggerUpdate);

        TriggerButtonWatcher.Instance.onLeftTriggerPress.AddListener(OnLeftTriggerUpdate);
        TriggerButtonWatcher.Instance.onRightTriggerPress.AddListener(OnRightTriggerUpdate);

        rightController = GameManager.Instance.RightUIController.gameObject.GetComponent<XRController>();
        leftController = GameManager.Instance.LeftUIController.gameObject.GetComponent<XRController>();
    }

    private void OnRightTriggerUpdate(bool pressed)
    {
        if (pressed)
        {
            if(rightSprite != null)
            {
                rContrLocalEuler = rightController.transform.localEulerAngles;
                rSprLocalEuler = rightSprite.localEulerAngles;
            }
        }
    }

    private void OnLeftTriggerUpdate(bool pressed)
    {
        if (pressed)
        {
            if(leftSprite != null)
            {
                lContrLocalEuler = leftController.transform.localEulerAngles;
                lSprLocalEuler = leftSprite.localEulerAngles;
            }
        }
    }

    /*private void ManageSustainedPress(XRController controller)
    {
        if (controller == leftController)
        {
            if (CheckForInputPressed(leftController.inputDevice, Trigger) > 0.5f)
            {
                float deltaAngle = lContrLocalEuler.z - leftController.transform.localEulerAngles.z;
                leftSprite.localEulerAngles = new Vector3(lSprLocalEuler.x, lSprLocalEuler.y, deltaAngle - lSprLocalEuler.z);
            }
        }
        else if (controller == rightController)
        {
            if (CheckForInputPressed(rightController.inputDevice, Trigger) > 0.5f)
            {
                float deltaAngle = rContrLocalEuler.z - rightController.transform.localEulerAngles.z;
                Debug.Log(deltaAngle.ToString());
                rightSprite.localEulerAngles = new Vector3(rSprLocalEuler.x, rSprLocalEuler.y, deltaAngle - rSprLocalEuler.z);
            }
        }
        else
        {
            Debug.LogError("Whiteboard_ManageSustainedPress: please assign a correct controller");
        }
    }*/

    public void ManageSustainedPress(XRController controller)
    {
        if (controller == leftController)
        {
            if (CheckForInputPressed(leftController.inputDevice, Trigger) > 0.5f)
            {
                //rotating the sprite on the left controller
                float deltaAngle = lContrLocalEuler.z - leftController.transform.localEulerAngles.z;
                leftSprite.localEulerAngles = new Vector3(lSprLocalEuler.x, lSprLocalEuler.y, deltaAngle + lSprLocalEuler.z);

                //translating the sprite on the left controller
                Vector3 refVector = leftSprite.position - leftController.transform.position;
                float spriteDistance = refVector.magnitude;
                Vector3 lineEnd = leftController.transform.position + (leftController.transform.forward * spriteDistance);
                leftSprite.transform.position = lineEnd;
            }
        }
        else if (controller == rightController)
        {
            if (CheckForInputPressed(rightController.inputDevice, Trigger) > 0.5f)
            {
                float deltaAngle = rContrLocalEuler.z - rightController.transform.localEulerAngles.z;
                Debug.Log(deltaAngle.ToString());
                rightSprite.localEulerAngles = new Vector3(rSprLocalEuler.x, rSprLocalEuler.y, deltaAngle + rSprLocalEuler.z);

                Vector3 refVector = rightSprite.position - rightController.transform.position;
                float spriteDistance = refVector.magnitude;
                Vector3 lineEnd = rightController.transform.position + (rightController.transform.forward * spriteDistance);
                rightSprite.transform.position = lineEnd;
            }
        }
        else
        {
            Debug.LogError("Whiteboard_ManageSustainedPress: please assign a correct controller");
        }
    }

    public void SelectSprite(XRController controller, Transform selection)
    {
        if(controller == leftController)
        {
            if (leftSprite != selection)
                leftSprite = selection;
        }
        else if(controller == rightController)
        {
            if (rightSprite != selection)
                rightSprite = selection;
        }
        else
        {
            Debug.LogError("Whiteboard_SelectSprite: please assign a correct controller");
        }
    }

    private void EmptySprite(Transform sprite)
    {
        sprite = null;
    }

    private void OnDisable()
    {
        //TriggerButtonWatcher.Instance.onLeftTriggerPress.RemoveListener(OnLeftTriggerUpdate);
        //TriggerButtonWatcher.Instance.onLeftTriggerPress.RemoveListener(OnRightTriggerUpdate);

        TriggerButtonWatcher.Instance.onLeftTriggerPress.RemoveListener(OnLeftTriggerUpdate);
        TriggerButtonWatcher.Instance.onLeftTriggerPress.RemoveListener(OnRightTriggerUpdate);
    }

}
