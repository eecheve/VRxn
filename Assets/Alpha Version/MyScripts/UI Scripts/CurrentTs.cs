using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentTs : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dieneTMesh = null;
    [SerializeField] private TextMeshProUGUI dienophileTMesh = null;

    private void Awake()
    {
        dieneTMesh.text = CurrentSceneManager.Instance.DieneName;
        dienophileTMesh.text = CurrentSceneManager.Instance.DienophileName;
    }
}
