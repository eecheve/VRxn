using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopAxialManager : MonoBehaviour
{
    [SerializeField] private List<TopAxial> points = null;
    [SerializeField] private Color secondaryColor = Color.white;

    public List<TopAxial> OccupiedPoints { get; set; } = new List<TopAxial>();

    public delegate void TopAxialMilestone();
    public static event TopAxialMilestone OnAxialElementsPlaced;

    public List<TopAxial> Points { get { return points; } private set { points = value; } }

    public void StopHighlights()
    {
        foreach (var point in points)
        {
            point.SphereCollider.enabled = false;
            point.SpriteRenderer.enabled = false;
        }
    }

    public void CommunicateAxialPointsCompleted()
    {
        if (OnAxialElementsPlaced != null)
            OnAxialElementsPlaced();
    }

    public void ResetHighlighters()
    {
        OccupiedPoints.Clear();
        StopHighlights();
    }
}
