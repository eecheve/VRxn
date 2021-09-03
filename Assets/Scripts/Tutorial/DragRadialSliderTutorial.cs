using UnityEngine;

public class DragRadialSliderTutorial : Condition
{
    [SerializeField] [Range(0, 1)] private float threshold = 0;
    [SerializeField] private RadialFill radialSlider = null;
    [SerializeField] private Transform handle = null;
    [SerializeField] private SpriteRenderer arrow = null;
    [SerializeField] private Vector3 positionOffset = Vector3.zero;

    private void OnEnable()
    {
        radialSlider.OnValueChange += CheckForSliderDragged;

        arrow.transform.position = handle.position + positionOffset;
        arrow.enabled = true;
    }

    private void CheckForSliderDragged(float value)
    {
        if (value > threshold)
        {
            arrow.enabled = false;
            this.enabled = false;
        }
    }

    private void OnDisable()
    {
        condition.FulfillCondition();
        radialSlider.OnValueChange -= CheckForSliderDragged;
    }
}
