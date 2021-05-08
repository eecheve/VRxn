using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomVertex : MonoBehaviour
{
    [Header("Vertices information")]
    [SerializeField] private BottomVertex leftVertex = null;
    [SerializeField] private BottomVertex rightVertex = null;

    [Header("Vertex properties")]
    [SerializeField] private bool isInitialVertex = false;
    [SerializeField] private BottomVertexManager vertexManager = null;

    public bool IsOccupied { get; set; }
    public SpriteRenderer SpriteRenderer { get; set; }
    public SphereCollider SphereCollider { get; set; }
    public ElementIcon Icon { get; set; } = null;
    public Color InitialColor { get; private set; }

    public bool IsInitialVertex { get { return isInitialVertex; } private set { isInitialVertex = value; } }

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
            
            if (vertexManager.OccupiedVertices.Count < 4) //check if there are four elements instantiated
            {
                vertexManager.CheckForElementsPlaced();
                AssignIconToVertexBottom(other);
                HighlightNeighborVertices();
            }
            else if(vertexManager.OccupiedVertices.Count == 4)
            {
                vertexManager.CheckForElementsPlaced();
                AssignIconToVertexBottom(other);
                vertexManager.DieneElementsPlaced = true;
                vertexManager.StopHighlights();
                vertexManager.AssignBottomVerticesByCase();
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
            Icon.BottomVertex = this;
            Icon.ChangeTag();
            Icon.BottomVertexManager = vertexManager;
        }

        IsOccupied = true; //tells everyone else this vertex is occupied
        SpriteRenderer.enabled = false; //stops highlighting the vertex
        SphereCollider.enabled = false;
    }

    private void HighlightNeighborVertices()
    {
        if (leftVertex.IsOccupied == false) //makes sure to highlight neighbor vertices if they're not
        {
            leftVertex.SpriteRenderer.enabled = true;
            leftVertex.SphereCollider.enabled = true;
        }

        if (rightVertex.IsOccupied == false)
        {
            rightVertex.SpriteRenderer.enabled = true;
            rightVertex.SphereCollider.enabled = true;
        }
    }
}
