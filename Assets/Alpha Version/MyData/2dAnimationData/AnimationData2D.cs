using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AnimationData2D : ScriptableObject
{
    [SerializeField] private string transitionStateName = "";
    [SerializeField] private Sprite[] orientations = null;

    public string TransitionStateName { get; private set; }
    public Sprite[] Orientations { get; private set; }

    private void Awake()
    {
        TransitionStateName = transitionStateName;
        Orientations = orientations;
    }
}
