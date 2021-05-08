using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OppositeVerticesConnector : MonoBehaviour
{
    [SerializeField] private Color secondaryColor = Color.white;

    private TopVertex top1 = null;
    private TopVertex top2 = null;
    private BottomVertex bottom1 = null;
    private BottomVertex bottom2 = null;

    private ElementIcon topIcon1 = null;
    private ElementIcon topIcon2 = null;
    private ElementIcon bottomIcon1 = null;
    private ElementIcon bottomIcon2 = null;

    private bool oppositesHighlighted = false;
    private bool backBoneCompleted = false;

    public delegate void BackboneMilestone();
    public static event BackboneMilestone OnBackboneCompleted;

    private DrawingPointsManager pointsManager;

    public Color SecondaryColor { get { return secondaryColor; } private set { secondaryColor = value; } }

    private void Awake()
    {
        pointsManager = GetComponent<DrawingPointsManager>();
    }

    private void OnEnable()
    {
        TopVertexManager.OnDoubleBondMade += SetVerticesToConnectByCase;
        Draw.OnSingleBondCreated += CheckForVerticesConnected;
    }

    private void SetVerticesToConnectByCase()
    {
        if (pointsManager.BottomVertexManager.BuildCase == 1)
        {
            top1 = pointsManager.TopVertexManager.Vertices[1];
            top2 = pointsManager.TopVertexManager.Vertices[2];

            bottom1 = pointsManager.BottomVertexManager.Vertices[0];
            bottom2 = pointsManager.BottomVertexManager.Vertices[3];

            EnableVerticesWithSecondaryColor();
        }
        else if (pointsManager.BottomVertexManager.BuildCase == 2)
        {
            top1 = pointsManager.TopVertexManager.Vertices[4];
            top2 = pointsManager.TopVertexManager.Vertices[5];

            bottom1 = pointsManager.BottomVertexManager.Vertices[3];
            bottom2 = pointsManager.BottomVertexManager.Vertices[0];

            EnableVerticesWithSecondaryColor();
        }
        else if (pointsManager.BottomVertexManager.BuildCase == 3)
        {
            top1 = pointsManager.TopVertexManager.Vertices[2];
            top2 = pointsManager.TopVertexManager.Vertices[3];

            bottom1 = pointsManager.BottomVertexManager.Vertices[1];
            bottom2 = pointsManager.BottomVertexManager.Vertices[4];

            EnableVerticesWithSecondaryColor();
        }
        else if (pointsManager.BottomVertexManager.BuildCase == 4)
        {
            top1 = pointsManager.TopVertexManager.Vertices[3];
            top2 = pointsManager.TopVertexManager.Vertices[4];

            bottom1 = pointsManager.BottomVertexManager.Vertices[2];
            bottom2 = pointsManager.BottomVertexManager.Vertices[5];

            EnableVerticesWithSecondaryColor();
        }
        else
        {
            Debug.Log("OppositeVerticesConnector says: wrong build case dude!");
        }
    }

    private void CheckForVerticesConnected()
    {
        if(oppositesHighlighted == true)
        {
            if (topIcon1 == null)
                topIcon1 = top1.Icon;

            if (topIcon2 == null)
                topIcon2 = top2.Icon;

            if (bottomIcon1 == null)
                bottomIcon1 = bottom1.Icon;

            if (bottomIcon2 == null)
                bottomIcon2 = bottom2.Icon;

            int value1 = 0;
            int value2 = 0;
            
            if (topIcon1 != null)
            {
                LineHolder[] topLines1 = topIcon1.gameObject.GetComponentsInChildren<LineHolder>();

                foreach (var line in topLines1)
                {
                    value1 += (line.name.Contains(bottom1.Icon.name)).IntValue();
                }
            }

            if (bottomIcon1 != null)
            {
                LineHolder[] bottomLines1 = bottomIcon1.gameObject.GetComponentsInChildren<LineHolder>();

                foreach (var line in bottomLines1)
                {
                    value1 += (line.name.Contains(top1.Icon.name)).IntValue();
                }
            }

            if (topIcon2 != null)
            {
                LineHolder[] topLines2 = topIcon2.gameObject.GetComponentsInChildren<LineHolder>();

                foreach (var line in topLines2)
                {
                    value2 += (line.name.Contains(bottom2.Icon.name)).IntValue();
                }
            }

            if (bottomIcon2 != null)
            {
                LineHolder[] bottomLines1 = bottomIcon2.gameObject.GetComponentsInChildren<LineHolder>();

                foreach (var line in bottomLines1)
                {
                    value2 += (line.name.Contains(top2.Icon.name)).IntValue();
                }
            }

            if(value1 > 0 && value2 > 0)
            {
                StopHighlights();

                Debug.Log("OppositeVerticesConnector_CheckForVertices: Backbone completed");

                if (OnBackboneCompleted != null && backBoneCompleted == false)
                {
                    OnBackboneCompleted();
                    backBoneCompleted = true;
                }
            }

        }
    }

    private void EnableVerticesWithSecondaryColor()
    {
        oppositesHighlighted = true;
        
        top1.SpriteRenderer.color = SecondaryColor;
        top1.SpriteRenderer.enabled = true;
        top1.SphereCollider.enabled = true;

        top2.SpriteRenderer.color = SecondaryColor;
        top2.SpriteRenderer.enabled = true;
        top2.SphereCollider.enabled = true;

        bottom1.SpriteRenderer.color = SecondaryColor;
        bottom1.SpriteRenderer.enabled = true;
        bottom1.SphereCollider.enabled = true;

        bottom2.SpriteRenderer.color = SecondaryColor;
        bottom2.SpriteRenderer.enabled = true;
        bottom2.SphereCollider.enabled = true;
    }

    private void StopHighlights()
    {
        top1.SpriteRenderer.enabled = false;
        top1.SphereCollider.enabled = false;
        top2.SpriteRenderer.enabled = false;
        top2.SphereCollider.enabled = false;

        bottom1.SpriteRenderer.enabled = false;
        bottom1.SphereCollider.enabled = false;
        bottom2.SpriteRenderer.enabled = false;
        bottom2.SphereCollider.enabled = false;
    }

    public void ResetHighlights()
    {
        top1.SpriteRenderer.color = top1.InitialColor;
        top2.SpriteRenderer.color = top2.InitialColor;

        bottom1.SpriteRenderer.color = bottom1.InitialColor;
        bottom2.SpriteRenderer.color = bottom2.InitialColor;

        top1.SpriteRenderer.enabled = false;
        top2.SpriteRenderer.enabled = false;

        if (bottom1.IsInitialVertex == false)
        {
            bottom1.SpriteRenderer.enabled = false;
            bottom1.SphereCollider.enabled = false;
        }
        if (bottom2.IsInitialVertex == false)
        {
            bottom2.SpriteRenderer.enabled = false;
            bottom2.SphereCollider.enabled = false;
        }

        top1 = null;
        top2 = null;
        bottom1 = null;
        bottom2 = null;

        topIcon1 = null;
        topIcon2 = null;
        bottomIcon1 = null;
        bottomIcon2 = null;

        oppositesHighlighted = false;
        backBoneCompleted = false;
    }

    private void OnDisable()
    {
        TopVertexManager.OnDoubleBondMade -= SetVerticesToConnectByCase;
        Draw.OnSingleBondCreated -= CheckForVerticesConnected;
    }
}
