using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToButtons : MonoBehaviour
{
    [SerializeField] private Image image = null;
    public void UpdateSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }
}
