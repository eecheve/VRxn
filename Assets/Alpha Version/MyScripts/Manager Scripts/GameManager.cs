using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("Controller References")]
    [SerializeField] private Camera mainCamera = null;
    [SerializeField] private XRController rightControllerRef = null;
    [SerializeField] private XRController leftControllerRef = null;

    [Header("Direct Interactors")]
    [SerializeField] private XRDirectInteractor rightDirectController = null;
    [SerializeField] private XRDirectInteractor leftDirectController = null;

    [Header("UI Interactors")]
    [SerializeField] private XRRayInteractor rightUIController = null;
    [SerializeField] private XRRayInteractor leftUIController = null;

    public delegate void RightFirstGrab();
    public static event RightFirstGrab OnRightFirstGrab;

    public delegate void LeftFirstGrab();
    public static event LeftFirstGrab OnLeftFirstGrab;

    public delegate void TwoHandParentGrab();
    public static event TwoHandParentGrab OnTwoHandParentGrab;

    public delegate void OneParentRelease();
    public static event OneParentRelease OnOneParentRelease;

    public delegate void RightHasSwapped();
    public static event RightHasSwapped OnRightHasSwapped;

    public delegate void LeftHasSwapped();
    public static event LeftHasSwapped OnLeftHasSwapped;

    public delegate void LeftHasDropped();
    public static event LeftHasDropped OnLeftHasDropped;

    public delegate void RightHasDropped();
    public static event RightHasDropped OnRightHasDropped;

    public List<Transform> LeftGrabbedChildren { get; private set; } //<--refer to this object only when SameParentGrab is true
    public Transform RightGrabbed { get; private set; }
    public Transform LeftGrabbed { get; private set; }
    public Transform RightGrabbedParent { get; private set; }
    public Transform LeftGrabbedParent { get; private set; }
    public bool SameParentGrab { get; private set; }
    

    public Camera MainCamera { get { return mainCamera; } private set { mainCamera = value; } }
    public XRController LeftControllerRef { get { return leftControllerRef; } private set { leftControllerRef = value; } }
    public XRController RightControllerRef { get { return rightControllerRef; } private set { rightControllerRef = value; } }
    public XRDirectInteractor RightDirectController { get { return rightDirectController; } private set { rightDirectController = value; } }
    public XRDirectInteractor LeftDirectController { get { return LeftDirectController; } private set { LeftDirectController = value; } }
    public XRRayInteractor RightUIController { get { return rightUIController; } private set { rightUIController = value; } }
    public XRRayInteractor LeftUIController { get { return leftUIController; } private set { leftUIController = value; } }

    private Transform previousLeftGrabbed = null;
    private Transform previousRightGrabbed = null;

    private void OnEnable()
    {
        LeftGrabbedChildren = new List<Transform>();

        rightDirectController.onSelectEntered.AddListener(RightGrabbedObject);
        rightDirectController.onSelectExited.AddListener(RightDropedObject);

        leftDirectController.onSelectEntered.AddListener(LeftGrabbedObject);
        leftDirectController.onSelectExited.AddListener(LeftDropedObject);
    }

    private void OnDisable()
    {
        LeftGrabbedChildren.Clear();
        
        rightDirectController.onSelectEntered.RemoveListener(RightGrabbedObject);
        rightDirectController.onSelectExited.RemoveListener(RightDropedObject);

        leftDirectController.onSelectEntered.RemoveListener(LeftGrabbedObject);
        leftDirectController.onSelectExited.RemoveListener(LeftDropedObject);
    }

    public void RightGrabbedObject(XRBaseInteractable interactable)
    {
        if (interactable != null)
        {
            RightGrabbed = interactable.transform;
            RightGrabbedParent = interactable.transform.parent;
            if(previousRightGrabbed == null)
            {
                previousRightGrabbed = RightGrabbed;
                if (OnRightFirstGrab != null)
                {
                    OnRightFirstGrab();
                }
            }
            else if(previousRightGrabbed != RightGrabbed)
            {
                previousRightGrabbed = RightGrabbed;
                if (OnRightHasSwapped != null)
                {
                    OnRightHasSwapped();
                }
            }
        }
        Debug.Log("GameManager: right hand has grabbed " + RightGrabbed.name);
    }

    public void RightDropedObject(XRBaseInteractable interactable)
    {
        RightGrabbed = null;
        RightGrabbedParent = null;
        Debug.Log("GameManager: right hand has released something");

        if (OnRightHasDropped != null)
            OnRightHasDropped();
    }

    public void LeftGrabbedObject(XRBaseInteractable interactable)
    {
        if (interactable != null)
        {
            LeftGrabbed = interactable.transform;
            LeftGrabbedParent = interactable.transform.parent;
            PopulateLeftParentChildren();
            if (previousLeftGrabbed == null)
            {
                previousLeftGrabbed = LeftGrabbed;
                if(OnLeftFirstGrab != null)
                {
                    OnLeftFirstGrab();
                    Debug.Log("GameManager: first left grab");
                }
            }
            else if (previousLeftGrabbed != LeftGrabbed)
            {
                previousLeftGrabbed = LeftGrabbed;
                if (OnLeftHasSwapped != null)
                {
                    OnLeftHasSwapped();
                    Debug.Log("GameManager: left has swapped");
                }
            }
        }
        Debug.Log("GameManager: left hand has grabbed " + LeftGrabbed.name);
    }

    private void PopulateLeftParentChildren()
    {
        if (LeftGrabbedParent != null)
        {
            foreach (Transform child in LeftGrabbedParent)
            {
                if (!LeftGrabbedChildren.Contains(child))
                {
                    LeftGrabbedChildren.Add(child);
                }
            }
        }
    }

    public void LeftDropedObject(XRBaseInteractable interactable)
    {
        LeftGrabbed = null;
        LeftGrabbedParent = null;
        ClearLeftParentChildren();

        if (OnLeftHasDropped != null)
            OnLeftHasDropped();
    }

    private void ClearLeftParentChildren()
    {
        if(!LeftGrabbedChildren.Any())
            LeftGrabbedChildren.Clear();
    }

    private void Update()
    {
        ManageSameParentGrab();
    }

    private void ManageSameParentGrab()
    {
        if (LeftGrabbed != null && RightGrabbed != null)
        {
            CheckIfTwoObjectsAreEqual(LeftGrabbedParent, RightGrabbedParent);
        }
        else
        { 
            SameParentGrab = false; 
            
            if(OnOneParentRelease != null)
                OnOneParentRelease();
        }
    }

    private void CheckIfTwoObjectsAreEqual(Transform obj1, Transform obj2)
    {
        if (obj1 == obj2 && obj1.name.Equals(obj2.name))
        {
            SameParentGrab = true;
            if (OnTwoHandParentGrab != null)
                OnTwoHandParentGrab();
        }
        else
            SameParentGrab = false;
    }
    
    private void CheckIfObjectsHaveSameParent(Transform obj1, Transform obj2)
    {
        Transform parent1 = obj1.transform.parent;

        if (obj2.IsChildOf(parent1))
        {
            SameParentGrab = true;
            Debug.Log("Objects have the same parent");
        }
        else
            SameParentGrab = false;
    }

    public void LoadScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber, LoadSceneMode.Single);
    }
}

