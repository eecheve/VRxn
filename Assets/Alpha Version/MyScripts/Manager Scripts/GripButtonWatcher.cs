using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

public class GripButtonWatcher : ButtonWatcher
{
    #region singleton
    private static GripButtonWatcher _instance;
    public static GripButtonWatcher Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("GripButtonWatcher is NULL.");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    #endregion

    public ButtonPressEvent onLeftGripPress;
    public ButtonHoldEvent onLeftGripHold;

    public ButtonPressEvent onRightGripPress;
    public ButtonHoldEvent onRightGripHold;

    public bool LeftButtonPressed { get; private set; } = false;
    public bool RightButtonPressed { get; private set; } = false;

    protected override void OnEnable()
    {
        base.OnEnable();

        if (onLeftGripPress == null)
            onLeftGripPress = new ButtonPressEvent();

        if (onLeftGripHold == null)
            onLeftGripHold = new ButtonHoldEvent();

        if (onRightGripPress == null)
            onRightGripPress = new ButtonPressEvent();

        if (onRightGripHold == null)
            onRightGripHold = new ButtonHoldEvent();

        onLeftGripPress.AddListener(LeftButtonListener);
        onRightGripPress.AddListener(RightButtonListener);
    }

    private void Update()
    {
        ManageSinglePress(leftDevice, LeftButtonPressed, CommonUsages.gripButton, onLeftGripPress);
        ManageSinglePress(rightDevice, RightButtonPressed, CommonUsages.gripButton, onRightGripPress);

        ManageSustainedPress(leftDevice, CommonUsages.grip, onLeftGripHold);
        ManageSustainedPress(rightDevice, CommonUsages.grip, onRightGripHold);
    }

    private void LeftButtonListener(bool pressed)
    {
        if (pressed)
            LeftButtonPressed = true;
        else
            LeftButtonPressed = false;
    }

    private void RightButtonListener(bool pressed)
    {
        if (pressed)
            RightButtonPressed = true;
        else
            RightButtonPressed = false;
    }
}
