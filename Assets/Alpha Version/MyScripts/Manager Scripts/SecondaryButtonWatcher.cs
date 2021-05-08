using UnityEngine;
using UnityEngine.XR;

public class SecondaryButtonWatcher : ButtonWatcher
{
    #region singleton
    private static SecondaryButtonWatcher _instance;
    public static SecondaryButtonWatcher Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("SecondaryButtonWatcher is NULL.");
            }
            return _instance;
        }
    }
    #endregion

    public ButtonPressEvent onLeftSecondaryPress;
    public ButtonHoldEvent onLeftSecondaryHold;

    public ButtonPressEvent onRightSecondaryPress;
    public ButtonHoldEvent onRightSecondaryHold;

    public bool leftButtonState { get; private set; } = false;
    public bool rightButtonState { get; private set; } = false;

    private void Awake()
    {
        _instance = this;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        if (onLeftSecondaryPress == null)
            onLeftSecondaryPress = new ButtonPressEvent();

        if (onLeftSecondaryHold == null)
            onLeftSecondaryHold = new ButtonHoldEvent();

        if (onRightSecondaryPress == null)
            onRightSecondaryPress = new ButtonPressEvent();

        if (onRightSecondaryHold == null)
            onRightSecondaryHold = new ButtonHoldEvent();

        onLeftSecondaryPress.AddListener(LeftButtonListener);
        onRightSecondaryPress.AddListener(RightButtonListener);
    }

    private void Update()
    {
        ManageSinglePress(leftDevice, leftButtonState, CommonUsages.secondaryButton, onLeftSecondaryPress);
        ManageSinglePress(rightDevice, rightButtonState, CommonUsages.secondaryButton, onRightSecondaryPress);

        ManageSustainedPress(leftDevice, CommonUsages.secondaryButton, onLeftSecondaryHold);
        ManageSustainedPress(rightDevice, CommonUsages.secondaryButton, onRightSecondaryHold);
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
