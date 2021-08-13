using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(ConditionTutorial))]
public class LookAround : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private Camera mainCamera = null;

    [Header("Interactable Components")]
    [SerializeField] private LayerMask lookAtLayer = 0;
    [SerializeField] private GameObject objectToObserve = null;
    [SerializeField] private GameObject[] objectsToDestroy = null;
    [SerializeField] private LayerMask aldeadyLookedLayer = 0;

    private ConditionTutorial tutorial;

    private void OnEnable()
    {
        tutorial = GetComponent<ConditionTutorial>();
        tutorial.OnConditionSetCompleted += CloseTutorialSection;
    }

    private void CloseTutorialSection()
    {
        Debug.Log("LookAround: closing the tutorial section");
        if (objectsToDestroy != null)
        {
            foreach (var obj in objectsToDestroy)
                Destroy(obj);
        }
        this.enabled = false;
    }

    private void Update()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        if(Physics.Raycast(ray, out RaycastHit hit, 100f, lookAtLayer))
        {
            if(ReferenceEquals(objectToObserve, hit.collider.gameObject))
            {
                if (!hit.collider.gameObject.CompareTag("ControllerModel"))
                {
                    MeshRenderer mesh = hit.collider.gameObject.GetComponent<MeshRenderer>();
                    if (mesh != null)
                    {
                        Debug.Log("LookAround: object has a mesh and is not a controller");
                        mesh.material.color = Color.gray;
                    }

                    FloatingRing ring = hit.collider.gameObject.GetComponent<FloatingRing>();
                    if (ring != null)
                        ring.Observed();
                }

                hit.collider.gameObject.layer = aldeadyLookedLayer;

                Debug.Log("LookAround: one of the objects was observed");
                tutorial.FulfillCondition();
            }
        }
    }

    private void OnDisable()
    {
        tutorial.OnConditionSetCompleted -= CloseTutorialSection;
    }
}
