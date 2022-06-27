using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VertexManager : MonoBehaviour
{
    [SerializeField] private List<Vertex> vertices = null;
    [SerializeField] private Color secondaryColor = Color.white;
    [SerializeField] private bool isProduct = false;

    public List<Vertex> OccupiedVertices { get; set; } = new List<Vertex>();

    public delegate void ElementPlaced();

    public List<Vertex> Vertices { get { return vertices; } private set { vertices = value; } }
    public bool IsProduct { get { return isProduct; } private set { isProduct = value; } }

    public void ClearAllVertices()
    {
        ///<summary>
        ///Clears all vertices from icons and bonds. Turns off the highlighters sprite renderers as well
        /// </summary>
        foreach (var vertex in Vertices)
        {
            Debug.Log($"VertexManager, currently at {vertex.name}");
            if(vertex.transform.childCount > 0)
            {
                Debug.Log($"VertexManager, {vertex.name} has something to erase");
                foreach (Transform child in vertex.transform)
                {
                    Debug.Log($"VertexManager, trying to erase {child.name}");
                    Destroy(child.gameObject);
                }
            }
        }
    }
    
    protected int ReturnIconConnectedValue(ElementIcon icon1, ElementIcon icon2)
    {
        ///<summary>
        ///Checks if two icons are connected in any way
        ///returns: value between 0 (no bond) and 3 (triple bond)
        ///</summary>
        int value = 0;

        if (icon1 != null)
        {
            LineHolder[] lines1 = icon1.GetComponentsInChildren<LineHolder>();

            foreach (var line in lines1)
            {
                if (line.name.Contains(icon2.name))
                {
                    value++;
                    break;
                }
            }
        }

        if (icon2 != null)
        {
            LineHolder[] lines2 = icon2.GetComponentsInChildren<LineHolder>();

            foreach (var line in lines2)
            {
                if (line.name.Contains(icon1.name))
                {
                    value++;
                    break;
                }
            }
        }

        return value;
    }
}
