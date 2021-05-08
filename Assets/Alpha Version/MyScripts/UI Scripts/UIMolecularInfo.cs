using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using TMPro;
using static Enumerators;
using System;

public class UIMolecularInfo : MonoBehaviour
{
    [Header("Player references")]
    [SerializeField] private XRNode controller = XRNode.LeftHand;

    [Header("UI References")]
    [SerializeField] private Canvas canvasToManage = new Canvas();
    [SerializeField] private TextMeshProUGUI molecularName = null;
    [SerializeField] private Image moleculeImage = null;
    [SerializeField] private Image orbitalImage = null;
    [SerializeField] private TextMeshProUGUI homoEnergyValue = null;
    [SerializeField] private TextMeshProUGUI lumoEnergyValue = null;
    [SerializeField] private Button[] buttons = null;

    private void OnEnable()
    {
        if(controller == XRNode.LeftHand)
        {
            GameManager.OnLeftFirstGrab += UpdateCanvasReferences;
            GameManager.OnLeftHasSwapped += UpdateCanvasReferences;
        }
        else if(controller == XRNode.RightHand)
        {
            GameManager.OnRightFirstGrab += UpdateCanvasReferences;
            GameManager.OnRightHasSwapped += UpdateCanvasReferences;
        }
        else
        {
            Debug.LogError("UIMolecularInfo: please assign a valid contorller");
        }
    }

    private void UpdateCanvasReferences()
    {
        if(controller == XRNode.LeftHand)
        {
            MoleculeData moleculeData = MolecularInfoExtractor.Instance.LeftMoleculeData;
            GetMoleculeReferences(moleculeData);
            ActivateButtonSet(moleculeData);
        }
        else if(controller == XRNode.RightHand)
        {
            MoleculeData moleculeData = MolecularInfoExtractor.Instance.RightMoleculeData;
            GetMoleculeReferences(moleculeData);
            ActivateButtonSet(moleculeData);
        }
        else
        {
            Debug.LogError("UIMolecularInfo: please assign a valid controller");
        }
    }

    private void GetMoleculeReferences(MoleculeData moleculeData)
    {
        if (moleculeData != null)
        {
            Debug.Log("molecule name is " + moleculeData.MoleculeName);
            
            molecularName.text = moleculeData.MoleculeName;
            moleculeImage.sprite = moleculeData.MoleculeSprite;
            homoEnergyValue.text = moleculeData.OrbitalEnergyDict[OrbitalType.Homo].ToString();
            lumoEnergyValue.text = moleculeData.OrbitalEnergyDict[OrbitalType.Lumo].ToString();
        }
    }

    private void ActivateButtonSet(MoleculeData moleculeData) //()
    {
        if(buttons!= null && moleculeData != null)
        {
            if (moleculeData.OrbitalCoeffsDict.Count == 2)
            {
                foreach (var button in buttons)
                {
                    button.targetGraphic.color = Color.gray;
                    button.enabled = false;
                }
                buttons[2].enabled = true;
                buttons[2].targetGraphic.color = Color.white;
                buttons[3].enabled = true;
                buttons[3].targetGraphic.color = Color.white;
            }
            else if (moleculeData.OrbitalCoeffsDict.Count == 4)
            {
                foreach (var button in buttons)
                {
                    button.targetGraphic.color = Color.white;
                    button.enabled = true;
                }
                buttons[0].enabled = false;
                buttons[0].targetGraphic.color = Color.gray;
                buttons[5].enabled = false;
                buttons[5].targetGraphic.color = Color.gray;
            }
            else if (moleculeData.OrbitalCoeffsDict.Count == 6)
            {
                foreach (var button in buttons)
                {
                    button.targetGraphic.color = Color.white;
                    button.enabled = true;
                }
            }
        }
    }

    public void UpdateOrbitalImage(string orbitalType)
    {
        Enum.TryParse(orbitalType, out OrbitalType orbital);
        if(controller == XRNode.LeftHand)
        {
            MoleculeData moleculeData = MolecularInfoExtractor.Instance.LeftMoleculeData;
            if (moleculeData != null)
            {
                Debug.Log("UIMolecularInfo_UpdateOrbiralImage(): molecule info accessed");
                orbitalImage.sprite = moleculeData.OrbitalDepictions[orbital];
            }
        }
        else if(controller == XRNode.RightHand)
        {
            MoleculeData moleculeData = MolecularInfoExtractor.Instance.RightMoleculeData;
            if (moleculeData != null)
            {
                orbitalImage.sprite = moleculeData.OrbitalDepictions[orbital];
            }
        }
    }

    public void ResetOrbitalImage()
    {
        orbitalImage.sprite = null;
    }

    private void OnDisable()
    {
        if (controller == XRNode.LeftHand)
        {
            GameManager.OnLeftFirstGrab -= UpdateCanvasReferences;
            GameManager.OnLeftHasSwapped -= UpdateCanvasReferences;
        }
        else if (controller == XRNode.RightHand)
        {
            GameManager.OnRightFirstGrab -= UpdateCanvasReferences;
            GameManager.OnRightHasSwapped -= UpdateCanvasReferences;
        }
        else
        {
            Debug.LogError("UIMolecularInfo: please assign a valid contorller");
        }
    }
}
