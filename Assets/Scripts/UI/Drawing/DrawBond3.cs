using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class DrawBond3 : MonoBehaviour
{
    [Header("2D References")]
    [SerializeField] private DrawBond drawBond = null;

    [Header("3D Attributes")]
    [SerializeField] private Transform[] vertices3D = null;

    [Header("Prefabs")]
    [SerializeField] private GameObject bondObject = null;
    [SerializeField] private GameObject[] elementPrefabs = null;

    private Dictionary<string, Transform> transforms = new Dictionary<string, Transform>();
    private Dictionary<string, Material> materials = new Dictionary<string, Material>();
    
    public List<GameObject> BondList { get; private set; } = new List<GameObject>();

    private void Awake()
    {
        foreach (Transform vertex in vertices3D)
        {
            transforms.Add(vertex.name, vertex);
        }

        foreach (GameObject element in elementPrefabs)
        {
            Material material = element.GetComponent<MeshRenderer>().sharedMaterial;
            string elementName = element.name.Replace("Element", "");
            if (materials.ContainsKey(elementName) == false)
                materials.Add(elementName, material);
        }
    }

    private void OnEnable()
    {
        drawBond.OnBondErased += DeleteBonds;
        drawBond.OnSingleBondDrawn += SpawnSingleBond;
        drawBond.OnDoubleBondDrawn += SpawnDoubleBond;
        drawBond.OnTripleBondDrawn += SpawnTripleBond;
        drawBond.OnTrantientBondDrawn += SpawnTrantientBond;
    }

    private void SpawnSingleBond(Transform vertex1, Transform vertex2)
    {
        Transform origin = transforms[vertex1.parent.name]; //only works if the hexagonal vertices and the vertices 3D share the same names
        Transform end = transforms[vertex2.parent.name];

        string label1 = vertex1.name.Replace("Icon(Clone)", "");
        string name1 = Regex.Replace(label1, @"\d", "");
        string label2 = vertex2.name.Replace("Icon(Clone)", "");
        string name2 = Regex.Replace(label2, @"\d", "");

        Material material1 = materials[name1];
        Material material2 = materials[name2];

        GameObject bond = InstantiateBondBetweenPoints(origin, end);
        bond.name = label1 + "-" + label2;

        BondList.Add(bond);
    }

    private void SpawnDoubleBond(Transform vertex1, Transform vertex2)
    {
        Transform origin = transforms[vertex1.parent.name]; //only works if the hexagonal vertices and the vertices 3D share the same names
        Transform end = transforms[vertex2.parent.name];

        string label1 = vertex1.name.Replace("Icon(Clone)", "");
        string name1 = Regex.Replace(label1, @"\d", "");
        string label2 = vertex2.name.Replace("Icon(Clone)", "");
        string name2 = Regex.Replace(label2, @"\d", "");

        Material material1 = materials[name1];
        Material material2 = materials[name2];

        string bondName = label1 + "-" + label2;

        var single1 = BondList.Where(obj => obj.name.Contains(bondName)).FirstOrDefault();
        SearchAndDestroy(single1);

        GameObject bond1 = InstantiateBondBetweenPoints(origin, end);
        bond1.transform.position += (bond1.transform.forward * 0.03f);
        bond1.name = label1 + "=" + label2 + "1";

        GameObject bond2 = InstantiateBondBetweenPoints(origin, end);
        bond2.transform.position += (bond2.transform.forward * -0.03f);
        bond2.name = label1 + "=" + label2 + "2";

        BondList.Add(bond1);
        BondList.Add(bond2);
    }

    private void SpawnTripleBond(Transform vertex1, Transform vertex2)
    {
        Transform origin = transforms[vertex1.parent.name]; //only works if the hexagonal vertices and the vertices 3D share the same names
        Transform end = transforms[vertex2.parent.name];

        string label1 = vertex1.name.Replace("Icon(Clone)", "");
        string name1 = Regex.Replace(label1, @"\d", "");
        string label2 = vertex2.name.Replace("Icon(Clone)", "");
        string name2 = Regex.Replace(label2, @"\d", "");

        Material material1 = materials[name1];
        Material material2 = materials[name2];

        string bondName1 = label1 + "=" + label2 + "1";
        string bondName2 = label1 + "=" + label2 + "2";

        var double1 = BondList.Where(obj => obj.name.Contains(bondName1)).FirstOrDefault();
        var double2 = BondList.Where(obj => obj.name.Contains(bondName2)).FirstOrDefault();
        SearchAndDestroy(double1);
        SearchAndDestroy(double2);

        GameObject bond1 = InstantiateBondBetweenPoints(origin, end);
        bond1.name = label1 + "#" + label2 + "1";

        GameObject bond2 = InstantiateBondBetweenPoints(origin, end);
        bond2.transform.position += (bond2.transform.forward * 0.05f);
        bond2.name = label1 + "#" + label2 + "2";

        GameObject bond3 = InstantiateBondBetweenPoints(origin, end);
        bond3.transform.position += (bond3.transform.forward * -0.05f);
        bond3.name = label1 + "#" + label2 + "3";

        BondList.Add(bond1);
        BondList.Add(bond2);
        BondList.Add(bond3);
    }

    private void SpawnTrantientBond(Transform vertex1, Transform vertex2)
    {
        Transform origin = transforms[vertex1.parent.name]; //only works if the hexagonal vertices and the vertices 3D share the same names
        Transform end = transforms[vertex2.parent.name];

        string label1 = vertex1.name.Replace("Icon(Clone)", "");
        string name1 = Regex.Replace(label1, @"\d", "");
        string label2 = vertex2.name.Replace("Icon(Clone)", "");
        string name2 = Regex.Replace(label2, @"\d", "");

        Material material1 = materials[name1];
        Material material2 = materials[name2];

        string bondName1 = label1 + "#" + label2 + "1";
        string bondName2 = label1 + "#" + label2 + "2";
        string bondName3 = label1 + "#" + label2 + "3";

        var triple1 = BondList.Where(obj => obj.name.Contains(bondName1)).FirstOrDefault();
        var triple2 = BondList.Where(obj => obj.name.Contains(bondName2)).FirstOrDefault();
        var triple3 = BondList.Where(obj => obj.name.Contains(bondName3)).FirstOrDefault();
        SearchAndDestroy(triple1);
        SearchAndDestroy(triple2);
        SearchAndDestroy(triple3);

        GameObject dashed = InstantiateBondBetweenPoints(origin, end, 0.4f);
        dashed.name = label1 + "_" + label2;

        BondList.Add(dashed);
    }

    private void DeleteBonds(Transform vertex1, Transform vertex2)
    {
        Transform origin = transforms[vertex1.parent.name]; //only works if the hexagonal vertices and the vertices 3D share the same names
        Transform end = transforms[vertex2.parent.name];

        string label1 = vertex1.name.Replace("Icon(Clone)", "");
        string label2 = vertex2.name.Replace("Icon(Clone)", "");

        string bondName = label1 + "#" + label2 + "1";

        var dashed = BondList.Where(obj => obj.name.Contains(bondName)).FirstOrDefault();
        SearchAndDestroy(dashed);
    }

    private GameObject InstantiateBondBetweenPoints(Transform origin, Transform end)
    {
        //to calculate position
        Vector3 v1 = origin.position;
        Vector3 v2 = end.position;
        Vector3 midPoint = (v1 + v2) / 2;
        
        //to calculate rotation
        Vector3 v3 = v2 - v1;
        float d = v3.magnitude;
        Vector3 n3 = v3 / d;

        //instantiate object
        GameObject bond = Instantiate(bondObject, midPoint, Quaternion.identity, origin);
        bond.transform.up = n3;
        float widthX = bond.transform.localScale.x;
        float widthZ = bond.transform.localScale.z;
        bond.transform.localScale = new Vector3(widthX, d * 4.0f, widthZ); //the 2.35 value is due to the prefab's dimensions.
        
        return bond;
    }

    private GameObject InstantiateBondBetweenPoints(Transform origin, Transform end, float scale)
    {
        //to calculate position
        Vector3 v1 = origin.position;
        Vector3 v2 = end.position;
        Vector3 midPoint = (v1 + v2) / 2;

        //to calculate rotation
        Vector3 v3 = v2 - v1;
        float d = v3.magnitude;
        Vector3 n3 = v3 / d;

        //instantiate object
        GameObject bond = Instantiate(bondObject, midPoint, Quaternion.identity, origin);
        bond.transform.up = n3;
        bond.transform.localScale = new Vector3(scale, d * 4.0f, scale); //the 2.35 value is due to the prefab's dimensions.

        return bond;
    }

    private void ChangeBondMaterials(GameObject bond, Material mat1, Material mat2)
    {
        //get material references
        MeshRenderer bondMesh = bond.GetComponent<MeshRenderer>();
        bondMesh.materials[0] = mat1;
        bondMesh.materials[1] = mat2;
    }

    public void SearchAndDestroy(GameObject item)
    {
        if (item != null)
        {
            RemoveItemFromList(item, BondList);
            Destroy(item);
        }
    }

    private void RemoveItemFromList(GameObject item, List<GameObject> list)
    {
        if (list.Contains(item))
        {
            list.Remove(item);
            list.RemoveAll(GameObject => GameObject == null);
        }
    }

    public void ToggleDrawnBonds(bool state)
    {
        if(BondList != null)
        {
            foreach (var bond in BondList)
            {
                bond.gameObject.SetActive(state);
            }
        }
    }

    public void Clear3DVertices()
    {
        foreach (var vertex in vertices3D)
        {
            if (vertex.childCount > 0)
            {
                foreach (Transform child in vertex)
                {
                    Destroy(child.gameObject);
                }
            }
        }
        BondList.Clear();
    }

    private void OnDisable()
    {
        drawBond.OnBondErased -= DeleteBonds;
        drawBond.OnSingleBondDrawn -= SpawnSingleBond;
        drawBond.OnDoubleBondDrawn -= SpawnDoubleBond;
        drawBond.OnTripleBondDrawn -= SpawnTripleBond;
        drawBond.OnTrantientBondDrawn -= SpawnTrantientBond;
    }
}
