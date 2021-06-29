using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingManager : MonoBehaviour
{
    public GameObject PreviousSelected { get; set; }
    public GameObject CurrentSelected { get; set; }

    private void Update()
    {
        if(PreviousSelected != null)
        {
            Debug.Log("DrawingManager: previous selected is " + PreviousSelected.name);
        }

        if(CurrentSelected != null)
        {
            Debug.Log("DrawingManager: current selected is " + CurrentSelected.name);
        }
    }
}
