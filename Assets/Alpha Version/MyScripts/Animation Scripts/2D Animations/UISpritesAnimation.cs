using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UISpritesAnimation : MonoBehaviour
{
    private Transform observer;
    private Image image;

    private Transform currentTs; 
    private Sprite[] animationFrames;
    private int framesNumber;

    private float deltaAngle;
    
    private void Awake()
    {
        currentTs = CurrentSceneManager.Instance.Grabbable;
        animationFrames = CurrentSceneManager.Instance.RotationAnim.Orientations;
        
        image = GetComponent<Image>();
        image.sprite = animationFrames[0];

        observer = GameManager.Instance.MainCamera.transform;
    }

    private void OnEnable()
    {
        GripButtonWatcher.Instance.onLeftGripHold.AddListener(RotateInReferenceToObserver);
        GripButtonWatcher.Instance.onRightGripHold.AddListener(RotateInReferenceToObserver);
    }

    public void AnimateSpriteOnValue(float value)
    {
        Debug.Log("AnimateSprite" + value.ToString());

        if (animationFrames != null)
        {
            image.sprite = animationFrames[(int)value];
        }

    }

    private void RotateInReferenceToObserver(bool held)
    {
        if (held)
        {
            deltaAngle = (observer.rotation.eulerAngles.y - currentTs.transform.rotation.eulerAngles.y) / 10f;
            if (deltaAngle < 0)
                deltaAngle += 36.0f;

            int deltaValue = (int)deltaAngle;

            if(animationFrames != null)
                image.sprite = animationFrames[deltaValue];
        }
    }

    private void OnDisable()
    {
        GripButtonWatcher.Instance.onLeftGripHold.RemoveListener(RotateInReferenceToObserver);
        GripButtonWatcher.Instance.onRightGripHold.RemoveListener(RotateInReferenceToObserver);
    }
}
