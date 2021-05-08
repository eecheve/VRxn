using System;
using System.Collections.Generic;
using UnityEngine;

public class GetTransformsFromList : MonoBehaviour
{
    private static GetTransformsFromList _instance;
    public static GetTransformsFromList Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("GetTransformsFromList is not assigned to game manager");

            return _instance;
        }
    }

    public GameObject objectToAnimate;
    [SerializeField] private GameObject[] animationFrames = null;
    public List<Transform[]> TransformList { get; private set; }
    
    
    public void LoadTransformList()
    {
        _instance = this;
        TransformList = new List<Transform[]>();

        if(animationFrames != null && objectToAnimate != null)
        {
            if (TransformList.Count == 0)
            {
                //ObjectToAnimate = animationFrames[0];
                foreach (var frame in animationFrames)
                {
                    Transform[] transforms = frame.GetComponentsInChildren<Transform>();
                    TransformList.Add(transforms);
                }
            }
            else
            {
                Debug.Log("The transform list has alreay been initiallized");
            }
        }
        else
        {
            Debug.Log("There are no animation frames initiallized or object to animate is null!!!");
        }
    }

    public void UnloadTransformList()
    {
        TransformList.Clear();
        Array.Clear(animationFrames, 0, animationFrames.Length);
    }
}
