using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Enumerators;

public class RotationTask : MonoBehaviour
{
    [Header("Prompt")]
    [SerializeField] private Image image = null;
    [SerializeField] private Transform initialT = null;

    [Header("Questions")]
    [SerializeField] private SpriteRenderer[] highlighters = null;

    [Header("Feedback")]
    [SerializeField] private TextMeshProUGUI feedback = null;
    [TextArea] [SerializeField] private string completionText = "";
    [SerializeField] private TextMeshProUGUI[] tMeshes = null;

    private Transform grabbable;
    
    private List<VertexPlacement> objectives = new List<VertexPlacement>();
    private int index = 0;
    private Transform lastGrabbed;

    private Animator animator;

    private readonly string correct = "<color=green>correct!</color>";
    private readonly string incorrect = "<color=red>incorrect!</color>";

    public delegate void TaskCompleted();
    public static event TaskCompleted OnTaskCompleted;

    private void Awake()
    {
        image.sprite = CurrentSceneManager.Instance.ModelAlternative;
        
        grabbable = CurrentSceneManager.Instance.Grabbable;
        animator = grabbable.GetComponent<Animator>();

        objectives.Add(VertexPlacement.TopOutsideBack);
        objectives.Add(VertexPlacement.BottomInsideBack);
        objectives.Add(VertexPlacement.TopOutsideFront);
        objectives.Add(VertexPlacement.BottomOutsideFront);
    }

    private void OnEnable()
    {
        GameManager.OnRightFirstGrab += CheckGrabbedObject;
        GameManager.OnLeftFirstGrab += CheckGrabbedObject;
        GameManager.OnRightHasSwapped += CheckGrabbedObject;
        GameManager.OnLeftHasSwapped += CheckGrabbedObject;

        GameManager.OnLeftHasDropped += RotateAndReset;
        GameManager.OnRightHasDropped += RotateAndReset;
    }

    private void CheckGrabbedObject()
    {
        if(GameManager.Instance.LeftGrabbed != null)
        {
            lastGrabbed = GameManager.Instance.LeftGrabbed;
            if(GameManager.Instance.LeftGrabbed.GetComponent<Connectivity>().vertexPlacement == objectives[index])
            {
                ManageObjectivesByIndex();
            }
            else
            {
                tMeshes[index].text = incorrect;
            }
        }
        else if(GameManager.Instance.RightGrabbed != null)
        {
            Debug.Log("RotationTask: I know something was grabbed");
            lastGrabbed = GameManager.Instance.RightGrabbed;
            if (GameManager.Instance.RightGrabbed.GetComponent<Connectivity>().vertexPlacement == objectives[index])
            {
                Debug.Log("RotationTask: I know that I grabbed the right thing!");
                ManageObjectivesByIndex();
            }
            else
            {
                tMeshes[index].text = incorrect;
            }
        }
    }

    private void ManageObjectivesByIndex()
    {
        if (index < objectives.Count)
        {
            if(index < 4)
                tMeshes[index].text = correct;
            
            index++;
            Debug.Log("RotationTask: current index is: " + index.ToString());


            foreach (var highlighter in highlighters)
            {
                Debug.Log("RotationTask: disabling highlighters");
                highlighter.enabled = false;
            }

            if(index < 4)
                highlighters[index].enabled = true;

            if (index == 4)
            {
                Debug.Log("RotationTask: Task is completed!!!!!!!!!!!!!!!!!!");
                
                GameManager.OnRightFirstGrab -= CheckGrabbedObject;
                GameManager.OnLeftFirstGrab -= CheckGrabbedObject;
                GameManager.OnRightHasSwapped -= CheckGrabbedObject;
                GameManager.OnLeftHasSwapped -= CheckGrabbedObject;

                GameManager.OnLeftHasDropped -= RotateAndReset;
                GameManager.OnRightHasDropped -= RotateAndReset;

                feedback.text = completionText;
                
                if (OnTaskCompleted != null)
                    OnTaskCompleted();
            }
        }
    }

    private void RotateAndReset()
    {
        if(lastGrabbed != null && index > 0)
        {
            Connectivity grabbedConnect = lastGrabbed.GetComponent<Connectivity>();
            if(grabbedConnect != null)
            {
                if(grabbedConnect.vertexPlacement == objectives[index - 1])
                {
                    ResetPosition();
                    SetAnimatorMovement();
                }
            }
        }
    }

    private void SetAnimatorMovement()
    {
        animator.SetInteger("index", index);
    }

    private void ResetPosition()
    {
        grabbable.position = initialT.position;
        grabbable.GetComponent<ResetChildrenFromParent>().ResetAllChildrenPos();
    }

    private void OnDisable()
    {
        GameManager.OnRightFirstGrab -= CheckGrabbedObject;
        GameManager.OnLeftFirstGrab -= CheckGrabbedObject;
        GameManager.OnRightHasSwapped -= CheckGrabbedObject;
        GameManager.OnLeftHasSwapped -= CheckGrabbedObject;

        GameManager.OnLeftHasDropped -= RotateAndReset;
        GameManager.OnRightHasDropped -= RotateAndReset;
    }
}
