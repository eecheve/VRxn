using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomVertexManager : MonoBehaviour
{
    [SerializeField] private List<BottomVertex> vertices = null;
    [SerializeField] private Color secondaryColor = Color.white;
    [SerializeField] private bool isProduct = false;

    public List<BottomVertex> OccupiedVertices { get; set; } = new List<BottomVertex>();

    public bool DieneElementsPlaced { get; set; } = false;
    public int BuildCase { get; private set; } = 0;

    public delegate void DieneMilestone();
    public static event DieneMilestone OnFirstElementPlaced;
    public static event DieneMilestone OnAllBottomElementsPlaced;
    public static event DieneMilestone OnFirstSingleBondMade;
    public static event DieneMilestone OnLastSingleBondMade;
    public static event DieneMilestone OnFirstDoubleBondMade;
    public static event DieneMilestone OnDieneCompleted;
    public static event DieneMilestone OnVertexSubstituentsOccupied;

    public Color SecondaryColor { get { return secondaryColor; } private set { secondaryColor = value; } }
    public List<BottomVertex> Vertices { get { return vertices; } private set { vertices = value; } }
    public bool IsProduct { get { return isProduct; } private set { isProduct = value; } }

    private BottomVertex terminal1;
    private BottomVertex middle1;
    private BottomVertex terminal2;
    private BottomVertex middle2;

    private bool highlightedSingle1 = false; //between terminal1 & middle1
    private bool single1 = false; //between terminal1 & middle1

    private bool highlightedSingle2 = false; //between middle1 & middle2
    private bool single2 = false; //between middle1 & middle2

    private bool highlightedSingle3 = false; //between middle2 & terminal2
    private bool single3 = false; //between middle2 & terminal2

    private bool highlightedDouble1 = false; //between terminal1 & middle1
    private bool double1 = false;

    private bool highlightedDouble2 = false; //between terminal2 & middle2
    private bool double2 = false;

    private void OnEnable()
    {
        Draw.OnSingleBondCreated += CheckForSingleBonds;
        Draw.OnDoubleBondCreated += CheckForDoubleBonds;
    }

    public void CheckForElementsPlaced()
    {
        if(OccupiedVertices.Count == 1)
        {
            Debug.Log("BottomVertexManager_CheckForElementsPlaced(): first element detected");
            
            if (OnFirstElementPlaced != null)
                OnFirstElementPlaced();
        }
        else if(OccupiedVertices.Count == 4)
        {
            Debug.Log("BottomVertexManager_CheckForElementsPlaced(): fourth element detected");

            if (OnAllBottomElementsPlaced != null)
                OnAllBottomElementsPlaced();
        }
        else if(OccupiedVertices.Count == 6)
        {
            if (OnVertexSubstituentsOccupied != null)
                OnVertexSubstituentsOccupied();
        }
    }

    private void Start()
    {
        ResetHighlighters();
    }

    public void StopHighlights()
    {
        foreach (var vertex in vertices)
        {
            vertex.SphereCollider.enabled = false;
            vertex.SpriteRenderer.enabled = false;
        }
    }

    private void CheckForSingleBonds()
    {
        if(highlightedSingle1 == true && single1 == false)
        {
            int value = ReturnIconConnectedValue(terminal1.Icon, middle1.Icon);

            if (value > 0)
            {
                single1 = true;
                StopHighlights();
                HighlightSingle2();

                if (OnFirstSingleBondMade != null)
                    OnFirstSingleBondMade();
            }
        }

        if(highlightedSingle2 == true && single2 == false)
        {
            int value = ReturnIconConnectedValue(middle1.Icon, middle2.Icon);

            if (value > 0)
            {
                single2 = true;
                StopHighlights();
                HighlightSingle3();
            }
        }

        if(highlightedSingle3 == true && single3 == false)
        {
            int value = ReturnIconConnectedValue(terminal2.Icon, middle2.Icon);

            if (value > 0)
            {
                single3 = true;
                StopHighlights();
                HighlightDouble1();

                if (OnLastSingleBondMade != null)
                    OnLastSingleBondMade();
            }
        }
    }

    private void CheckForDoubleBonds()
    {
        if (highlightedDouble1 == true && double1 == false)
        {
            int value = ReturnIconConnectedValue(terminal1.Icon, middle1.Icon, "=");

            if (value > 0)
            {
                double1 = true;
                StopHighlights();
                HighlightDouble2();

                if (OnFirstDoubleBondMade != null)
                    OnFirstDoubleBondMade();
            }
        }

        if (highlightedDouble2 == true && double2 == false)
        {
            int value = ReturnIconConnectedValue(terminal2.Icon, middle2.Icon, "=");

            if (value > 0)
            {
                double2 = true;
                StopHighlights();

                if (OnDieneCompleted != null)
                    OnDieneCompleted();
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

    public void HighlightSingle1()
    {
        terminal1.SpriteRenderer.color = secondaryColor;
        terminal1.SpriteRenderer.enabled = true;
        terminal1.SphereCollider.enabled = true;

        middle1.SpriteRenderer.color = secondaryColor;
        middle1.SpriteRenderer.enabled = true;
        middle1.SphereCollider.enabled = true;

        highlightedSingle1 = true;
    }

    private void HighlightSingle2()
    {
        middle1.SpriteRenderer.color = secondaryColor;
        middle1.SpriteRenderer.enabled = true;
        middle1.SphereCollider.enabled = true;

        middle2.SpriteRenderer.color = secondaryColor;
        middle2.SpriteRenderer.enabled = true;
        middle2.SphereCollider.enabled = true;

        highlightedSingle2 = true;
    }

    private void HighlightSingle3()
    {
        terminal2.SpriteRenderer.color = secondaryColor;
        terminal2.SpriteRenderer.enabled = true;
        terminal2.SphereCollider.enabled = true;

        middle2.SpriteRenderer.color = secondaryColor;
        middle2.SpriteRenderer.enabled = true;
        middle2.SphereCollider.enabled = true;

        highlightedSingle3 = true;
    }

    private void HighlightDouble1()
    {
        terminal1.SpriteRenderer.color = terminal1.InitialColor;
        terminal1.SpriteRenderer.enabled = true;
        terminal1.SphereCollider.enabled = true;

        middle1.SpriteRenderer.color = middle1.InitialColor;
        middle1.SpriteRenderer.enabled = true;
        middle1.SphereCollider.enabled = true;

        highlightedDouble1 = true;
    }

    private void HighlightDouble2()
    {
        terminal2.SpriteRenderer.color = terminal2.InitialColor;
        terminal2.SpriteRenderer.enabled = true;
        terminal2.SphereCollider.enabled = true;

        middle2.SpriteRenderer.color = middle2.InitialColor;
        middle2.SpriteRenderer.enabled = true;
        middle2.SphereCollider.enabled = true;

        highlightedDouble2 = true;
    }

    public void AssignBottomVerticesByCase()
    {
        BuildCase = CheckDieneCase();

        if (BuildCase == 1)
        {
            terminal1 = vertices[0];
            middle1 = vertices[5];
            middle2 = vertices[4];
            terminal2 = vertices[3];

            HighlightSingle1();
        }
        else if (BuildCase == 2)
        {
            terminal1 = vertices[0];
            middle1 = vertices[1];
            middle2 = vertices[2];
            terminal2 = vertices[3];

            HighlightSingle1();
        }
        else if (BuildCase == 3)
        {
            terminal1 = vertices[1];
            middle1 = vertices[0];
            middle2 = vertices[5];
            terminal2 = vertices[4];

            HighlightSingle1();
        }
        else if (BuildCase == 4)
        {
            terminal1 = vertices[2];
            middle1 = vertices[1];
            middle2 = vertices[0];
            terminal2 = vertices[5];

            HighlightSingle1();
        }
        else
        {
            Debug.Log("VertexManager_HighlightTopVertices: wrong combination, dude!");
        }
    }

    private int CheckDieneCase()
    {
        /// Returns the case number (between 1 and 4), depending on which vertices are occupied
        /// If the 4 occupied vertices are not neighbors, it will return 0

        if (vertices[0].IsOccupied && vertices[5].IsOccupied
            && vertices[4].IsOccupied && vertices[3].IsOccupied)
        {
            return 1;
        }
        else if (vertices[0].IsOccupied && vertices[1].IsOccupied
            && vertices[2].IsOccupied && vertices[3].IsOccupied)
        {
            return 2;
        }
        else if (vertices[0].IsOccupied && vertices[1].IsOccupied
            && vertices[5].IsOccupied && vertices[4].IsOccupied)
        {
            return 3;
        }
        else if (vertices[0].IsOccupied && vertices[1].IsOccupied
            && vertices[2].IsOccupied && vertices[5].IsOccupied)
        {
            return 4;
        }
        else
        {
            return 0;
        }
    }

    public void ResetHighlighters()
    {
        foreach (var vertex in vertices)
        {
            if (vertex.IsInitialVertex == false)
            {
                vertex.SpriteRenderer.color = vertex.InitialColor;
                vertex.SpriteRenderer.enabled = false;
                vertex.SphereCollider.enabled = false;
                vertex.IsOccupied = false;
            }
            else
            {
                vertex.SpriteRenderer.color = vertex.InitialColor;
                vertex.SpriteRenderer.enabled = true;
                vertex.SphereCollider.enabled = true;
                vertex.IsOccupied = false;
            }
            OccupiedVertices.Clear();
        }
       
        BuildCase = 0;

        DieneElementsPlaced = false;

        terminal1 = null;
        terminal2 = null;
        middle1 = null;
        middle2 = null;

        highlightedSingle1 = false; //between terminal1 & middle1
        single1 = false; //between terminal1 & middle1

        highlightedSingle2 = false; //between middle1 & middle2
        single2 = false; //between middle1 & middle2

        highlightedSingle3 = false; //between middle2 & terminal2
        single3 = false; //between middle2 & terminal2

        highlightedDouble1 = false; //between terminal1 & middle1
        double1 = false;

        highlightedDouble2 = false; //between terminal2 & middle2
        double2 = false;
}

    private void OnDisable()
    {
        Draw.OnSingleBondCreated -= CheckForSingleBonds;
        Draw.OnDoubleBondCreated -= CheckForDoubleBonds;
    }
}
