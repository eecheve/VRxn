using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomAxialManager : MonoBehaviour
{
    [SerializeField] private List<BottomAxial> points = null;
    [SerializeField] private Color secondaryColor = Color.white;

    public List<BottomAxial> OccupiedPoints { get; set; } = new List<BottomAxial>();

    public delegate void BottomAxialMilestone();
    public static event BottomAxialMilestone OnAxialElementsPlaced;

    public List<BottomAxial> Points { get { return points; } private set { points = value; } }

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
