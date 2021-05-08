using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatableSprite : MonoBehaviour
{
    public void UpdateLayerAndTag(string name)
    {
        gameObject.layer = LayerMask.NameToLayer(name);
        gameObject.tag = name;
    }
}
