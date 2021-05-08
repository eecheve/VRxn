using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentFollowingChild : MonoBehaviour
{
    private Transform firstChild;

    private void Awake()
    {
        firstChild = transform.GetChild(0);
    }

    private void Update()
    {
        transform.position = firstChild.position;
    }
}
