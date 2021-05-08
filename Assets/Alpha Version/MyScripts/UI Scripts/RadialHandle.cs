using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialHandle : MonoBehaviour
{
    [SerializeField] private RadialFill radialFill = null;

    private Animation anim;

    private void Awake()
    {
        anim = GetComponent<Animation>();
    }

    private void OnEnable()
    {
        radialFill.OnValueChange += AnimateHandleOnChange;
    }

    private void AnimateHandleOnChange(float value)
    {
        anim.Play();
        anim[anim.clip.name].speed = 0; //so it doesn't move by itself

        anim[anim.clip.name].normalizedTime = value;
        if (value == 0)
        {
            anim.Stop();
        }
        else
        {
            anim.Play();
        }
    }

    private void OnDisable()
    {
        radialFill.OnValueChange -= AnimateHandleOnChange;
    }
}
