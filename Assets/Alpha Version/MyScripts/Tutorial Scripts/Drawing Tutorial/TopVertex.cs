using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopVertex : MonoBehaviour
{
    [SerializeField] private TopVertexManager vertexManager = null;
    
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
            vertexManager.OccupiedVertices.Add(this); //if it is, add this vertex to the list of occupied vertices
            if (vertexManager.OccupiedVertices.Count < 2) //check if there are four elements instantiated
            {
                AssignIconToVertexBottom(other);
            }
            else if(vertexManager.OccupiedVertices.Count == 2)
            {
                AssignIconToVertexBottom(other);
            }
            else
            {
                AssignIconToVertexBottom(other);
            }
        }
    }

    private void AssignIconToVertexBottom(Collider other)
    {
        Icon = other.gameObject.GetComponent<ElementIcon>(); //assigns to the instantiated icon its vertex.

        if (Icon != null)
        {
            Icon.TopVertex = this;
            Icon.ChangeTag();
            Icon.TopVertexManager = vertexManager;
        }

        IsOccupied = true; //tells everyone else this vertex is occupied
        SpriteRenderer.enabled = false; //stops highlighting the vertex
        SphereCollider.enabled = false;
    }
}
