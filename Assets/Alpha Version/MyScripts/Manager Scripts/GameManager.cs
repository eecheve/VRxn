using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("Controller References")]
    [SerializeField] private Camera mainCamera = null;

    [Header("Direct Interactors")]
    [SerializeField] private XRDirectInteractor rightDirectController = null;
    [SerializeField] private XRDirectInteractor leftDirectController = null;

    [Header("Distance Interactors")]
    [SerializeField] private XRRayInteractor rightDistanceController = null;
    [SerializeField] private XRRayInteractor leftDistanceController = null;

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

        rightDistanceController.onSelectEntered.AddListener(RightGrabbedObject);
        rightDistanceController.onSelectExited.AddListener(RightDropedObject);

        leftDistanceController.onSelectEntered.AddListener(LeftGrabbedObject);
        leftDistanceController.onSelectExited.AddListener(LeftDropedObject);
    }

    private void OnDisable()
    {
        LeftGrabbedChildren.Clear();
        
        rightDirectController.onSelectEntered.RemoveListener(RightGrabbedObject);
        rightDirectController.onSelectExited.RemoveListener(RightDropedObject);

        leftDirectController.onSelectEntered.RemoveListener(LeftGrabbedObject);
        leftDirectController.onSelectExited.RemoveListener(LeftDropedObject);

        rightDistanceController.onSelectEntered.RemoveListener(RightGrabbedObject);
        rightDistanceController.onSelectExited.RemoveListener(RightDropedObject);

        leftDistanceController.onSelectEntered.RemoveListener(LeftGrabbedObject);
        leftDistanceController.onSelectExited.RemoveListener(LeftDropedObject);
    }

    public void RightGrabbedObject(XRBaseInteractable interactable)
    {
        Debug.Log("GameManager: rightHand is being listened to");
        if (interactable != null)
        {
            RightGrabbed = interactable.transform;
            RightGrabbedParent = interactable.transform.parent;
            if(previousRightGrabbed == null)
            {
                previousRightGrabbed = RightGrabbed;
                Debug.Log("GameManager: right hand has grabbed " + RightGrabbed.name);
                Debug.Log("GameManager: it is the first time something is grabbed");
                OnRightFirstGrab?.Invoke();
            }
            else if(previousRightGrabbed != RightGrabbed)
            {
                previousRightGrabbed = RightGrabbed;
                Debug.Log("GameManager: right hand has grabbed " + RightGrabbed.name);
                OnRightHasSwapped?.Invoke();
            }
        }
    }

    public void RightDropedObject(XRBaseInteractable interactable)
    {
        RightGrabbed = null;
        RightGrabbedParent = null;
        Debug.Log("GameManager: right hand has released something");

        OnRightHasDropped?.Invoke();
    }

    public void LeftGrabbedObject(XRBaseInteractable interactable)
    {
        Debug.Log("GameManager: Left hand is being listened to");
        if (interactable != null)
        {
            LeftGrabbed = interactable.transform;
            LeftGrabbedParent = interactable.transform.parent;
            PopulateLeftParentChildren();
            if (previousLeftGrabbed == null)
            {
                previousLeftGrabbed = LeftGrabbed;
                OnLeftFirstGrab?.Invoke();
            }
            else if (previousLeftGrabbed != LeftGrabbed)
            {
                previousLeftGrabbed = LeftGrabbed;
                OnLeftHasSwapped?.Invoke();
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

        OnLeftHasDropped?.Invoke();
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

            OnOneParentRelease?.Invoke();
        }
    }

    private void CheckIfTwoObjectsAreEqual(Transform obj1, Transform obj2)
    {
        if (obj1 == obj2 && obj1.name.Equals(obj2.name))
        {
            SameParentGrab = true;
            OnTwoHandParentGrab?.Invoke();
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

