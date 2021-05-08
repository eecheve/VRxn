using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelExplanation : MonoBehaviour
{
    [Header("Labels")]
    [SerializeField] private Image label = null;

    [Header("Stages")]
    [SerializeField] private Image stage1 = null;
    [SerializeField] private Image stage2 = null;
    [SerializeField] private Image stage3 = null;
    [SerializeField] private Image stage4 = null;

    private void Awake()
    {
        label.sprite = CurrentSceneManager.Instance.Label;
        
        stage1.sprite = CurrentSceneManager.Instance.Stage1;
        stage2.sprite = CurrentSceneManager.Instance.Stage2;
        stage3.sprite = CurrentSceneManager.Instance.Stage3;
        stage4.sprite = CurrentSceneManager.Instance.Stage4;
    }
}
