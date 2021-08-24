using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class DrawElement3 : MonoBehaviour
{
    [Header("2D references")]
    [SerializeField] private HexagonalVertex[] hexagonalVertices = null;

    [Header("Attributes")]
    [SerializeField] Transform[] vertices3D = null;
    [SerializeField] private GameObject[] elementPrefabs = null;

    private Dictionary<string, GameObject> elements = new Dictionary<string, GameObject>();

    private void Awake()
    {
        foreach (var element in elementPrefabs)
        {
            string elementName = element.name;
            elementName = elementName.Replace("Element", "");
            elements.Add(elementName, element);
        }
    }

    private void OnEnable()
    {
        Icon2D.OnElementDeleted += DeleteElement;
        
        foreach (HexagonalVertex vert in hexagonalVertices)
        {
            vert.OnWichVertOccupied += SpawnElement;
        }
    }

    private void SpawnElement(string vertName)
    {
        for (int i = 0; i < hexagonalVertices.Length; i++)
        {
            if(hexagonalVertices[i].name == vertName)
            {
                Icon2D icon = hexagonalVertices[i].Icon;
                string elementName = Regex.Replace(icon.name, @"\d", "");
                elementName = elementName.Replace("Icon(Clone)", "");
                GameObject element = elements[elementName];

                Instantiate(element, vertices3D[i].position, vertices3D[i].rotation, vertices3D[i]);
                break;
            }
        }
    }

    private void DeleteElement(Vertex vertex)
    {
        for (int i = 0; i < hexagonalVertices.Length; i++)
        {
            if(hexagonalVertices[i].name == vertex.name)
            {
                Transform element = vertices3D[i].GetChild(0);
                if (element != null)
                    DestroyImmediate(element.gameObject);

                break;
            }
        }
    }

    private void OnDisable()
    {
        Icon2D.OnElementDeleted -= DeleteElement;

        foreach (HexagonalVertex vert in hexagonalVertices)
        {
            vert.OnWichVertOccupied -= SpawnElement;
        }
    }
}
