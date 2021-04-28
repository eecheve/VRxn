using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandler : MonoBehaviour
{
    [Header("Arrows")]
    [SerializeField] private GameObject arrowUp = null;
    [SerializeField] private GameObject arrowDown = null;
    [SerializeField] private GameObject arrowLeft = null;
    [SerializeField] private GameObject arrowRight = null;

    [Header("Player")]
    [SerializeField] private LayerMask controllersLayer = 0;
    [SerializeField] private Camera mainCamera = null;

    [Header("UI")]
    [SerializeField] private WelcomeScreen welcomeScreen = null;
    
    public delegate void LookAroundTask();
    public event LookAroundTask OnControllersSpotted;

    /*private void Update()
    {
        CastSphere();
    }*/

    private void OnEnable()
    {
        welcomeScreen.OnWelcomeScreenRead += ToggleDownArrow;
    }

    private void ToggleDownArrow()
    {
        arrowDown.SetActive(true);
        Debug.Log("TutorialHandler: Toggling the arrow");
    }

    private void CastSphere()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        if (Physics.SphereCast(ray, 1.0f, out RaycastHit hit, controllersLayer))
        {
            Debug.Log("TutorialHandler: Controllers detected");
        }
    }

    private void OnDisable()
    {
        welcomeScreen.OnWelcomeScreenRead -= ToggleDownArrow;
    }

}
