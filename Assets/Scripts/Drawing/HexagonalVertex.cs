using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonalVertex : Vertex
{
    public event VertexOccupied OnHexVertOccupied;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(m_tag) && IsOccupied == false)
        {
            Debug.Log("HexagonalVertex: vertex has been occupied");
            Icon = other.gameObject.GetComponent<Icon2D>();
            OnHexVertOccupied?.Invoke();
        }
    }

    
}
