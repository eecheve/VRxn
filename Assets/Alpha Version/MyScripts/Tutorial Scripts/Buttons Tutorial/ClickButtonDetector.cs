using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickButtonDetector : MonoBehaviour
{
    [SerializeField] private Button initialButton = null;
    
    private Button buttonToListen;

    public delegate void ClickDetection();
    public event ClickDetection OnClickDetected;

    public Button InitialButton { get { return initialButton; } set { initialButton = value; } }

    private void OnEnable()
    {
        if(initialButton != null)
        {
            SetButtonToListen(initialButton);
        }
    }

    private void OnDisable()
    {
        if (buttonToListen != null)
            buttonToListen.onClick.RemoveListener(ClickDetected);
    }

    private void ClickDetected()
    {
        if (OnClickDetected != null)
            OnClickDetected();
    }

    public void SetButtonToListen(Button button)
    {
        RestartClickDetection();

        Debug.Log("ClickButtonDetector_SetButtonToListen()");

        buttonToListen = button;
        buttonToListen.onClick.AddListener(ClickDetected);
    }

    public void RestartClickDetection()
    {
        if (buttonToListen != null)
        {
            buttonToListen.onClick.RemoveListener(ClickDetected);
            ToggleButtonAnimation(false);
        }

        buttonToListen = null;
    }

    public void ToggleButtonAnimation(bool state)
    {
        buttonToListen.GetComponentInChildren<Animator>().SetBool("AnimationNeeded", state);
    }
}
