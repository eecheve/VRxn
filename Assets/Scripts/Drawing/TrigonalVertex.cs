using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigonalVertex : Vertex
{
    public event VertexOccupied OnTrigVertOccupied;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TrigonalVertex: vertex occupied has tag " + other.gameObject.tag);

        if (other.gameObject.CompareTag(m_tag) && IsOccupied == false)
        {
            Debug.Log("TrigonalVertex: vertex has been occupied");
            
            Icon = other.gameObject.GetComponent<Icon2D>();
            Icon.Vertex = this;
            Icon.VertexManager = vertexManager;
            IsOccupied = true;
         
            OnTrigVertOccupied?.Invoke();
        }
    }
}
