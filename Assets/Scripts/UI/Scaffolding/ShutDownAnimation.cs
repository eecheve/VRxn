using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutDownAnimation : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.speed = 0;
    }
}
