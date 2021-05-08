using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

public class TriggerButtonWatcher : ButtonWatcher
{
    #region singleton
    private static TriggerButtonWatcher _instance;
    public static TriggerButtonWatcher Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("TriggerButtonWatcher is NULL.");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    #endregion

    public ButtonPressEvent onLeftTriggerPress;
    public ButtonHoldEvent onLeftTriggerHold;

    public ButtonPressEvent onRightTriggerPress;
    public ButtonHoldEvent onRightTriggerHold;

    public bool LeftButtonState { get; private set; } = false;
    public bool RightButtonState { get; private set; } = false;

    protected override void OnEnable()
    {
        base.OnEnable();

        if (onLeftTriggerPress == null)
            onLeftTriggerPress = new ButtonPressEvent();

        if (onLeftTriggerHold == null)
            onLeftTriggerHold = new ButtonHoldEvent();

        if (onRightTriggerPress == null)
            onRightTriggerPress = new ButtonPressEvent();

        if (onRightTriggerHold == null)
            onRightTriggerHold = new ButtonHoldEvent();

        onLeftTriggerPress.AddListener(LeftButtonListener);
        onRightTriggerPress.AddListener(RightButtonListener);
    }

    private void Update()
    {
        ManageSinglePress(leftDevice, LeftButtonState, CommonUsages.triggerButton, onLeftTriggerPress);
        ManageSinglePress(rightDevice, RightButtonState, CommonUsages.triggerButton, onRightTriggerPress);

        ManageSustainedPress(leftDevice, CommonUsages.trigger, onLeftTriggerHold);
        ManageSustainedPress(rightDevice, CommonUsages.trigger, onRightTriggerHold);
    }

    private void LeftButtonListener(bool pressed)
    {
        if (pressed)
            LeftButtonState = true;
        else
            LeftButtonState = false;
    }

    private void RightButtonListener(bool pressed)
    {
        if (pressed)
            RightButtonState = true;
        else
            RightButtonState = false;
    }

    private void OnDisable()
    {
        onLeftTriggerPress.RemoveListener(LeftButtonListener);
        onRightTriggerPress.RemoveListener(RightButtonListener);
    }
}
