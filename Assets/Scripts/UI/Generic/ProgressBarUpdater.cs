using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProgressBarUpdater : MonoBehaviour
{
    [SerializeField] private MeshRenderer progressBar = null;
    [SerializeField] private TextMeshProUGUI tmesh = null;
    [SerializeField] private TutorialManager tutorialManager = null;
    [SerializeField] private int tutorialCount = 0;
    [SerializeField] private float maxValue = 0;

    private Material barMaterial;
    private float barIncrement;
    private float textIncrement;
    private float percentage;

    private void Start()
    {
        barIncrement = (maxValue * 2) / tutorialCount;
        barMaterial = progressBar.material;
        textIncrement = 100f / tutorialCount;
        percentage = 0;
        tmesh.text = percentage.ToString() + "%";
    }

    private void OnEnable()
    {
        tutorialManager.OnTutorialCompleted += UpdateProgressBar;
    }

    private void UpdateProgressBar()
    {
        UpdateShaderFill();

        percentage += textIncrement;
        int round = (int)percentage;
        tmesh.text = round.ToString() + "%";
    }

    private void UpdateShaderFill()
    {
        float current = barMaterial.GetFloat("_FillRate");
        current += barIncrement;


        if (current <= maxValue)
            barMaterial.SetFloat("_FillRate", current);
    }

    private void OnDisable()
    {
        tutorialManager.OnTutorialCompleted -= UpdateProgressBar;
    }
}
