using UnityEngine;
using UnityEngine.XR;

public class PrimaryButtonWatcher : ButtonWatcher
{
    #region singleton
    private static PrimaryButtonWatcher _instance;
    public static PrimaryButtonWatcher Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("PrimaryButtonWatcher is NULL.");
            }
            return _instance;
        }
    }
    #endregion

    public ButtonPressEvent onLeftPrimaryPress;
    public ButtonHoldEvent onLeftPrimaryHold;

    public ButtonPressEvent onRightPrimaryPress;
    public ButtonHoldEvent onRightPrimaryHold;

    public bool leftButtonState { get; private set; } = false;
    public bool rightButtonState { get; private set; } = false;

    private void Awake()
    {
        _instance = this;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        if (onLeftPrimaryPress == null)
            onLeftPrimaryPress = new ButtonPressEvent();

        if (onLeftPrimaryHold == null)
            onLeftPrimaryHold = new ButtonHoldEvent();

        if (onRightPrimaryPress == null)
            onRightPrimaryPress = new ButtonPressEvent();

        if (onRightPrimaryHold == null)
            onRightPrimaryHold = new ButtonHoldEvent();

        onLeftPrimaryPress.AddListener(LeftButtonListener);
        onRightPrimaryPress.AddListener(RightButtonListener);
    }

    private void Update()
    {
        ManageSinglePress(leftDevice, leftButtonState, CommonUsages.primaryButton, onLeftPrimaryPress);
        ManageSinglePress(rightDevice, rightButtonState, CommonUsages.primaryButton, onRightPrimaryPress);

        ManageSustainedPress(leftDevice, CommonUsages.primaryButton, onLeftPrimaryHold);
        ManageSustainedPress(rightDevice, CommonUsages.primaryButton, onRightPrimaryHold);
    }

    private void LeftButtonListener(bool pressed)
    {
        if (pressed)
            leftButtonState = true;
        else
            leftButtonState = false;
    }

    private void RightButtonListener(bool pressed)
    {
        if (pressed)
            rightButtonState = true;
        else
            rightButtonState = false;
    }   
}
