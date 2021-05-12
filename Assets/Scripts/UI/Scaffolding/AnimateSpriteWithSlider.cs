using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateSpriteWithSlider : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    [SerializeField] private AnimationClip clip = null;

    private Slider slider;

    private void OnEnable()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(AnimateSprite);
        animator.speed = 0;
    }

    private void AnimateSprite(float value)
    {
        animator.Play(clip.name, -1, value);
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(AnimateSprite);
    }

    public void ResetAnimation()
    {
        animator.Play(clip.name, -1, 0);
    }
}
