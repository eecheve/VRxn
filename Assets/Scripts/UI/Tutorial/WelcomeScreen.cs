using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeScreen : MonoBehaviour
{
    [SerializeField] private GameObject[] panels = null;
    [SerializeField] private Timer timer = null;

    public delegate void WelcomeScreenTask();
    public event WelcomeScreenTask OnWelcomeScreenRead;

    private void Awake()
    {
        TogglePanel(0);
    }

    private void OnEnable()
    {
        timer.OnTimeRanOut += GoToPanel1;    
    }

    private void GoToPanel1()
    {
        panels[0].SetActive(false);
        panels[1].SetActive(true);

        if (OnWelcomeScreenRead != null)
            OnWelcomeScreenRead();
    }

    private void TogglePanel(int index)
    {
        if(panels != null && index < panels.Length)
        {
            for (int i = 0; i < panels.Length; i++)
            {
                if (i == index)
                {
                    panels[i].SetActive(true);
                }
                else
                {
                    panels[i].SetActive(false);
                }
            }
        }
    }
}
