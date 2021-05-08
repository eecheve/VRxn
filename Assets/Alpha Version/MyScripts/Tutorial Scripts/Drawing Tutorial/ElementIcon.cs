using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementIcon : MonoBehaviour
{
    public BottomVertexManager BottomVertexManager { get; set; } = null;
    public TopVertexManager TopVertexManager { get; set; } = null;
    public BottomOutsideManager BottomOutsideManager { get; set; } = null;
    public TopOutsideManager TopOutsideManager { get; set; } = null;
    public BottomAxialManager BottomAxialManager { get; set; } = null;
    public TopAxialManager TopAxialManager { get; set; } = null;

    
    public BottomVertex BottomVertex { get; set; } = null;
    public TopVertex TopVertex { get; set; } = null;
    public BottomOutside BottomOutside { get; set; } = null;
    public TopOutside TopOutside { get; set; } = null;
    public BottomAxial BottomAxial { get; set; } = null;
    public TopAxial TopAxial { get; set; } = null;
    
    public void ChangeTag()
    {
        gameObject.tag = "DryInk";
    }

    private void OnDestroy()
    {
        if(BottomVertex != null)
        {
            BottomVertex.IsOccupied = false;
            BottomVertexManager.OccupiedVertices.Remove(BottomVertex);
            BottomVertex.Icon = null;
        }

        else if (TopVertex != null)
        {
            TopVertex.IsOccupied = false;
            TopVertexManager.OccupiedVertices.Remove(TopVertex);
            TopVertex.Icon = null;
        }

        else if(BottomOutside != null)
        {
            BottomOutside.IsOccupied = false;
            BottomOutsideManager.OccupiedVertices.Remove(BottomOutside);
            BottomOutside.Icon = null;
        }

        else if (TopOutside != null)
        {
            TopOutside.IsOccupied = false;
            TopOutsideManager.OccupiedVertices.Remove(TopOutside);
            TopOutside.Icon = null;
        }

        else if(BottomAxial != null)
        {
            BottomAxial.IsOccupied = false;
            BottomAxialManager.OccupiedPoints.Remove(BottomAxial);
            BottomAxial.Icon = null;
        }

        else if(TopAxial != null)
        {
            TopAxial.IsOccupied = false;
            TopAxialManager.OccupiedPoints.Remove(TopAxial);
            TopAxial.Icon = null;
        }
    }
}
