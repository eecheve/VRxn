using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button3DHelper : MonoBehaviour
{
    [SerializeField] private MeshRenderer arrowPointer = null;
    [SerializeField] private MeshRenderer spriteHolder = null;
    [SerializeField] private SpriteRenderer spriteText = null;

    public void ToggleButtonHelper(bool state)
    {
        arrowPointer.enabled = state;
        spriteHolder.enabled = state;
        spriteText.enabled = state;
    }
}
