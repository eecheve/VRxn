using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon2D : MonoBehaviour
{
    public VertexManager VertexManager { get; set; } = null;
    public Vertex Vertex { get; set; } = null;

    public delegate void DeletedAsset(Vertex vertex);
    public static event DeletedAsset OnElementDeleted;

    public void ChangeTag()
    {
        gameObject.tag = "DryInk";
    }

    private void OnDestroy()
    {
        if(Vertex != null)
        {
            Debug.Log(name + "Icon2D: removing references to destroyed object");
            Vertex.IsOccupied = false;
            Vertex.GetComponent<SpriteRenderer>().enabled = true;
            Vertex.GetComponent<BoxCollider>().enabled = true;
            VertexManager.OccupiedVertices.Remove(Vertex);
            Vertex.Icon = null;

            OnElementDeleted?.Invoke(Vertex);
        }
    }
}
