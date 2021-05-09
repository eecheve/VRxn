using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour
{
    [SerializeField] private int index = 0;
    public int Index { get { return index; } private set { index = value; } }
}
