using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintsButton : MonoBehaviour
{
    [SerializeField] private GameObject leftControllerHints = null;
    [SerializeField] private GameObject rightControllerHints = null;

    private List<SkinnedMeshRenderer> allMeshes = new List<SkinnedMeshRenderer>();

    private void Awake()
    {
        PopulateMeshListFromTransform(leftControllerHints.transform);
        PopulateMeshListFromTransform(rightControllerHints.transform);
    }

    private void PopulateMeshListFromTransform(Transform parent)
    {
        foreach (Transform child in parent)
        {
            SkinnedMeshRenderer mesh = child.GetComponent<SkinnedMeshRenderer>();

            if (mesh != null && !allMeshes.Contains(mesh))
                allMeshes.Add(mesh);
        }
    }

    public void ToggleControllerHints(bool state)
    {
        foreach (var mesh in allMeshes)
        {
            mesh.enabled = state;
        }
    }
}
