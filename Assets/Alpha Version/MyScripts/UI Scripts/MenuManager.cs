using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private Panel currentPanel = null;
    [SerializeField] private GameObject menuToManage = null;

    //[Header("Parameters")]
    //[SerializeField] private OVRInput.Button menuQuitButton = OVRInput.Button.Four;

    //required by Oculus Store: when user hits back, menu has to go back.
    private List<Panel> panelHistory = new List<Panel>(); 
    //private bool menuIsActive = false;

    //private Canvas canvasRender;

    //private Image menuBackground;

    private void Start()
    {
        SetupPanels();
        //canvasRender = menuToManage.GetComponent<Canvas>();

        //menuBackground = currentPanel.GetComponentInParent<Image>();
        //parentMenu.enabled = false;
        //menuBackground.enabled = false;
    }

    private void SetupPanels()
    {
        Panel[] panels = menuToManage.GetComponentsInChildren<Panel>();

        foreach (Panel panel in panels)
        {
            panel.Setup(this);
        }

        currentPanel.Show();
    }

    /*private void Update()
    {
        if (OVRInput.GetDown(menuQuitButton))
        {
            if (menuIsActive == false)
            {
                canvasRender.enabled = true;
                menuIsActive = true;
            }

            else
            {
                canvasRender.enabled = false;
                menuIsActive = false;
            }
        }
    }*/

    private void SetCurrent(Panel newPanel)
    {
        currentPanel.Hide();

        currentPanel = newPanel;
        currentPanel.Show();
    }

    public void GoToPrevious()
    {
        if (panelHistory.Count == 0)
        {
            //OVRManager.PlatformUIConfirmQuit();
            return;
        }

        int lastIndex = panelHistory.Count - 1;
        SetCurrent(panelHistory[lastIndex]);
        panelHistory.RemoveAt(lastIndex);
    }

    public void SetCurrentWithHistory(Panel newPanel)
    {
        panelHistory.Add(currentPanel);
        SetCurrent(newPanel);
    }

    /*public Panel GetCurrentPanel()
    {
        return currentPanel; //this should not be public, could be a source of problems!
    }*/

    /*public void EnableCanvas()
    {
        parentMenu.enabled = true;
    }*/
}
