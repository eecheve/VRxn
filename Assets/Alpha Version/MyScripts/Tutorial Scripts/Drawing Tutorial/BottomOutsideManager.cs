using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomOutsideManager : MonoSingleton<BottomOutsideManager>
{
    [SerializeField] private List<BottomOutside> points = new List<BottomOutside>();
    [SerializeField] private Color secondaryColor = Color.white;

    public List<BottomOutside> OccupiedVertices { get; set; } = new List<BottomOutside>();
    public Color SecondaryColor { get { return secondaryColor; } set { secondaryColor = value; } }

    public delegate void BottomSubstituentsMilestone();
    public static event BottomSubstituentsMilestone OnOutsideElementsPlaced;
    public static event BottomSubstituentsMilestone OnInsideElementsPlaced;
    public static event BottomSubstituentsMilestone OnSingleBond1Connected;
    public static event BottomSubstituentsMilestone OnOutsideSubstituentsConnected;
    public static event BottomSubstituentsMilestone OnAllBottomSubstituentsConnected;

    private BottomVertex terminal1;
    private BottomOutside point1;
    private BottomVertex vertex1;
    private BottomAxial axial1;

    private BottomVertex terminal2;
    private BottomOutside point2;
    private BottomVertex vertex2;
    private BottomAxial axial2;

    private bool highlightedOutside1 = false;
    private bool highlightedOutside2 = false;
    private bool singleOutside1 = false;
    private bool singleOutside2 = false;

    private bool highlightedInside1 = false;
    private bool highlightedInside2 = false;
    private bool singleInside1 = false;
    private bool singleInside2 = false;

    private DrawingPointsManager pointsManager;

    private void Awake()
    {
        pointsManager = GetComponent<DrawingPointsManager>();
    }

    private void OnEnable()
    {
        OppositeVerticesConnector.OnBackboneCompleted += AssignOutsidePointsByCase;
        Draw.OnSingleBondCreated += CheckForBondConnections;
    }

    private void AssignOutsidePointsByCase()
    {
        Debug.Log("BottomOutsideManager_AssignOutsidePointsByCase");
        
        if (pointsManager.BottomVertexManager.BuildCase == 1)
        {
            terminal1 = pointsManager.BottomVertexManager.Vertices[0];
            point1 = points[0];
            vertex1 = pointsManager.BottomVertexManager.Vertices[1];
            axial1 = pointsManager.BottomAxialManager.Points[0];

            terminal2 = pointsManager.BottomVertexManager.Vertices[3];
            point2 = points[3];
            vertex2 = pointsManager.BottomVertexManager.Vertices[2];
            axial2 = pointsManager.BottomAxialManager.Points[3];

            HighlightOutsidePoints();

        }
        else if (pointsManager.BottomVertexManager.BuildCase == 2)
        {
            terminal1 = pointsManager.BottomVertexManager.Vertices[0];
            point1 = points[0];
            vertex1 = pointsManager.BottomVertexManager.Vertices[5];
            axial1 = pointsManager.BottomAxialManager.Points[0];

            terminal2 = pointsManager.BottomVertexManager.Vertices[3];
            point2 = points[3];
            vertex2 = pointsManager.BottomVertexManager.Vertices[4];
            axial2 = pointsManager.BottomAxialManager.Points[3];

            HighlightOutsidePoints();
        }
        else if (pointsManager.BottomVertexManager.BuildCase == 3)
        {
            terminal1 = pointsManager.BottomVertexManager.Vertices[1];
            point1 = points[1];
            vertex1 = pointsManager.BottomVertexManager.Vertices[2];
            axial1 = pointsManager.BottomAxialManager.Points[1];

            terminal2 = pointsManager.BottomVertexManager.Vertices[4];
            point2 = points[4];
            vertex2 = pointsManager.BottomVertexManager.Vertices[3];
            axial2 = pointsManager.BottomAxialManager.Points[4];

            HighlightOutsidePoints();
        }
        else if (pointsManager.BottomVertexManager.BuildCase == 4)
        {
            terminal1 = pointsManager.BottomVertexManager.Vertices[2];
            point1 = points[2];
            vertex1 = pointsManager.BottomVertexManager.Vertices[3];
            axial1 = pointsManager.BottomAxialManager.Points[2];

            terminal2 = pointsManager.BottomVertexManager.Vertices[5];
            point2 = points[5];
            vertex2 = pointsManager.BottomVertexManager.Vertices[4];
            axial2 = pointsManager.BottomAxialManager.Points[5];

            HighlightOutsidePoints();
        }
        else
        {
            Debug.Log("BottomOutsideManager says: wrong combination, dude!");
        }
    }

    private void HighlightOutsidePoints()
    {
        Debug.Log("BottomOutsideManager - HighlightOutsidePoints");

        point1.SpriteRenderer.enabled = true;
        point1.SphereCollider.enabled = true;
        point2.SpriteRenderer.enabled = true;
        point2.SphereCollider.enabled = true;
    }

    private void CheckForBondConnections()
    {
        Debug.Log("BottomOutsideManager - CheckForBondConnections");
        

        if (pointsManager.BottomVertexManager.IsProduct == false)
        {
            ManageBottomPoints1();

            if (highlightedInside1 == true && singleInside1 == false)
            {
                int value = ReturnIconConnectedValue(terminal1.Icon, vertex1.Icon);

                if (value > 0)
                {
                    singleInside1 = true;

                    StopHighlights();
                    HighlightInside2();

                    Debug.Log("BottomOutsideManager - CheckForBondConnections: highlightInside1 = true");
                }
            }

            else if (highlightedInside2 == true && singleInside2 == false)
            {
                int value = ReturnIconConnectedValue(terminal2.Icon, vertex2.Icon);

                if (value > 0)
                {
                    singleInside2 = true;

                    StopHighlights();

                    if (OnAllBottomSubstituentsConnected != null)
                        OnAllBottomSubstituentsConnected();

                    Debug.Log("BottomOutsideManager - CheckForBondConnections: highlightInside2 = true");
                }
            }
        }
        else
        {
            ManageBottomPoints1();
            Debug.Log("BottomOutsideManager - CheckForBondConnections: THIS IS THE PRODUCT");

            if (highlightedInside1 == true && singleInside1 == false)
            {
                int value = ReturnIconConnectedValue(terminal1.Icon, axial1.Icon);

                if (value > 0)
                {
                    singleInside1 = true;

                    StopHighlights();
                    HighlightInside2();

                    Debug.Log("BottomOutsideManager - CheckForBondConnections: highlightInside1 = true");
                }
            }

            else if (highlightedInside2 == true && singleInside2 == false)
            {
                int value = ReturnIconConnectedValue(terminal2.Icon, axial2.Icon);

                if (value > 0)
                {
                    singleInside2 = true;

                    StopHighlights();

                    if (OnAllBottomSubstituentsConnected != null)
                        OnAllBottomSubstituentsConnected();

                    Debug.Log("BottomOutsideManager - CheckForBondConnections: highlightInside2 = true");
                }
            }
        }
    }

    private void ManageBottomPoints1()
    {
        if (highlightedOutside1 == true && singleOutside1 == false)
        {
            int value = ReturnIconConnectedValue(terminal1.Icon, point1.Icon);

            if (value > 0)
            {
                singleOutside1 = true;
                StopHighlights();
                HighlightConnection2();

                if (OnSingleBond1Connected != null)
                    OnSingleBond1Connected();

                Debug.Log("BottomOutsideManager - CheckForBondConnections: highlightOutside1 = true");
            }
        }

        else if (highlightedOutside2 == true && singleOutside2 == false)
        {
            int value = ReturnIconConnectedValue(terminal2.Icon, point2.Icon);

            if (value > 0)
            {
                singleOutside2 = true;

                StopHighlights();
                HighlightInside1();

                Debug.Log("BottomOutsideManager - CheckForBondConnections: highlightOutside2 = true");

                if (OnOutsideSubstituentsConnected != null)
                    OnOutsideSubstituentsConnected();
            }
        }
    }

    public void CheckForElementsPlaced()
    {
        Debug.Log("BottomOutsideManager - CheckForElementsPlaced");

        if (OccupiedVertices.Count == 2)
        {
            Debug.Log("BottomOutsideManager_CheckForElementsPlaced: count is 2");
            
            if (OnOutsideElementsPlaced != null)
                OnOutsideElementsPlaced();
        }

        else if(OccupiedVertices.Count == 4)
        {
            Debug.Log("BottomOutsideManager_CheckForElementsPlaced: count is 4");

            if (OnInsideElementsPlaced != null)
                OnInsideElementsPlaced();
        }
    }

    private int ReturnIconConnectedValue(ElementIcon icon1, ElementIcon icon2)
    {
        ///<summary>
        ///Checks if two icons are connected in any way
        ///</summary>
        Debug.Log("BottomOutsideManager - ReturnIconConnectedValue");

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

    public void HighlightConnection1()
    {
        Debug.Log("BottomOutsideManager_HighlightConnection1()");
        
        if(highlightedOutside1 == false)
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

        if(highlightedOutside2 == false)
        {
            terminal2.SpriteRenderer.color = secondaryColor;
            terminal2.SpriteRenderer.enabled = true;
            terminal2.SphereCollider.enabled = true;

            point2.SpriteRenderer.color = secondaryColor;
            point2.SpriteRenderer.enabled = true;
            point2.SphereCollider.enabled = true;

            highlightedOutside2 = true;
        }
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

    public void StopHighlights()
    {
        Debug.Log("BottomOutsideManager - StopHighlights");

        terminal1.SphereCollider.enabled = false;
        point1.SphereCollider.enabled = false;
        vertex1.SphereCollider.enabled = false;
        axial1.SphereCollider.enabled = false;

        terminal2.SphereCollider.enabled = false;
        point2.SphereCollider.enabled = false;
        vertex2.SphereCollider.enabled = false;
        axial2.SphereCollider.enabled = false;

        terminal1.SpriteRenderer.enabled = false;
        point1.SpriteRenderer.enabled = false;
        vertex1.SpriteRenderer.enabled = false;
        axial1.SpriteRenderer.enabled = false;

        terminal1.SpriteRenderer.color = terminal1.InitialColor;
        point1.SpriteRenderer.color = point1.InitialColor;
        vertex1.SpriteRenderer.color = vertex1.InitialColor;
        axial1.SpriteRenderer.color = axial1.InitialColor;

        terminal2.SpriteRenderer.enabled = false;
        point2.SpriteRenderer.enabled = false;
        vertex2.SpriteRenderer.enabled = false;
        axial2.SpriteRenderer.enabled = false;

        terminal2.SpriteRenderer.color = terminal2.InitialColor;
        point2.SpriteRenderer.color = point2.InitialColor;
        vertex2.SpriteRenderer.color = vertex2.InitialColor;
        axial2.SpriteRenderer.color = axial2.InitialColor;
    }

    public void ResetHighlights()
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

        if (terminal1.IsInitialVertex == false)
        {
            terminal1.SphereCollider.enabled = false;
            terminal1.SpriteRenderer.enabled = false;
        }
        if(terminal2.IsInitialVertex == false)
        {
            terminal2.SphereCollider.enabled = false;
            terminal2.SpriteRenderer.enabled = false;
        }
        if(vertex1.IsInitialVertex == false)
        {
            vertex1.SpriteRenderer.enabled = false;
            vertex1.SphereCollider.enabled = false;
        }
        if (vertex2.IsInitialVertex == false)
        {
            vertex2.SpriteRenderer.enabled = false;
            vertex2.SphereCollider.enabled = false;
        }

        point1.SpriteRenderer.enabled = false;
        point1.SphereCollider.enabled = false;

        point2.SphereCollider.enabled = false;
        point2.SpriteRenderer.enabled = false;

        axial1.SphereCollider.enabled = false;
        axial1.SpriteRenderer.enabled = false;

        axial2.SphereCollider.enabled = false;
        axial2.SpriteRenderer.enabled = false;

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
    }

    private void OnDisable()
    {
        OppositeVerticesConnector.OnBackboneCompleted -= AssignOutsidePointsByCase;
        Draw.OnSingleBondCreated -= CheckForBondConnections;
    }
}
