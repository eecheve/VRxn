using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismTab : MonoBehaviour
{
    [SerializeField] private RectTransform rotatable = null;

    private RectTransform initial;

    private void Awake()
    {
        initial.position = rotatable.position;
        initial.rotation = rotatable.rotation;
    }

    public void ResetTransform()
    {
        rotatable.position = initial.position;
        rotatable.rotation = initial.rotation;
    }
}
