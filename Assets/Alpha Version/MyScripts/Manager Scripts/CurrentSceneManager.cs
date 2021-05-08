using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentSceneManager : MonoSingleton<CurrentSceneManager>
{
    [Header("Molecule Names")]
    [SerializeField] private string dieneName = "";
    [SerializeField] private string dienophileName = "";

    [Header("Molecule Transforms")]
    [SerializeField] private Transform animatable = null;
    [SerializeField] private Transform grabbable = null;

    [Header("2D Animation Attributes")]
    [SerializeField] private AnimationData2D rotationAnim = null;

    [Header("2D Reaction Scheme")]
    [SerializeField] private Sprite usualRepresentation = null;
    [SerializeField] private Sprite modelAlternative = null;
    [SerializeField] private Sprite product = null;

    [Header("2D Molecular Orbitals")]
    [SerializeField] private Sprite dieneMO = null;
    [SerializeField] private Sprite dienophileMO = null;

    [Header("Introduction Sprites")]
    [SerializeField] private Sprite label = null;
    [SerializeField] private Sprite stage1 = null;
    [SerializeField] private Sprite stage2 = null;
    [SerializeField] private Sprite stage3 = null;
    [SerializeField] private Sprite stage4 = null;
    [SerializeField] private Sprite stage5 = null;

    public string DieneName { get { return dieneName; } private set { dieneName = value; } }
    public string DienophileName { get { return dienophileName; } private set { dienophileName = value; } }
    public Transform Animatable { get { return animatable; } private set { animatable = value; } }
    public Transform Grabbable { get { return grabbable; } private set { grabbable = value; } }
    public AnimationData2D RotationAnim { get { return rotationAnim; } private set { rotationAnim = value; } }
    public Sprite UsualRepresentation { get { return usualRepresentation; } private set { usualRepresentation = value; } }
    public Sprite ModelAlternative { get { return modelAlternative; } private set { modelAlternative = value; } }
    public Sprite Product { get { return product; } private set { product = value; } }
    public Sprite DieneMO { get { return dieneMO; } private set { dieneMO = value; } }
    public Sprite DienophileMO { get { return dienophileMO; } private set { dienophileMO = value; } }
    public Sprite Label { get { return label; } private set { label = value; } }
    public Sprite Stage1 { get { return stage1; } private set { stage1 = value; } }
    public Sprite Stage2 { get { return stage2; } private set { stage2 = value; } }
    public Sprite Stage3 { get { return stage3; } private set { stage3 = value; } }
    public Sprite Stage4 { get { return stage4; } private set { stage4 = value; } }

}
