using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vertex : MonoBehaviour
{
    [SerializeField] protected VertexManager vertexManager = null;
    [SerializeField] protected string m_tag = "";

    public bool IsOccupied { get; set; }
    public SpriteRenderer SpriteRenderer { get; set; }
    public Collider Collider { get; set; }
    public Icon2D Icon { get; set; } = null;
    public Color InitialColor { get; private set; }

    public delegate void VertexOccupied();

    protected FixedJoint fj;

    protected virtual void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Collider = GetComponent<Collider>();

        InitialColor = SpriteRenderer.color;

        fj = GetComponent<FixedJoint>();
    }

}
