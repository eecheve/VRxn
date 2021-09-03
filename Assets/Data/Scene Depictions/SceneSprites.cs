using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SceneSprites : ScriptableObject
{
    [Header("Usual Representations")]
    [SerializeField] private Sprite usualReactant = null;
    [SerializeField] private Sprite usualProduct = null;
    [SerializeField] private Sprite reactMOs = null;
    [SerializeField] private Sprite productMOs = null;

    [Header("Stages")]
    [SerializeField] private Sprite stage1 = null;
    [SerializeField] private Sprite stage2 = null;
    [SerializeField] private Sprite stage3 = null;
    [SerializeField] private Sprite stage4 = null;

    [Header("Rotations")]
    [SerializeField] private Sprite task1 = null;
    [SerializeField] private Sprite task2 = null;
    [SerializeField] private Sprite task3 = null;

    [Header("Animation")]
    [SerializeField] private GameObject stage1ToStage2 = null;

    public Sprite UsualReactant { get { return usualReactant; } private set { usualReactant = value; } }
    public Sprite UsualProduct { get { return usualProduct; } private set { usualProduct = value; } }
    public Sprite ReactMOs { get { return reactMOs; } private set { reactMOs = value; } }
    public Sprite ProductMOs { get { return productMOs; } private set { productMOs = value; } }

    public Sprite Stage1 { get { return stage1; } private set { stage1 = value; } }
    public Sprite Stage2 { get { return stage2; } private set { stage2 = value; } }
    public Sprite Stage3 { get { return stage3; } private set { stage3 = value; } }
    public Sprite Stage4 { get { return stage4; } private set { stage4 = value; } }

    public Sprite Task1 { get { return task1; } private set { task1 = value; } }
    public Sprite Task2 { get { return task2; } private set { task2 = value; } }
    public Sprite Task3 { get { return task3; } private set { task3 = value; } }

    public GameObject Stage1ToStage2 { get { return stage1ToStage2; } private set { stage1ToStage2 = value; } }

}
