using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSprites : MonoBehaviour
{
    [SerializeField] private Image[] images = null;
    [SerializeField] private Sprite[] oldSprites = null;
    [SerializeField] private Sprite[] newSprites = null;

    public void ChangeSprites()
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].sprite = newSprites[i];
        }
    }

    public void RevertSprites()
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].sprite = oldSprites[i];
        }
    }
}
