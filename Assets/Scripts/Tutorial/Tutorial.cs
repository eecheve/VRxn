using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tutorial : MonoBehaviour
{
    [SerializeField] private int order = 0;
    //[TextArea] [SerializeField] private string explanation = "";

    public int Order { get { return order; } set { order = value; } }
    //public string Explanation { get { return explanation; } set { explanation = value; } }

    public delegate void TutorialCompleted();
    
    //private void Awake()
    //{
    //    Debug.Log("Tutorial: Adding the tutorials to the list");
    //    TutorialManager.Instance.Tutorials.Add(this);
    //}

    public virtual void CheckIfHappening() { }
}
