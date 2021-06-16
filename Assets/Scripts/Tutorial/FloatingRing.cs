using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FloatingRing : MonoBehaviour
{
    public UnityEvent OnObserved;
    
    public void Observed()
    {
        Debug.Log("FloatingRing: Observed is called");
        OnObserved?.Invoke();
    }
}
