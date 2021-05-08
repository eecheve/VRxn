using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopVertexManager : MonoBehaviour
{
    [SerializeField] private List<TopVertex> vertices = null;
    [SerializeField] private Color secondaryColor = Color.white;

    public List<TopVertex> OccupiedVertices { get; set; } = new List<TopVertex>();

    public delegate void DienophileMilestone();
    public static event DienophileMilestone OnElementsPlaced;
    public static event DienophileMilestone OnSingleBondMade;
    public static event DienophileMilestone OnDoubleBondMade;

    public List<TopVertex> Vertices { get { return vertices; } private set { vertices = value; } }
    public Color SecondaryColor { get { return secondaryColor; } private set { secondaryColor = value; } }

    private DrawingPointsManager pointsManager;
    
    private TopVertex vertex1;
    private TopVertex vertex2;

    private bool highlighted = false;
    private bool singleBond = false;
    private bool highlighted2 = false;
    private bool doubleBond = false;

    private void OnEnable()
    {
        pointsManager = GetComponent<DrawingPointsManager>();
        
        BottomVertexManager.OnDieneCompleted += AssignDienophileByCase;

        Draw.OnSingleBondCreated += CheckForSingleBonds;
        Draw.OnDoubleBondCreated += CheckForDoubleBonds;

        Draw.OnElementCreated += CheckForElementsAdded;
    }

    private void CheckForElementsAdded()
    {
        if(OccupiedVertices.Count == 1)
        {
            if (OnElementsPlaced != null)
                OnElementsPlaced(); 
        }
    }

    private void CheckForSingleBonds()
    {
        if(highlighted == true && singleBond == false)
        {
            int value = ReturnIconConnectedValue(vertex1.Icon, vertex2.Icon);

            if (value > 0)
            {
                singleBond = true;
                StopHighlights();
                HighlightVertices2();

                if (OnSingleBondMade != null)
                    OnSingleBondMade();
            }
        }
    }



    private void CheckForDoubleBonds()
    {
        if(highlighted2 == true && doubleBond == false)
        {
            int value = ReturnIconConnectedValue(vertex1.Icon, vertex2.Icon, "=");

            if(value > 0)
            {
                doubleBond = true;
                StopHighlights();

                if (OnDoubleBondMade != null)
                    OnDoubleBondMade();
            }
        }
    }

    private int ReturnIconConnectedValue(ElementIcon icon1, ElementIcon icon2)
    {
        ///<summary>
        ///Checks if two icons are connected in any way
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

    private int ReturnIconConnectedValue(ElementIcon icon1, ElementIcon icon2, string bondType)
    {
        ///<summary>
        ///Checks if two icons are connected in a specific way
        ///</summary>
        int value = 0;

        if (icon1 != null)
        {
            LineHolder[] lines1 = icon1.GetComponentsInChildren<LineHolder>();

            foreach (var line in lines1)
            {
                if (line.name.Contains(icon2.name) && line.name.Contains(bondType))
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
                if (line.name.Contains(icon1.name) && line.name.Contains(bondType))
                {
                    value++;
                    break;
                }
            }
        }

        return value;
    }

    private void AssignDienophileByCase()
    {
        if(highlighted == false)
        {        
            if (pointsManager.BottomVertexManager.BuildCase == 1) //BottomVertexManager.Instance.BuildCase == 1)
            {
                vertex1 = vertices[1];
                vertex2 = vertices[2];

                HighlightVertices();
            }
            else if (pointsManager.BottomVertexManager.BuildCase == 2)
            {
                vertex1 = vertices[4];
                vertex2 = vertices[5];

                HighlightVertices();
            }
            else if (pointsManager.BottomVertexManager.BuildCase == 3)
            {
                vertex1 = vertices[2];
                vertex2 = vertices[3];

                HighlightVertices();
            }
            else if (pointsManager.BottomVertexManager.BuildCase == 4)
            {
                vertex1 = vertices[3];
                vertex2 = vertices[4];

                HighlightVertices();
            }
            else
            {
                Debug.Log("TopVertexManager says: Wrong build case dude.");
            }
        }
    }

    public void HighlightVertices()
    {
        vertex1.SpriteRenderer.enabled = true;
        vertex1.SphereCollider.enabled = true;

        vertex2.SpriteRenderer.enabled = true;
        vertex2.SphereCollider.enabled = true;

        highlighted = true;
    }

    private void HighlightVertices2()
    {
        vertex1.SpriteRenderer.color = secondaryColor;
        vertex1.SpriteRenderer.enabled = true;
        vertex1.SphereCollider.enabled = true;

        vertex2.SpriteRenderer.color = secondaryColor;
        vertex2.SpriteRenderer.enabled = true;
        vertex2.SphereCollider.enabled = true;

        highlighted2 = true;
    }

    public void StopHighlights()
    {
        foreach (var vertex in vertices)
        {
            vertex.SphereCollider.enabled = false;
            vertex.SpriteRenderer.enabled = false;
        }
    }

    public void ResetHighlighters()
    {
        foreach (var vertex in vertices)
        {
            vertex.SpriteRenderer.color = vertex.InitialColor;
            vertex.SpriteRenderer.enabled = false;
            vertex.SphereCollider.enabled = false;
            vertex.IsOccupied = false;
        }

        OccupiedVertices.Clear();

        vertex1 = null;
        vertex2 = null;

        highlighted = false;
        highlighted2 = false;
        singleBond = false;
        doubleBond = false;
    }

    private void OnDisable()
    {
        BottomVertexManager.OnDieneCompleted += AssignDienophileByCase;

        Draw.OnElementCreated -= CheckForElementsAdded;

        Draw.OnSingleBondCreated -= CheckForSingleBonds;
        Draw.OnDoubleBondCreated -= CheckForDoubleBonds;
    }
}
