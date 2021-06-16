using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeScreen : MonoBehaviour
{
    [SerializeField] private GameObject[] panels = null;

    private void Awake()
    {
        GoToPanel(0);
    }

    public void GoToPanel(int index)
    {
        if(panels != null && index < panels.Length)
        {
            for (int i = 0; i < panels.Length; i++)
            {
                if (i == index)
                    panels[i].SetActive(true);
                
                else
                    panels[i].SetActive(false);
            }
        }
    }
}
