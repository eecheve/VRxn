using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputGuide : MonoBehaviour
{
    private List<Button3DHelper> allButtons = new List<Button3DHelper>();

    private bool isOn = false;
    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        
        foreach (var helper in ButtonHelperManager.Instance.GrabHelpers)
        {
            allButtons.Add(helper);
        }
        foreach (var helper in ButtonHelperManager.Instance.SelectHelpers)
        {
            allButtons.Add(helper);
        }

        allButtons.Add(ButtonHelperManager.Instance.HelpHelper);
        allButtons.Add(ButtonHelperManager.Instance.UndoHelper);
        allButtons.Add(ButtonHelperManager.Instance.PrismHelper);
        allButtons.Add(ButtonHelperManager.Instance.ToggleGrabHelper);
        allButtons.Add(ButtonHelperManager.Instance.TeleportHelper);
        allButtons.Add(ButtonHelperManager.Instance.RotateHelper);
    }

    private void OnEnable()
    {
        SecondaryButtonWatcher.Instance.onRightSecondaryPress.AddListener(ToggleCanvas);
    }

    private void ToggleCanvas(bool pressed)
    {
        if (pressed)
        {
            isOn = !isOn;
            canvas.enabled = isOn;
        }
    }

    public void ToggleOffAllHelpers()
    {
        foreach (var helper in allButtons)
        {
            helper.ToggleButtonHelper(false);
        }
    }
    
    public void ToggleHelpHelper(bool state)
    {
        ButtonHelperManager.Instance.ToggleHelpers(ButtonHelperManager.Instance.HelpHelper, state);
    }

    public void ToggleMoveHelpers(bool state)
    {
        ButtonHelperManager.Instance.ToggleHelpers(ButtonHelperManager.Instance.TeleportHelper, state);
    }

    public void ToggleGrabHelpers(bool state)
    {
        ButtonHelperManager.Instance.ToggleHelpers(ButtonHelperManager.Instance.GrabHelpers, state);
    }

    public void ToggleDistanceGrabHelpers(bool state)
    {
        ButtonHelperManager.Instance.ToggleHelpers(ButtonHelperManager.Instance.GrabHelpers, state);
    }

    public void ToggleUndoHelper(bool state)
    {
        ButtonHelperManager.Instance.ToggleHelpers(ButtonHelperManager.Instance.UndoHelper, state);
    }

    public void TogglePrismHelper(bool state)
    {
        ButtonHelperManager.Instance.ToggleHelpers(ButtonHelperManager.Instance.PrismHelper, state);
    }

    public void ToggleMosHelper(bool state)
    {
        ButtonHelperManager.Instance.ToggleHelpers(ButtonHelperManager.Instance.ToggleGrabHelper, state);
    }

    public void ToggleRotateHelpers(bool state)
    {
        ButtonHelperManager.Instance.ToggleHelpers(ButtonHelperManager.Instance.RotateHelper, state);
    }

    private void OnDisable()
    {
        SecondaryButtonWatcher.Instance.onRightSecondaryPress.RemoveListener(ToggleCanvas);
    }
}
