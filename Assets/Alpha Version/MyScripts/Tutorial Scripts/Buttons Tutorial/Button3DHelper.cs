using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button3DHelper : MonoBehaviour
{
    //[Header("Button Mesh")]
    //[SerializeField] private MeshRenderer buttonMesh = null;
    //[SerializeField] private Color highlightColor = Color.white;
    
    [Header("Helper Attributes")]
    [SerializeField] private MeshRenderer arrowPointer = null;
    [SerializeField] private MeshRenderer spriteHolder = null;
    [SerializeField] private SpriteRenderer spriteText = null;
    
    [Header("Button Attributes")]
    [SerializeField] private MeshRenderer buttonMesh = null;
    [SerializeField] private Material initialMat = null;
    [SerializeField] private Material highlightMat = null;

    public void ToggleButtonHelper(bool state)
    {
        arrowPointer.enabled = state;
        spriteHolder.enabled = state;
        spriteText.enabled = state;

        if(state == true)
        {
            buttonMesh.material = highlightMat;
        }
        else
        {
            buttonMesh.material = initialMat;
        }
    }
}
