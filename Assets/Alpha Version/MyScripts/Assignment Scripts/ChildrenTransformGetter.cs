using System;
using UnityEngine;

public class ChildrenTransformGetter : MonoBehaviour
{
    #region singleton
    private static ChildrenTransformGetter _instance;
    public static ChildrenTransformGetter Instance
    {
        get
        {
            if(_instance == null)
                Debug.LogError("ChildrenTransformGetter is not in the scene");
            
            return _instance;
        }
    }
    #endregion

    public GameObject objectToModify; //this object must have the same number of children
    public Transform[] ObjectTransforms { get; private set; }

    public void LoadObjectInformation()
    {
        _instance = this;
        ObjectTransforms = objectToModify.GetComponentsInChildren<Transform>();
    }
}
