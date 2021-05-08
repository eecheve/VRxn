using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGrabs : MonoBehaviour
{
    [SerializeField] private ToggleOptionsMenu toggleMenu = null;

    private void OnEnable()
    {
        toggleMenu.OnOptionDown += ToggleRegularGrab;
        toggleMenu.OnOptionUp += ToggleDistanceGrab;
    }

    private void ToggleDistanceGrab()
    {
        Debug.Log("ToggleGrabs: ToggleDistanceGrab()");

        ControllerReferences.Instance.RRayInteractor.gameObject.SetActive(true);
        ControllerReferences.Instance.RDirectInteractor.gameObject.SetActive(false);

        ControllerReferences.Instance.LRayInteractor.gameObject.SetActive(true);
        ControllerReferences.Instance.LDirectInteractor.gameObject.SetActive(false);
    }

    private void ToggleRegularGrab()
    {
        Debug.Log("ToggleGrabs: ToggleRegularGrab()");

        ControllerReferences.Instance.RRayInteractor.gameObject.SetActive(false);
        ControllerReferences.Instance.RDirectInteractor.gameObject.SetActive(true);

        ControllerReferences.Instance.LRayInteractor.gameObject.SetActive(false);
        ControllerReferences.Instance.LDirectInteractor.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        toggleMenu.OnOptionDown -= ToggleRegularGrab;
        toggleMenu.OnOptionUp -= ToggleDistanceGrab;
    }
}
