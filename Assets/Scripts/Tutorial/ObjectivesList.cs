using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TutorialManager))]
public class ObjectivesList : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmesh = null;
    [SerializeField] [TextArea] private string[] objectives = null;

    private TutorialManager manager;

    private void OnEnable()
    {
        manager = GetComponent<TutorialManager>();
        manager.OnSectionCompleted += UpdateTextMesh;

        tmesh.text = objectives[0];
    }

    private void UpdateTextMesh()
    {
        Debug.Log("ObjectivesList: updating text mesh");

        int index = manager.CurrentTutorial.Order;

        if (index < objectives.Length)
            tmesh.text = objectives[index];
    }

    private void OnDisable()
    {
        manager.OnSectionCompleted -= UpdateTextMesh;
    }
}
