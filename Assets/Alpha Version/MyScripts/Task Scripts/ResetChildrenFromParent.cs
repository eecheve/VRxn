﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetChildrenFromParent : MonoBehaviour
{
    private List<Transform> children = new List<Transform>();
    private List<Vector3> positions = new List<Vector3>();


    private void Awake()
    {
        foreach (Transform child in transform)
        {
            children.Add(child);
        }

        for (int i = 0; i < children.Count; i++)
        {
            positions.Add(transform.position - children[i].position);
        }
    }

    public void ResetAllChildrenPos()
    {
        for (int i = 0; i < children.Count; i++)
        {
            children[i].position = transform.position - positions[i];
        }
    }

    public void ResetFirstChildPos()
    {
        children[0].position = transform.position - positions[0];
    }
}
