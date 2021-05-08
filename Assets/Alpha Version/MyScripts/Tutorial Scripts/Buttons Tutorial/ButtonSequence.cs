using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSequence : MonoBehaviour
{
    [Header("Pointer")]
    [SerializeField] private Image pointer = null;
    
    [Header("Buttons")]
    [SerializeField] private List<Button> buttons = new List<Button>();

    private bool[] flags;
    private ClickButtonDetector buttonDetector;

    private void Awake()
    {
        buttonDetector = GetComponent<ClickButtonDetector>();
        buttonDetector.ToggleButtonAnimation(true);

        flags = new bool[buttons.Count];
        for (int i = 0; i < flags.Length; i++)
        {
            flags[i] = false;
        }
    }

    private void OnEnable()
    {
        buttonDetector.OnClickDetected += StopHighlight;

        OppositeVerticesConnector.OnBackboneCompleted += SetNextBuildButton;
        TopOutsideManager.OnDrawingTaskCompleted += SetMoveButton;
    }

    private void SetMoveButton()
    {
        if(flags[2] == false)
        {
            buttonDetector.SetButtonToListen(buttons[2]);
            buttonDetector.ToggleButtonAnimation(true);
        }
    }

    private void StopHighlight()
    {
        Debug.Log("ButtonSequence_StopHighlight()");
        buttonDetector.RestartClickDetection();

        for (int i = 0; i < flags.Length; i++)
        {
            if(flags[i] == false)
            {
                flags[i] = true;
                break;
            }
        }

        pointer.enabled = false;
    }

    private void SetNextBuildButton()
    {
        if(flags[1] == false)
        {
            buttonDetector.SetButtonToListen(buttons[1]);
            buttonDetector.ToggleButtonAnimation(true);
            pointer.enabled = true;
        }
    }



    public void ResetButtonInstructions()
    {
        buttonDetector.SetButtonToListen(buttons[0]);
        buttonDetector.ToggleButtonAnimation(true);

        for (int i = 0; i < flags.Length; i++)
        {
            flags[i] = false;
        }
        pointer.enabled = true;
    }

    private void OnDisable()
    {
        buttonDetector.OnClickDetected -= StopHighlight;

        OppositeVerticesConnector.OnBackboneCompleted -= SetNextBuildButton;
    }
}
