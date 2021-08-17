using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(ConditionTutorial))]
public class GrabObjectWithTag : MonoBehaviour
{
    [SerializeField] private string m_tag = "";
    
    private ConditionTutorial tutorial;
    private SnapMeshToTransform snapMesh;
    private UpdateTextAfterCondition updateText;

    private void Awake()
    {
        tutorial = GetComponent<ConditionTutorial>();
        snapMesh = GetComponent<SnapMeshToTransform>();
        updateText = GetComponent<UpdateTextAfterCondition>();
    }

    private void OnEnable()
    {
        GameManager.OnLeftFirstGrab += LeftObjectGrabbed;
        GameManager.OnRightFirstGrab += RightObjectGrabbed;

        GameManager.OnLeftHasSwapped += LeftObjectGrabbed;
        GameManager.OnRightHasSwapped += RightObjectGrabbed;
    }

    private void LeftObjectGrabbed()
    {
        Transform grabbed = GameManager.Instance.LeftGrabbed;
        ObjectGrabbed(grabbed);
    }

    private void RightObjectGrabbed()
    {
        Transform grabbed = GameManager.Instance.RightGrabbed;
        ObjectGrabbed(grabbed);
    }

    private void ObjectGrabbed(Transform grabbed)
    {
        if (grabbed.CompareTag(m_tag))
        {
            if (snapMesh != null)
            {
                snapMesh.Object = grabbed;
                snapMesh.SnapMesh();
            }

            if (updateText != null)
                updateText.UpdateText(true);

            FulfillCondition(); 
        }
        else
        {
            if(updateText != null)
                updateText.UpdateText(false);
        }
    }

    private void FulfillCondition()
    {
        tutorial.FulfillCondition();
        this.enabled = false;
    }

    private void OnDisable()
    {
        GameManager.OnLeftFirstGrab -= LeftObjectGrabbed;
        GameManager.OnRightFirstGrab -= RightObjectGrabbed;

        GameManager.OnLeftHasSwapped -= LeftObjectGrabbed;
        GameManager.OnRightHasSwapped -= RightObjectGrabbed;
    }
}
