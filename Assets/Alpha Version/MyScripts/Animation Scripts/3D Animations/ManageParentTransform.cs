using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageParentTransform : MonoBehaviour
{
    [SerializeField] private Transform rightAnchor = null;

    private List<Transform> children = new List<Transform>();
    
    private bool childWithNoParent = false;
    private bool onLeftEnterAlligned = false; //from AllignmentTrigger(Script) on Left XR Controller
    private bool onRightEnterAlligned = false; //from AllignmentTrigger(Script) on Right XR Controller

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            if(!children.Contains(child))
                children.Add(child);
        }
    }

    private void OnEnable()
    {
        GameManager.OnRightFirstGrab += SetParentToAnchor;
        GameManager.OnRightHasSwapped += SetParentToAnchor;
        GameManager.OnRightHasDropped += SetAnchorToParent;

        ThumbstickWatcher.Instance.onRThumbstickDown.AddListener(ThumbstickNullChildrenParent);
    }

    private void SetParentToAnchor()
    {
        //StartCoroutine(WaitEndOfFrame());

        rightAnchor.position = transform.position;
        rightAnchor.rotation = transform.rotation;
        
        foreach (var child in children)
        {
            if (child.Equals(GameManager.Instance.RightGrabbed))
            {
                Debug.Log("ManageParentT: Detecting " + GameManager.Instance.RightGrabbed.name + " is grabbed");
            }
            else
            {
                child.SetParent(null);
            }
        }
    }

    private void SetAnchorToParent()
    {
        //StartCoroutine(WaitEndOfFrame());

        transform.position = rightAnchor.position;
        transform.rotation = rightAnchor.rotation;

        foreach (var child in children)
        {
            child.SetParent(transform);
        }
        childWithNoParent = false;
    }

    private void ThumbstickNullChildrenParent(bool pressed)
    {
        if (pressed && childWithNoParent == false && onRightEnterAlligned == true)
        {
            Debug.Log("ManageParentT: Setting parent to null from attractor beam");
            rightAnchor.position = transform.position;
            rightAnchor.rotation = transform.rotation;

            //setting parent to null
            foreach (var child in children)
            {
                if (child.Equals(GameManager.Instance.RightGrabbed))
                {
                    Debug.Log("ManageParentT: Detecting " + GameManager.Instance.RightGrabbed.name + " is grabbed");
                }
                else
                {
                    child.SetParent(null);
                }
            }

            childWithNoParent = true;
        }
    }

    private IEnumerator WaitEndOfFrame()
    {
        yield return new WaitForEndOfFrame();
    }

    private void OnDisable()
    {
        GameManager.OnRightFirstGrab -= SetParentToAnchor;
        GameManager.OnRightHasSwapped -= SetParentToAnchor;
        GameManager.OnRightHasDropped -= SetAnchorToParent;

        ThumbstickWatcher.Instance.onRThumbstickDown.RemoveListener(ThumbstickNullChildrenParent);
    }

    public void SetOnEnterAllignedLeft(bool state)
    {
        onLeftEnterAlligned = state;
    }

    public void SetOnEnterAllignedRight(bool state)
    {
        onRightEnterAlligned = state;
    }
}
