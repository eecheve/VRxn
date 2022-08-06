using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class PanelNavigation : MonoBehaviour
{
    [SerializeField] private List<GameObject> panels = null;

    private List<Button> nextButtons;
    private List<Button> previousButtons;
    
    private Canvas menuCanvas;
    private GameObject currentPanel;
    private int index = 0;

    private void Awake()
    {
        menuCanvas = GetComponent<Canvas>();
    }

    private void OnEnable() //<---- 8/6/2022 methods inside OnEnable were inside Awake. I haven't tested this change yet. If there is a bug in the UI, it might come from here.
    {
        PopulateButtonLists();
        SetupOnClickEvents();
    }

    private void SetupOnClickEvents()
    {
        if (nextButtons != null)
        {
            foreach (var button in nextButtons)
            {
                button.onClick.AddListener(Next);
            }
        }

        if (previousButtons != null)
        {
            foreach (var button in previousButtons)
            {
                button.onClick.AddListener(Previous);
            }
        }
    }

    private void RemoveOnClickEvents()
    {
        if (nextButtons != null)
        {
            foreach (var button in nextButtons)
            {
                button.onClick.RemoveListener(Next);
            }
        }
        if (previousButtons != null)
        {
            foreach (var button in previousButtons)
            {
                button.onClick.RemoveListener(Previous);
            }
        }
    }

    private void PopulateButtonLists()
    {
        if (panels != null)
        {
            currentPanel = panels[0];
            foreach (var panel in panels)
            {
                foreach (Transform child in panel.transform)
                {
                    if (child.name.Equals("b_next"))
                    {
                        nextButtons.Add(child.gameObject.GetComponent<Button>());
                    }
                    else if (child.name.Equals("b_previous"))
                    {
                        previousButtons.Add(child.gameObject.GetComponent<Button>());
                    }
                }
            }
        }
    }

    public void Next()
    {
        if (index + 1 < panels.Count)
        {
            currentPanel.SetActive(false);
            currentPanel = panels[index + 1];
            currentPanel.SetActive(true);
            index++;
        }
    }

    public void Previous()
    {
        if(index - 1 > 0)
        {
            currentPanel.SetActive(false);
            currentPanel = panels[index - 1];
            currentPanel.SetActive(true);
            index--;
        }
    }

    public void ToggleCanvas(bool state)
    {
        menuCanvas.enabled = state;
    }

    public int GetCurrentPanelIndex()
    {
        return panels.IndexOf(currentPanel);
    }

    public void SetCurrentPanel(int pos)
    {
        if (pos < panels.Count)
        {
            currentPanel = panels[pos];
            index = pos;
        }
    }

    private void OnDisable()
    {
        RemoveOnClickEvents();
    }
}
