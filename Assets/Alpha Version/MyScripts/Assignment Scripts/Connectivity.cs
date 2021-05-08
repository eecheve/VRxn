using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enumerators;

[RequireComponent(typeof(Rigidbody))]
public class Connectivity : MonoBehaviour
{
    public EGeometry eGeometry;
    public List<Transform> connectedAtoms = new List<Transform>();
    public AtomLabel atomLabel;
    public VertexPlacement vertexPlacement;

    private void OnDestroy()
    {
        connectedAtoms.Clear();
    }
}
