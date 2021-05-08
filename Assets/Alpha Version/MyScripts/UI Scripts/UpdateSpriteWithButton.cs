using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSpriteWithButton : MonoBehaviour
{
    [SerializeField] private Image prompt = null;
    [SerializeField] private SpriteRenderer hint = null;
    
    public void SetHintSprite(Sprite sprite)
    {
        hint.sprite = sprite;
    }

    public void SetPromptSprite(Sprite sprite)
    {
        prompt.sprite = sprite;
    }
}
