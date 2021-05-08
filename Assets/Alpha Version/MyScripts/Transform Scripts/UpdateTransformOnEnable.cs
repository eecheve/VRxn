using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTransformOnEnable : MonoBehaviour
{
    [SerializeField] private Transform toFollow = null;
    
    private void OnEnable()
    {
        Debug.Log("UpdateTransformOnEnable: new pos is (" + 
            toFollow.position.x.ToString() + "," +
            toFollow.position.y.ToString() + "," +
            toFollow.position.z.ToString() + ")");

        transform.position = toFollow.position;
        transform.rotation = toFollow.rotation;
    }
}
