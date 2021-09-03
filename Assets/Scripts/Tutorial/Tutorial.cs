using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tutorial : MonoBehaviour
{
    [SerializeField] private int order = 0;

    public int Order { get { return order; } set { order = value; } }
    public delegate void TutorialCompleted();
    
    public virtual void CheckIfHappening() { }

    public void SetOrder(int value)
    {
        order = value;
    }
}
