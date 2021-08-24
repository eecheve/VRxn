using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonalVertex : Vertex
{
    public event VertexOccupied OnHexVertOccupied;
    public event WhichVertexOccupied OnWichVertOccupied;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(m_tag) && IsOccupied == false)
        {
            Debug.Log("HexagonalVertex: vertex has been occupied");
            //GetComponent<SpriteRenderer>().enabled = false;
            //Icon.Vertex.GetComponent<BoxCollider>().enabled = false;
            Icon = other.gameObject.GetComponent<Icon2D>();
            Icon.Vertex = this;
            Icon.VertexManager = vertexManager;
            IsOccupied = true;

            OnHexVertOccupied?.Invoke();
            OnWichVertOccupied?.Invoke(name);
        }
    }

    
}
