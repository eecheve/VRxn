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
    [SerializeField] private LayerMask drawLayerMask = 0;
    [SerializeField] private LayerMask moveLayerMask = 0;
    [SerializeField] private ColorRayLine leftDrawer = null;
    [SerializeField] private ColorRayLine rightDrawer = null;

    [Header("UI Attributes")]
    [SerializeField] private Icon[] iconsInPanel = null;
    [SerializeField] private GameObject[] rotatableSprites = null;
    //[SerializeField] private DrawLine drawSystem = null; //<--- ORIGINAL ONE, WAS WORKING
    [SerializeField] private Draw drawSystem = null;

    private void OnEnable() //https://answers.unity.com/questions/1288510/buttononclickaddlistener-how-to-pass-parameter-or.html
    {
        foreach (var icon in iconsInPanel)
        {
            icon.button.onClick.AddListener(() => { SetElementIcon(icon.icon); });
            icon.button.onClick.AddListener(() => { SetLinesColor(icon.htmlColor); });
        }
    }

    public void ToggleButtonsInPanel(bool state)
    {
        foreach (var icon in iconsInPanel)
        {
            icon.button.enabled = state;
        }
    }

    public void UpdateRotatablesLayerMask(bool state)
    {
        if(state == true) //rotation is enabled
        {
            foreach (var rotatable in rotatableSprites)
            {
                if(rotatable.activeSelf == true)
                {
                    foreach (Transform child in rotatable.transform)
                    {
                        if (child.gameObject.layer != moveLayerMask)
                            child.gameObject.layer = moveLayerMask;
                    }
                }
                else
                {
                    rotatable.SetActive(true);

                    foreach (Transform child in rotatable.transform)
                    {
                        if (child.gameObject.layer != moveLayerMask)
                            child.gameObject.layer = moveLayerMask;
                    }

                    rotatable.SetActive(false);
                }
            }
        }
        else //rotation is disabled
        {
            foreach (var rotatable in rotatableSprites)
            {
                if(rotatable.activeSelf == true)
                {
                    foreach (Transform child in rotatable.transform)
                    {
                        if (child.gameObject.layer != drawLayerMask)
                            child.gameObject.layer = drawLayerMask;
                    }
                }
                else
                {
                    rotatable.SetActive(true);

                    foreach (Transform child in rotatable.transform)
                    {
                        if (child.gameObject.layer != drawLayerMask)
                            child.gameObject.layer = drawLayerMask;
                    }

                    rotatable.SetActive(false);
                }
                
            }
        }
    }

    private void SetElementIcon(GameObject icon)
    {
        Debug.Log("Setting element icon " + icon.name);
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

    private void OnDisable()
    {
        foreach (var icon in iconsInPanel)
        {
            icon.button.onClick.RemoveListener(() => { SetElementIcon(icon.icon); });
            icon.button.onClick.RemoveListener(() => { SetLinesColor(icon.htmlColor); });
        }
    }

    public void ToggleRotatableRenderer(bool state)
    {
        foreach (var rotatable in rotatableSprites)
        {
            rotatable.GetComponentInChildren<SpriteRenderer>().enabled = state;
        }
    }

    public void ReactantOnProductOff(bool value)
    {
        rotatableSprites[0].gameObject.SetActive(value);
        rotatableSprites[1].gameObject.SetActive(!value);
    }
}
