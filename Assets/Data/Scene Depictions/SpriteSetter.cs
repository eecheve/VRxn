using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSetter : MonoBehaviour
{
    [SerializeField] private SceneSprites sceneSprites = null;
    
    [Header("Current Example")]
    [SerializeField] private Image p1_transitionState = null;
    [SerializeField] private Image p1_product = null;
    [SerializeField] private Image p2_reactantMOs = null;
    [SerializeField] private Image p2_productMOs = null;
    [SerializeField] private Image p3_stage1 = null;
    [SerializeField] private Image p3_stage2 = null;
    [SerializeField] private GameObject p4_stage1ToStage2 = null; //sprite renderer & animator controller
    [SerializeField] private GameObject p5_stage1ToStage2 = null; //sprite renderer & animator controller

    [Header("TS Placement")]
    [SerializeField] private Image ts_p4transitionState = null;
    [SerializeField] private Image ts_p5transitionState = null;

    public void SetCurrentExample()
    {
        p1_transitionState.sprite = sceneSprites.UsualReactant;
        p1_product.sprite = sceneSprites.UsualProduct;
        p2_reactantMOs.sprite = sceneSprites.ReactMOs;
        p2_productMOs.sprite = sceneSprites.ProductMOs;
        p3_stage1.sprite = sceneSprites.Stage1;
        p3_stage2.sprite = sceneSprites.Stage2;

        Sprite initial = sceneSprites.Stage1ToStage2.GetComponent<SpriteRenderer>().sprite;
        var animatorController = sceneSprites.Stage1ToStage2.GetComponent<Animator>().runtimeAnimatorController;
        p4_stage1ToStage2.GetComponent<Animator>().runtimeAnimatorController = animatorController;
        p4_stage1ToStage2.GetComponent<SpriteRenderer>().sprite = initial;
        p5_stage1ToStage2.GetComponent<Animator>().runtimeAnimatorController = animatorController;
        p5_stage1ToStage2.GetComponent<SpriteRenderer>().sprite = initial;
    }

    public void SetTransitionStatePlacement()
    {
        ts_p4transitionState.sprite = sceneSprites.Stage1;
        ts_p5transitionState.sprite = sceneSprites.Stage1;
    }
}
