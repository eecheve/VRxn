using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Enumerators;

public class ButtonsPanel : MonoBehaviour
{
    [Serializable] public struct Icon
    {
        public Button button;
        public GameObject icon;
        public HTMLColor htmlColor;
    }

    [Header("Player Attributes")]
    [SerializeField] private ColorRayLine leftDrawer = null;
    [SerializeField] private ColorRayLine rightDrawer = null;

    [Header("UI Attributes")]
    [SerializeField] private Icon[] iconsInPanel = null;
    [SerializeField] private GameObject[] rotatableSprites = null;
    [SerializeField] private DrawElement drawSystem = null;

    private void OnEnable() //https://answers.unity.com/questions/1288510/buttononclickaddlistener-how-to-pass-parameter-or.html
    {
        foreach (var icon in iconsInPanel)
        {
            icon.button.onClick.AddListener(() => { SetElementIcon(icon.icon); });
            icon.button.onClick.AddListener(() => { SetLinesColor(icon.htmlColor); });
            icon.button.onClick.AddListener(() => { DeactivateOtherButtons(icon); });
        }
    }

    public void ToggleButtonsInPanel(bool state)
    {
        foreach (var icon in iconsInPanel)
        {
            icon.button.enabled = state;
        }
    }

    private void SetElementIcon(GameObject icon)
    {
        Debug.Log("ButtonsPanel: Setting element icon " + icon.name);
        //leftDrawer.SetElement(icon);
        //rightDrawer.SetElement(icon);
        drawSystem.SetElement(icon);
    }

    private void SetLinesColor(HTMLColor color)
    {
        leftDrawer.ChangeLineColor(color.GetStringValue());
        rightDrawer.ChangeLineColor(color.GetStringValue());
    }

    public void ToggleRotatableLayerAndTag(string name)
    {
        foreach (var rotatable in rotatableSprites)
        {
            RotatableSprite rotatableScript = rotatable.GetComponent<RotatableSprite>();
            rotatableScript.UpdateLayerAndTag(name);
        }
    }

    public void ToggleRotatableRenderer(bool state)
    {
        foreach (var rotatable in rotatableSprites)
        {
            rotatable.GetComponentInChildren<SpriteRenderer>().enabled = state;
        }
    }

    private void DeactivateOtherButtons(Icon icon)
    {
        foreach (var other in iconsInPanel)
        {
            if (other.button == icon.button)
                continue;
            else
            {
                ButtonManager bManager = other.button.GetComponent<ButtonManager>();
                if (bManager != null)
                {
                    Debug.Log("ButtonsPanel - getting ButtonManager for " + other.button.name);
                    bManager.ResetButton();
                }
                
            }
        }
    }

    public void ReactantOnProductOff(bool value)
    {
        rotatableSprites[0].gameObject.SetActive(value);
        rotatableSprites[1].gameObject.SetActive(!value);
    }

    private void OnDisable()
    {
        foreach (var icon in iconsInPanel)
        {
            icon.button.onClick.RemoveListener(() => { SetElementIcon(icon.icon); });
            icon.button.onClick.RemoveListener(() => { SetLinesColor(icon.htmlColor); });
            icon.button.onClick.RemoveListener(() => { DeactivateOtherButtons(icon); });
        }
    }
}
