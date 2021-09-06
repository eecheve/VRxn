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

    [Header("Substituent Types")]
    [SerializeField] private Image st_p1TransitionState = null;
    [SerializeField] private Image st_p2TransitionState = null;

    [Header("Orientation Task")]
    [SerializeField] private Image ot_p3RotateInY = null;
    [SerializeField] private Image ot_p4RotateInZ = null;
    [SerializeField] private Image ot_p5RotateInX = null;

    [Header("Model Stages")]
    [SerializeField] private Image ms_p1Stage1 = null;
    [SerializeField] private Image ms_p2Stage1 = null;
    [SerializeField] private Image ms_p2Stage2 = null;
    [SerializeField] private Image ms_p3Stage1 = null;
    [SerializeField] private Image ms_p3Stage2 = null;
    [SerializeField] private Image ms_p3Stage3 = null;
    [SerializeField] private Image ms_p4Stage1 = null;
    [SerializeField] private Image ms_p4Stage2 = null;
    [SerializeField] private Image ms_p4Stage3 = null;
    [SerializeField] private Image ms_p4Stage4 = null;
    [SerializeField] private Image ms_p5Stage1 = null;
    [SerializeField] private Image ms_p5Stage2 = null;
    [SerializeField] private Image ms_p5Stage3 = null;
    [SerializeField] private Image ms_p5Stage4 = null;

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

    public void SetSubstituentTypes()
    {
        st_p1TransitionState.sprite = sceneSprites.Stage1;
        st_p2TransitionState.sprite = sceneSprites.Stage1;
    }

    public void SetRotationTask()
    {
        ot_p3RotateInY.sprite = sceneSprites.Task1;
        ot_p4RotateInZ.sprite = sceneSprites.Task2;
        ot_p5RotateInX.sprite = sceneSprites.Task3;
    }

    public void SetModelStages()
    {
        ms_p1Stage1.sprite = sceneSprites.Stage1;
        ms_p2Stage1.sprite = sceneSprites.Stage1;
        ms_p2Stage2.sprite = sceneSprites.Stage2;
        ms_p3Stage1.sprite = sceneSprites.Stage1;
        ms_p3Stage2.sprite = sceneSprites.Stage2;
        ms_p3Stage3.sprite = sceneSprites.Stage3;
        ms_p4Stage1.sprite = sceneSprites.Stage1;
        ms_p4Stage2.sprite = sceneSprites.Stage2;
        ms_p4Stage3.sprite = sceneSprites.Stage3;
        ms_p4Stage4.sprite = sceneSprites.Stage4;
        ms_p5Stage1.sprite = sceneSprites.Stage1;
        ms_p5Stage2.sprite = sceneSprites.Stage2;
        ms_p5Stage3.sprite = sceneSprites.Stage3;
        ms_p5Stage4.sprite = sceneSprites.Stage4;
    }
}
