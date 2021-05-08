using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ChartData : ScriptableObject
{
    [SerializeField] private List<Vector2> points = new List<Vector2>();

    public List<Vector2> Points { get { return points; } private set { points = value; } }

    public void SetPoints(List<Vector2> list)
    {
        points = list;
    }
}
