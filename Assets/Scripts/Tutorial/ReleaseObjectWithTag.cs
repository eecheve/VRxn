using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConditionTutorial))]
public class ReleaseObjectWithTag : MonoBehaviour
{
    [SerializeField] private string m_tag = "";

    private ConditionTutorial tutorial;
    private UpdateTextAfterCondition updateText;

    private Transform leftGrabbed;
    private Transform rightGrabbed;

    private void Awake()
    {
        tutorial = GetComponent<ConditionTutorial>();
        updateText = GetComponent<UpdateTextAfterCondition>();
    }

    private void OnEnable()
    {
        Debug.Log(name + "Release enabled");
        
        GameManager.OnLeftFirstGrab += GetLeftGrabbed;
        GameManager.OnRightFirstGrab += GetRightGrabbed;

        GameManager.OnLeftHasSwapped += GetLeftGrabbed;
        GameManager.OnRightHasSwapped += GetRightGrabbed;
        
        GameManager.OnLeftHasDropped += LeftObjectDropped;
        GameManager.OnRightHasDropped += RightObjectDropped;
    }

    private void GetLeftGrabbed()
    {
        leftGrabbed = GameManager.Instance.LeftGrabbed;
    }

    private void GetRightGrabbed()
    {
        rightGrabbed = GameManager.Instance.RightGrabbed;
    }

    private void LeftObjectDropped()
    {
        CompareReleaseTag(leftGrabbed);
    }

    private void RightObjectDropped()
    {
        CompareReleaseTag(rightGrabbed);
    }

    private void CompareReleaseTag(Transform grabbed)
    {
        Debug.Log("ReleaseObjectWithTag: releasing " + grabbed.name);
        if (grabbed.CompareTag(m_tag))
        {
            if(updateText != null)
                updateText.UpdateText(true);

            FulfillCondition();
        }
        else
        {
            if (updateText != null)
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
        GameManager.OnLeftHasDropped -= LeftObjectDropped;
        GameManager.OnRightHasDropped -= RightObjectDropped;

        GameManager.OnLeftHasSwapped -= GetLeftGrabbed;
        GameManager.OnRightHasSwapped -= GetRightGrabbed;

        GameManager.OnLeftHasDropped -= LeftObjectDropped;
        GameManager.OnRightHasDropped -= RightObjectDropped;
    }
}
