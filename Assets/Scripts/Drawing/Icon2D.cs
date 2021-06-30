using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon2D : MonoBehaviour
{
    public VertexManager VertexManager { get; set; } = null;
    public Vertex Vertex { get; set; } = null;

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
            VertexManager.OccupiedVertices.Remove(Vertex);
            Vertex.Icon = null;
        }
    }
}
