using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class WhiteboardSlider : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private AnimationData2D animationData = null;

    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        slider.onValueChanged.AddListener(UpdateSpriteFromValue);
    }

    private void UpdateSpriteFromValue(float value)
    {
        Sprite sprite = animationData.Orientations[(int)value];
        spriteRenderer.sprite = sprite;
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(UpdateSpriteFromValue);
    }
}
