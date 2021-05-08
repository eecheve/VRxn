using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIVisualHelper : MonoBehaviour
{
    [Header("Reaction Scheme")]
    [SerializeField] private Image reactantImage = null;
    [SerializeField] private Image productImage = null;

    [Header("Molecular Orbitals")]
    [SerializeField] private Image dieneImage = null;
    [SerializeField] private Image dienophileImage = null;

    [Header("Buttons")]
    [SerializeField] private Button prismButton = null;
    [SerializeField] private Button moButton = null;

    private Transform animatable;
    private Transform grabbable;
   
    private void Awake()
    {
        animatable = CurrentSceneManager.Instance.Animatable;
        grabbable = CurrentSceneManager.Instance.Grabbable;

        dieneImage.sprite = CurrentSceneManager.Instance.DieneMO;
        dienophileImage.sprite = CurrentSceneManager.Instance.DienophileMO;

        reactantImage.sprite = CurrentSceneManager.Instance.UsualRepresentation;
        productImage.sprite = CurrentSceneManager.Instance.Product;
    }

    private void OnEnable()
    {
        SecondaryButtonWatcher.Instance.onLeftSecondaryPress.AddListener(Toggle3DPrism);
        PrimaryButtonWatcher.Instance.onLeftPrimaryPress.AddListener(Toggle3DMos);
    }

    private void Toggle3DPrism(bool pressed)
    {
        if (pressed)
        {
            prismButton.onClick.Invoke();
        }
    }

    private void Toggle3DMos(bool pressed)
    {
        if (pressed)
        {
            moButton.onClick.Invoke();
        }
    }

    public void ToggleMOs(bool state)
    {
        ToggleMOMeshes(grabbable, state);
        ToggleMOMeshes(animatable, state);

        dieneImage.gameObject.SetActive(state);
        dienophileImage.gameObject.SetActive(state);
    }

    private void ToggleMOMeshes(Transform parent, bool state)
    {
        if (parent != null)
        {
            Debug.Log("UpdateMOs_ToggleMOs: currentMolecule exists");
            foreach (Transform child in parent)
            {
                foreach (Transform granChild in child)
                {
                    if (granChild.name.Contains("pz"))
                    {
                        Debug.Log("UpdateMOs: granChild has pz");
                        foreach (Transform grandGrandChild in granChild)
                        {
                            Debug.Log("UpdateMOs: setting mesh to " + state.ToString());
                            MeshRenderer mesh = grandGrandChild.GetComponent<MeshRenderer>();
                            mesh.enabled = state;
                        }
                    }
                }
            }
        }
    }

    public void ToggleReferenceFrame(bool state)
    {
        ToggleRefMeshes(grabbable, state);
        ToggleRefMeshes(animatable, state);

        if (state == true)
            reactantImage.sprite = CurrentSceneManager.Instance.ModelAlternative;//model;
        else
            reactantImage.sprite = CurrentSceneManager.Instance.UsualRepresentation;//alternative;
    }

    private void ToggleRefMeshes(Transform parent, bool state)
    {
        if (parent != null)
        {
            foreach (Transform child in parent)
            {
                if (child.name.Contains("ref"))
                {
                    foreach (Transform grandChild in child)
                    {
                        grandChild.GetComponent<MeshRenderer>().enabled = state;
                    }
                }
            }
        }
    }
}
