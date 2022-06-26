using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Page : MonoBehaviour
{
    [SerializeField] private int index = 0;
    public int Index { get { return index; } private set { index = value; } }

    public UnityEvent onPageDisabled;

    private void OnDisable()
    {
        onPageDisabled?.Invoke();
    }
}
