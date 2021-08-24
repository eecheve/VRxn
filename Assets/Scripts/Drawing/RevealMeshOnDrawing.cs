using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealMeshOnDrawing : MonoBehaviour
{
    [SerializeField] private DrawBond drawBond = null;
    [SerializeField] private HexagonalVertex[] hexagonalVertices = null;
    [SerializeField] private Transform[] points3D = null;

    private Dictionary<string, List<Transform>> pointTransforms = new Dictionary<string, List<Transform>>();

    private void Awake()
    {
        foreach (var point in points3D)
        {
            List<Transform> list = new List<Transform>();
            foreach (Transform child in point)
            {
                if (child.name == point.name)
                    continue;
                else
                    list.Add(child);
            }
            pointTransforms.Add(point.name, list);
        }
    }

    private void OnEnable()
    {
        foreach (var vertex in hexagonalVertices)
        {
            vertex.OnWichVertOccupied += ShowElement;
        }

        Icon2D.OnElementDeleted += HideElement;

        drawBond.OnBondErased += HideBond;
        //drawBond.OnSingleBondDrawn += ShowSingleBond;
        //drawBond.OnDoubleBondDrawn += ShowDoubleBond;
        //drawBond.OnTripleBondDrawn += ShowTripleBond;
        //drawBond.OnTrantientBondDrawn += ShowDashedBond;
    }

    private void ShowElement(string elemName)
    {
        Transform element = pointTransforms[elemName][0];
        MeshRenderer meshRenderer = element.GetComponent<MeshRenderer>();
        meshRenderer.enabled = true;
    }

    private void HideElement(Vertex vertex)
    {
        Transform element = pointTransforms[vertex.name][0];
        MeshRenderer meshRenderer = element.GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }

    private void HideBond(Transform vertex1, Transform vertex2)
    {
        throw new NotImplementedException();
    }

    private void ShowSingleBond(Transform vertex1, Transform vertex2)
    {
        throw new NotImplementedException();
    }

    private void ShowDoubleBond(Transform vertex1, Transform vertex2)
    {
        throw new NotImplementedException();
    }

    private void ShowTripleBond(Transform vertex1, Transform vertex2)
    {
        throw new NotImplementedException();
    }

    private void ShowDashedBond(Transform vertex1, Transform vertex2)
    {
        throw new NotImplementedException();
    }

    private void ShowBond(List<Transform> bondList, string tag)
    {
        foreach (var bond in bondList)
        {
            if (bond.CompareTag(tag) == true)
            {
                MeshRenderer mesh = bond.GetComponent<MeshRenderer>();
                mesh.enabled = true;
                break;
            }
        }
    }

    private void HideBonds(List<Transform> bondList)
    {
        foreach (var bond in bondList)
        {
            MeshRenderer mesh = bond.GetComponent<MeshRenderer>();
            mesh.enabled = false;
        }
    }

    private void OnDisable()
    {
        drawBond.OnBondErased -= HideBond;
        drawBond.OnSingleBondDrawn -= ShowSingleBond;
        drawBond.OnDoubleBondDrawn -= ShowDoubleBond;
        drawBond.OnTripleBondDrawn -= ShowTripleBond;
        drawBond.OnTrantientBondDrawn -= ShowDashedBond;
    }
}
