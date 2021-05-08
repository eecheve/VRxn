using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaffolding : MonoBehaviour
{
    private Transform currentTs;
    private List<Rigidbody> anchors = new List<Rigidbody>();

    private MeshRenderer[] meshes;
    private FixedJoint[] joints;

    private void Awake()
    {
        meshes = GetComponentsInChildren<MeshRenderer>();
        joints = GetComponents<FixedJoint>();
    }

    private void OnEnable()
    {
        //GripButtonWatcher.Instance.onLeftGripPress.AddListener(UpdateTS);
        GripButtonWatcher.Instance.onLeftGripPress.AddListener(UpdateTS);
        GripButtonWatcher.Instance.onRightGripPress.AddListener(UpdateTS);
        //TriggerButtonWatcher.Instance.onTriggerPress.AddListener(UpdateTS);
    }

    private void UpdateTS(bool pressed)
    {
        if (pressed)
        {
            if(GameManager.Instance.LeftGrabbedParent != null)
            {
                if(currentTs == null)
                {
                    UpdateAnchors();
                    Debug.Log("grabbing the first Transition State");
                }
                else if(currentTs != GameManager.Instance.LeftGrabbedParent)
                {
                    anchors.Clear();
                    UpdateAnchors();
                    Debug.Log("updating current Transition State");
                }
            }
        }
    }

    private void UpdateAnchors()
    {
        currentTs = GameManager.Instance.LeftGrabbedParent;

        foreach (Transform child in currentTs)
        {
            if (child.CompareTag("Anchor"))
            {
                anchors.Add(child.GetComponent<Rigidbody>());
            }
        }
    }

    public void ToggleMeshes(bool state)
    {
        foreach (var mesh in meshes)
        {
            mesh.enabled = state;
        }
    }

    public void ActivateFixedJoints()
    {
        for (int i = 0; i < anchors.Count; i++)
        {
            joints[i].connectedBody = anchors[i];
            Debug.Log("Scaffolding: connecting joint " + anchors[i].name);
        }
    }

    public void RemoveFixedJoints()
    {
        foreach (var joint in joints)
        {
            joint.connectedBody = null;
        }
    }

    private void OnDisable()
    {
        GripButtonWatcher.Instance.onLeftGripPress.RemoveListener(UpdateTS);
        GripButtonWatcher.Instance.onRightGripPress.RemoveListener(UpdateTS);

        //GripButtonWatcher.Instance.onGripPress.RemoveListener(UpdateTS);
        //TriggerButtonWatcher.Instance.onTriggerPress.RemoveListener(UpdateTS);
    }
}
