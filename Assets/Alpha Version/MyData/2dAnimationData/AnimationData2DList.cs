using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationData2DList : MonoSingleton<AnimationData2DList>
{
    [SerializeField] private AnimationData2D[] animationItems = null;
    public Dictionary<string, Sprite[]> OrientationsDict { get; private set; }

    private void OnEnable()
    {
        OrientationsDict = new Dictionary<string, Sprite[]>();
        PopulateDictionary();
    }

    public void PopulateDictionary()
    {
        if(animationItems != null)
        {
            for (int i = 0; i < animationItems.Length; i++)
            {
                OrientationsDict.Add(animationItems[i].TransitionStateName, animationItems[i].Orientations);
            }
        }
    }

    private void OnDisable()
    {
        OrientationsDict.Clear();
    }
}
