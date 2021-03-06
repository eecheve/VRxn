using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketCondition : Condition
{
    [SerializeField] private XRSocketInteractor socket = null;
    [SerializeField] private XRBaseInteractable snap = null;
    
    private void OnEnable()
    {
        socket.onSelectEntered.AddListener(CheckForObject);
    }

    private void CheckForObject(XRBaseInteractable snapped)
    {
        if (snapped.Equals(snap))
        {
            Debug.Log("SocketCondition: condition fulfilled");
            condition.FulfillCondition();
        }
    }

    private void OnDisable()
    {
        socket.onSelectEntered.RemoveListener(CheckForObject);
    }
}
