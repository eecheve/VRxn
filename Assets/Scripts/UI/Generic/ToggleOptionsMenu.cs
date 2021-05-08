using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ToggleOptionsMenu : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] private InputActionReference toggleAction = null;
    [SerializeField] private InputActionReference thumbstick = null;
    [SerializeField] private InputActionReference deactivateAction = null;

    [Header("UI Attributes")]
    [SerializeField] private Color buttonHighlightColor = Color.white;
    [SerializeField] private Color textHighlightColor = Color.white;
    [SerializeField] private Color inactiveColor = Color.white;
    [SerializeField] private Button upButton = null;
    [SerializeField] private TextMeshProUGUI upTMesh = null;
    [SerializeField] private Button downButton = null;
    [SerializeField] private TextMeshProUGUI downTMesh = null;

    private Canvas canvas;
    private bool canvasState = false;

    private bool optionTopState = false;

    private Color distanceTextColor;
    private Color regularTextColor;

    public delegate void OptionPicked();
    public event OptionPicked OnOptionUp;
    public event OptionPicked OnOptionDown;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();

        distanceTextColor = upTMesh.color;
        regularTextColor = downTMesh.color;
    }

    private void OnEnable()
    {
        toggleAction.action.performed += ToggleUI;
        thumbstick.action.performed += ChooseBetweenOptions;
        deactivateAction.action.performed += DeactivateCanvas;
    }



    private void ToggleUI(InputAction.CallbackContext obj)
    {
        canvasState = !canvasState;
        canvas.enabled = canvasState;
    }

    private void ChooseBetweenOptions(InputAction.CallbackContext obj)
    {
        if (canvasState == true)
        {
            if (obj.ReadValue<Vector2>().y > 0.5)
            {
                upButton.image.color = buttonHighlightColor;
                upTMesh.color = textHighlightColor;

                downButton.image.color = inactiveColor;
                downTMesh.color = regularTextColor;

                if (optionTopState == false)
                {
                    if (OnOptionUp != null)
                    {
                        OnOptionUp();
                        Debug.Log("ToggleOptionsMenu: toggling top option");
                    }
                        
                    optionTopState = true;
                }
            }
            else if (obj.ReadValue<Vector2>().y < -0.5)
            {
                upButton.image.color = inactiveColor;
                upTMesh.color = distanceTextColor;

                downButton.image.color = buttonHighlightColor;
                downTMesh.color = textHighlightColor;

                if (optionTopState == true)
                {
                    if (OnOptionDown != null)
                    {
                        Debug.Log("ToggleOptionsMenu: toggling bottom option");
                        OnOptionDown();
                    }
                        

                    optionTopState = false;
                }
            }
        }
    }

    private void DeactivateCanvas(InputAction.CallbackContext obj)
    {
        canvasState = false;
        canvas.enabled = false;
    }

    private void OnDisable()
    {
        toggleAction.action.performed -= ToggleUI;
        thumbstick.action.performed -= ChooseBetweenOptions;
        deactivateAction.action.performed -= DeactivateCanvas;
    }
}
