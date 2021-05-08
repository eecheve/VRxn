using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopAxial : MonoBehaviour
{
    [SerializeField] private TopAxialManager pointsManager = null;

    public bool IsOccupied { get; set; }
    public SpriteRenderer SpriteRenderer { get; set; }
    public SphereCollider SphereCollider { get; set; }
    public ElementIcon Icon { get; set; } = null;
    public Color InitialColor { get; private set; }


    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        SphereCollider = GetComponent<SphereCollider>();

        InitialColor = SpriteRenderer.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ink") && IsOccupied == false) //checking if vertex is empty and if instantiated object is "ink"
        {
            pointsManager.OccupiedPoints.Add(this); //if it is, add this vertex to the list of occupied vertices
            AssignIconToVertexBottom(other);


            if (pointsManager.OccupiedPoints.Count == 2)
            {
                pointsManager.StopHighlights();
                pointsManager.CommunicateAxialPointsCompleted();
            }
        }
    }

    private void AssignIconToVertexBottom(Collider other)
    {
        Icon = other.gameObject.GetComponent<ElementIcon>(); //assigns to the instantiated icon its vertex.

        if (Icon != null)
        {
            Icon.ChangeTag();
            Icon.TopAxial = this;
            Icon.TopAxialManager = pointsManager;
        }

        IsOccupied = true; //tells everyone else this vertex is occupied
        SpriteRenderer.enabled = false; //stops highlighting the vertex
        SphereCollider.enabled = false;
    }
}
