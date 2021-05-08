using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ThumbstickWatcher : ButtonWatcher
{
    #region singleton
    private static ThumbstickWatcher _instance;
    public static ThumbstickWatcher Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("ThumbstickWatcher is NULL.");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    #endregion

    public ButtonPressEvent onLThumbstickTouch;
    public ButtonPressEvent onLThumbstickPress;

    public ButtonPressEvent onRThumbstickTouch;
    public ButtonPressEvent onRThumbstickPress;

    public ButtonPressEvent onLThumbstickUp;
    public ButtonPressEvent onLThumbstickDown;
    public ButtonPressEvent onLThumbstickLeft;
    public ButtonPressEvent onLThumbstickRight;

    public ButtonPressEvent onRThumbstickUp;
    public ButtonPressEvent onRThumbstickDown;
    public ButtonPressEvent onRThumbstickLeft;
    public ButtonPressEvent onRThumbstickRight;

    public bool LeftTouchState { get; private set; } = false;
    public bool RightTouchState { get; private set; } = false;
    
    public bool LeftPressState { get; private set; } = false;
    public bool RightPressState { get; private set; } = false;

    public Vector2 LeftThumbstickInput { get; private set; } = new Vector2();
    public Vector2 RightThumbstickInput { get; private set; } = new Vector2();

    protected override void OnEnable()
    {
        base.OnEnable();

        if (onLThumbstickTouch == null)
            onLThumbstickTouch = new ButtonPressEvent();

        if (onLThumbstickPress == null)
            onLThumbstickPress = new ButtonPressEvent();

        if (onRThumbstickTouch == null)
            onRThumbstickTouch = new ButtonPressEvent();

        if (onRThumbstickPress == null)
            onRThumbstickPress = new ButtonPressEvent();

        if (onLThumbstickUp == null)
            onLThumbstickUp = new ButtonPressEvent();

        if (onLThumbstickDown == null)
            onLThumbstickDown = new ButtonPressEvent();

        if (onLThumbstickLeft == null)
            onLThumbstickLeft = new ButtonPressEvent();

        if (onLThumbstickRight == null)
            onLThumbstickRight = new ButtonPressEvent();

        if (onRThumbstickUp == null)
            onRThumbstickUp = new ButtonPressEvent();

        if (onRThumbstickDown == null)
            onRThumbstickDown = new ButtonPressEvent();

        if (onRThumbstickLeft == null)
            onRThumbstickLeft = new ButtonPressEvent();

        if (onRThumbstickRight == null)
            onRThumbstickRight = new ButtonPressEvent();

        onLThumbstickTouch.AddListener(LeftTouchListener);
        onRThumbstickTouch.AddListener(RightTouchListener);

        onLThumbstickPress.AddListener(LeftPressListener);
        onRThumbstickPress.AddListener(RightPressListener);
    }

    private void Update()
    {
        ManageSinglePress(leftDevice, LeftTouchState, CommonUsages.primary2DAxisTouch, onLThumbstickTouch);
        ManageSinglePress(rightDevice, RightTouchState, CommonUsages.primary2DAxisTouch, onRThumbstickTouch);

        ManageSinglePress(leftDevice, LeftPressState, CommonUsages.primary2DAxisClick, onLThumbstickPress);
        ManageSinglePress(rightDevice, RightPressState, CommonUsages.primary2DAxisClick, onRThumbstickPress);

        if(leftDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 leftValue))
        {
            if (leftValue.x > 0.8)
                onLThumbstickRight.Invoke(true);
            else if (leftValue.x < -0.8)
                onLThumbstickLeft.Invoke(true);
            else if (leftValue.y > 0.8)
                onLThumbstickUp.Invoke(true);
            else if (leftValue.y < -0.8)
                onLThumbstickDown.Invoke(true);

            LeftThumbstickInput = leftValue;
        }

        if (rightDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 rightValue))
        {
            if (rightValue.x > 0.8)
                onRThumbstickRight.Invoke(true);
            else if (rightValue.x < -0.8)
                onRThumbstickLeft.Invoke(true);
            else if (rightValue.y > 0.8)
                onRThumbstickUp.Invoke(true);
            else if (rightValue.y < -0.8)
                onRThumbstickDown.Invoke(true);

            RightThumbstickInput = rightValue;
        }
    }

    private void LeftTouchListener(bool pressed)
    {
        if (pressed)
        {
            LeftTouchState = true;
        }
        else
            LeftTouchState = false;
    }

    private void LeftPressListener(bool pressed)
    {
        if (pressed)
            LeftPressState = true;
        else
            LeftPressState = false;
    }

    private void RightPressListener(bool pressed)
    {
        if (pressed)
            RightPressState = true;
        else
            RightPressState = false;
    }

    private void RightTouchListener(bool pressed)
    {
        if (pressed)
            RightTouchState = true;
        else
            RightTouchState = false;
    }
}
