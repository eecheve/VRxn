using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopOutsideManager : MonoSingleton<TopOutsideManager>
{
    [SerializeField] private List<TopOutside> points = new List<TopOutside>();
    [SerializeField] private Color secondaryColor = Color.white;

    public List<TopOutside> OccupiedVertices { get; set; } = new List<TopOutside>();

    private TopVertex terminal1;
    private TopOutside point1;
    private TopVertex vertex1;
    private TopAxial axial1;

    private TopVertex terminal2;
    private TopOutside point2;
    private TopVertex vertex2;
    private TopAxial axial2;

    private bool highlightedOutside1 = false;
    private bool highlightedOutside2 = false;
    private bool singleOutside1 = false;
    private bool singleOutside2 = false;

    private bool highlightedInside1 = false;
    private bool highlightedInside2 = false;
    private bool singleInside1 = false;
    private bool singleInside2 = false;

    private DrawingPointsManager pointsManager;

    public bool InsideCompleted { get; set; } = false;
    public bool OutsideCompleted { get; set; } = false;

    public delegate void DrawingTaskCompleted();
    public static event DrawingTaskCompleted OnDrawingTaskCompleted;

    private void Awake()
    {
        pointsManager = GetComponent<DrawingPointsManager>();
    }


    private void OnEnable()
    {
        BottomOutsideManager.OnAllBottomSubstituentsConnected += AssignOutsidePointsByCase;
        Draw.OnSingleBondCreated += CheckForBondConnections;
    }

    private void CheckForBondConnections()
    {
        if(pointsManager.BottomVertexManager.IsProduct == false)
        {

            ManageBonds1();

            if (highlightedInside1 == true && singleInside1 == false)
            {
                int value = ReturnIconConnectedValue(terminal1.Icon, vertex1.Icon);

                if (value > 0)
                {
                    singleInside1 = true;

                    StopHighlights();
                    HighlightInside2();
                }
            }

            if (highlightedInside2 == true && singleInside2 == false)
            {
                int value = ReturnIconConnectedValue(terminal2.Icon, vertex2.Icon);

                if (value > 0)
                {
                    singleInside2 = true;

                    //StopHighlights();

                    foreach (var vertex in pointsManager.BottomVertexManager.OccupiedVertices)
                    {
                        vertex.SphereCollider.enabled = false;
                        vertex.SpriteRenderer.enabled = false;
                    }

                    foreach (var vertex in pointsManager.TopVertexManager.OccupiedVertices)
                    {
                        vertex.SphereCollider.enabled = false;
                        vertex.SpriteRenderer.enabled = false;
                    }

                    foreach (var vertex in pointsManager.BottomOutsideManager.OccupiedVertices)
                    {
                        vertex.SphereCollider.enabled = false;
                        vertex.SpriteRenderer.enabled = false;
                    }

                    foreach (var vertex in pointsManager.TopOutsideManager.OccupiedVertices)
                    {
                        vertex.SphereCollider.enabled = false;
                        vertex.SpriteRenderer.enabled = false;
                    }

                    if (OnDrawingTaskCompleted != null)
                        OnDrawingTaskCompleted();
                }
            }
        }
        else
        {
            ManageBonds1();

            if (highlightedInside1 == true && singleInside1 == false)
            {
                int value = ReturnIconConnectedValue(terminal1.Icon, axial1.Icon);

                if (value > 0)
                {
                    singleInside1 = true;

                    StopHighlights();
                    HighlightInside2();
                }
            }

            if (highlightedInside2 == true && singleInside2 == false)
            {
                int value = ReturnIconConnectedValue(terminal2.Icon, axial2.Icon);

                if (value > 0)
                {
                    singleInside2 = true;

                    //StopHighlights();

                    foreach (var vertex in pointsManager.BottomVertexManager.OccupiedVertices)
                    {
                        vertex.SphereCollider.enabled = false;
                        vertex.SpriteRenderer.enabled = false;
                    }

                    foreach (var vertex in pointsManager.TopVertexManager.OccupiedVertices)
                    {
                        vertex.SphereCollider.enabled = false;
                        vertex.SpriteRenderer.enabled = false;
                    }

                    foreach (var vertex in pointsManager.BottomOutsideManager.OccupiedVertices)
                    {
                        vertex.SphereCollider.enabled = false;
                        vertex.SpriteRenderer.enabled = false;
                    }

                    foreach (var vertex in pointsManager.TopOutsideManager.OccupiedVertices)
                    {
                        vertex.SphereCollider.enabled = false;
                        vertex.SpriteRenderer.enabled = false;
                    }

                    foreach (var point in pointsManager.BottomAxialManager.OccupiedPoints)
                    {
                        point.SphereCollider.enabled = false;
                        point.SpriteRenderer.enabled = false;
                    }

                    foreach (var point in pointsManager.TopAxialManager.OccupiedPoints)
                    {
                        point.SphereCollider.enabled = false;
                        point.SpriteRenderer.enabled = false;
                    }

                    if (OnDrawingTaskCompleted != null)
                        OnDrawingTaskCompleted();
                }
            }
        }
    }

    private void ManageBonds1()
    {
        if (highlightedOutside1 == true && singleOutside1 == false)
        {
            int value = ReturnIconConnectedValue(terminal1.Icon, point1.Icon);

            if (value > 0)
            {
                singleOutside1 = true;
                HighlightConnection2();
            }
        }

        if (highlightedOutside2 == true && singleOutside2 == false)
        {
            int value = ReturnIconConnectedValue(terminal2.Icon, point2.Icon);

            if (value > 0)
            {
                singleOutside2 = true;

                StopHighlights();
                HighlightInside1();
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

    private void AssignOutsidePointsByCase()
    {
        if (pointsManager.BottomVertexManager.BuildCase == 1)
        {
            terminal1 = pointsManager.TopVertexManager.Vertices[1];
            point1 = points[1];
            vertex1 = pointsManager.TopVertexManager.Vertices[0];
            axial1 = pointsManager.TopAxialManager.Points[1];

            terminal2 = pointsManager.TopVertexManager.Vertices[2];
            point2 = points[2];
            vertex2 = pointsManager.TopVertexManager.Vertices[3];
            axial2 = pointsManager.TopAxialManager.Points[2];

            HighlightOutsidePoints();

        }
        else if (pointsManager.BottomVertexManager.BuildCase == 2)
        {
            terminal1 = pointsManager.TopVertexManager.Vertices[5];
            point1 = points[5];
            vertex1 = pointsManager.TopVertexManager.Vertices[0];
            axial1 = pointsManager.TopAxialManager.Points[5];

            terminal2 = pointsManager.TopVertexManager.Vertices[4];
            point2 = points[4];
            vertex2 = pointsManager.TopVertexManager.Vertices[3];
            axial2 = pointsManager.TopAxialManager.Points[4];

            HighlightOutsidePoints();
        }
        else if (pointsManager.BottomVertexManager.BuildCase == 3)
        {
            terminal1 = pointsManager.TopVertexManager.Vertices[2];
            point1 = points[2];
            vertex1 = pointsManager.TopVertexManager.Vertices[1];
            axial1 = pointsManager.TopAxialManager.Points[2];

            terminal2 = pointsManager.TopVertexManager.Vertices[3];
            point2 = points[3];
            vertex2 = pointsManager.TopVertexManager.Vertices[4];
            axial2 = pointsManager.TopAxialManager.Points[3];

            HighlightOutsidePoints();
        }
        else if (pointsManager.BottomVertexManager.BuildCase == 4)
        {
            terminal1 = pointsManager.TopVertexManager.Vertices[3];
            point1 = points[3];
            vertex1 = pointsManager.TopVertexManager.Vertices[2];
            axial1 = pointsManager.TopAxialManager.Points[3];

            terminal2 = pointsManager.TopVertexManager.Vertices[4];
            point2 = points[4];
            vertex2 = pointsManager.TopVertexManager.Vertices[5];
            axial2 = pointsManager.TopAxialManager.Points[4];

            HighlightOutsidePoints();
        }
        else
        {
            Debug.Log("BottomOutsideManager says: wrong combination, dude!");
        }
    }

    private void HighlightOutsidePoints()
    {
        point1.SpriteRenderer.enabled = true;
        point1.SphereCollider.enabled = true;
        point2.SpriteRenderer.enabled = true;
        point2.SphereCollider.enabled = true;
    }

    public void StopHighlights()
    {
        terminal1.SphereCollider.enabled = false;
        point1.SphereCollider.enabled = false;
        vertex1.SphereCollider.enabled = false;

        terminal2.SphereCollider.enabled = false;
        point2.SphereCollider.enabled = false;
        vertex2.enabled = false;

        terminal1.SpriteRenderer.enabled = false;
        point1.SpriteRenderer.enabled = false;
        vertex1.SpriteRenderer.enabled = false;

        terminal1.SpriteRenderer.color = terminal1.InitialColor;
        point1.SpriteRenderer.color = point1.InitialColor;
        vertex1.SpriteRenderer.color = vertex1.InitialColor;

        terminal2.SpriteRenderer.enabled = false;
        point2.SpriteRenderer.enabled = false;
        vertex2.SpriteRenderer.enabled = false;

        terminal2.SpriteRenderer.color = terminal2.InitialColor;
        point2.SpriteRenderer.color = point2.InitialColor;
        vertex2.SpriteRenderer.color = vertex2.InitialColor;
    }

    public void HighlightConnection1()
    {
        Debug.Log("BottomOutsideManager_HighlightConnection1()");

        if (highlightedOutside1 == false)
        {
            terminal1.SpriteRenderer.color = secondaryColor;
            terminal1.SpriteRenderer.enabled = true;
            terminal1.SphereCollider.enabled = true;

            point1.SpriteRenderer.color = secondaryColor;
            point1.SpriteRenderer.enabled = true;
            point1.SphereCollider.enabled = true;

            highlightedOutside1 = true;
        }
    }

    private void HighlightConnection2()
    {
        Debug.Log("BottomOutsideManager_HighlightConnection2()");

        terminal2.SpriteRenderer.color = secondaryColor;
        terminal2.SpriteRenderer.enabled = true;
        terminal2.SphereCollider.enabled = true;

        point2.SpriteRenderer.color = secondaryColor;
        point2.SpriteRenderer.enabled = true;
        point2.SphereCollider.enabled = true;

        highlightedOutside2 = true;
    }

    private void HighlightInside1()
    {
        Debug.Log("BottomOutsideManager_HighlightInside1()");

        if(pointsManager.BottomVertexManager.IsProduct == false)
        {
            terminal1.SpriteRenderer.enabled = true;
            terminal1.SphereCollider.enabled = true;

            vertex1.SpriteRenderer.color = secondaryColor;
            vertex1.SpriteRenderer.enabled = true;
            vertex1.SphereCollider.enabled = true;

            highlightedInside1 = true;
        }
        else
        {
            terminal1.SpriteRenderer.enabled = true;
            terminal1.SphereCollider.enabled = true;

            axial1.SpriteRenderer.color = secondaryColor;
            axial1.SpriteRenderer.enabled = true;
            axial1.SphereCollider.enabled = true;

            highlightedInside1 = true;
        }

    }

    private void HighlightInside2()
    {
        Debug.Log("BottomOutsideManager_HighlightInside2()");

        if(pointsManager.BottomVertexManager.IsProduct == false)
        {
            terminal2.SpriteRenderer.enabled = true;
            terminal2.SphereCollider.enabled = true;

            vertex2.SpriteRenderer.color = secondaryColor;
            vertex2.SpriteRenderer.enabled = true;
            vertex2.SphereCollider.enabled = true;

            highlightedInside2 = true;
        }
        else
        {
            terminal2.SpriteRenderer.enabled = true;
            terminal2.SphereCollider.enabled = true;

            axial2.SpriteRenderer.color = secondaryColor;
            axial2.SpriteRenderer.enabled = true;
            axial2.SphereCollider.enabled = true;

            highlightedInside2 = true;
        }
    }

    public void ResetHighlighters()
    {
        OccupiedVertices.Clear();

        terminal1.SpriteRenderer.color = terminal1.InitialColor;
        point1.SpriteRenderer.color = point1.InitialColor;
        vertex1.SpriteRenderer.color = vertex1.InitialColor;
        axial1.SpriteRenderer.color = axial1.InitialColor;

        terminal2.SpriteRenderer.color = terminal1.InitialColor;
        point2.SpriteRenderer.color = point1.InitialColor;
        vertex2.SpriteRenderer.color = vertex1.InitialColor;
        axial2.SpriteRenderer.color = axial2.InitialColor;

        terminal1.SphereCollider.enabled = false;
        terminal1.SpriteRenderer.enabled = false;
        terminal2.SphereCollider.enabled = false;
        terminal2.SpriteRenderer.enabled = false;
        
        vertex1.SpriteRenderer.enabled = false;
        vertex1.SphereCollider.enabled = false;
        vertex2.SpriteRenderer.enabled = false;
        vertex2.SphereCollider.enabled = false;

        point1.SpriteRenderer.enabled = false;
        point1.SphereCollider.enabled = false;
        point2.SphereCollider.enabled = false;
        point2.SpriteRenderer.enabled = false;

        axial1.SpriteRenderer.enabled = false;
        axial1.SphereCollider.enabled = false;
        axial2.SpriteRenderer.enabled = false;
        axial2.SphereCollider.enabled = false;

        terminal1 = null;
        point1 = null;
        vertex1 = null;
        axial1 = null;

        terminal2 = null;
        point2 = null;
        vertex2 = null;
        axial2 = null;

        highlightedOutside1 = false;
        highlightedOutside2 = false;

        singleOutside1 = false;
        singleOutside2 = false;

        highlightedInside1 = false;
        highlightedInside2 = false;
        singleInside1 = false;
        singleInside2 = false;

        InsideCompleted = false;
        OutsideCompleted = false;
    }

    private void OnDisable()
    {
        BottomOutsideManager.OnAllBottomSubstituentsConnected += AssignOutsidePointsByCase;
        Draw.OnSingleBondCreated -= CheckForBondConnections;
    }

}
