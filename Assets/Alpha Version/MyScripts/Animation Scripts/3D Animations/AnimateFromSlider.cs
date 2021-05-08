using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class AnimateFromSlider : MonoBehaviour
{
    private Slider slider;

    private Transform animatable;
    private Animator animator;
    private AnimatorClipInfo[] clipInfos;
    private string clipName;

    private void Awake()
    {
        animatable = CurrentSceneManager.Instance.Animatable;
        animator = animatable.GetComponent<Animator>();
        if (animator != null)
        {
            clipInfos = animator.GetCurrentAnimatorClipInfo(0);
            clipName = clipInfos[0].clip.name;
        }

        slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        slider.onValueChanged.AddListener(AnimateOnSliderValue);
    }

    private void AnimateOnSliderValue(float value)
    {
        animator.Play(clipName, 0, value);
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(AnimateOnSliderValue);
    }
}
