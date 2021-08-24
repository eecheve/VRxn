using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawBond3Animation : MonoBehaviour
{
    [Serializable] public struct VTransforms
    {
        public Transform origin;
        public Transform end;

        public VTransforms(Transform origin, Transform end)
        {
            this.origin = origin;
            this.end = end;
        }
    }

    [Header("UI Attributes")]
    [SerializeField] Slider slider = null;

    [Header("Reference Frame Attributes")]
    [SerializeField] private DrawBond3 drawBond = null;
    [SerializeField] private Transform[] points3D = null;

    [Header("Script Attributes")]
    [SerializeField] private Material virtualBondMaterial = null;

    private readonly int maxVirtualBondCount = 14;
    private GameObject[] virtualBonds;

    private List<string> pointNames = new List<string>();
    private Dictionary<string, Transform> pointsDict = new Dictionary<string, Transform>();
    private Dictionary<GameObject, VTransforms> bondsDict = new Dictionary<GameObject, VTransforms>();


    private void Awake()
    {
        InitializeVirtualBonds();
        InitiallizeRefPointsDict();
        ToggleVirtualBonds(false);
    }

    private void InitiallizeRefPointsDict()
    {
        foreach (var point in points3D)
        {
            //make sure the point 3D array is in the same order as the points of the object you want to animate
            pointNames.Add(point.name);
            pointsDict.Add(point.name, point);
        }

        VTransforms key0 = GetDictEntryByIndices(0, 1);
        VTransforms key1 = GetDictEntryByIndices(1, 2);
        VTransforms key2 = GetDictEntryByIndices(2, 3);
        VTransforms key3 = GetDictEntryByIndices(3, 4);
        VTransforms key4 = GetDictEntryByIndices(3, 7);
        VTransforms key5 = GetDictEntryByIndices(0, 6);
        VTransforms key6 = GetDictEntryByIndices(0, 5);

        VTransforms key7 = GetDictEntryByIndices(0, 11);
        VTransforms key8 = GetDictEntryByIndices(3, 10);

        VTransforms key9 = GetDictEntryByIndices(8, 11);
        VTransforms key10 = GetDictEntryByIndices(9, 10);
        VTransforms key11 = GetDictEntryByIndices(10, 11);
        VTransforms key12 = GetDictEntryByIndices(10, 13);
        VTransforms key13 = GetDictEntryByIndices(11, 12);

        bondsDict.Add(virtualBonds[0], key0);
        bondsDict.Add(virtualBonds[1], key1);
        bondsDict.Add(virtualBonds[2], key2);
        bondsDict.Add(virtualBonds[3], key3);
        bondsDict.Add(virtualBonds[4], key4);
        bondsDict.Add(virtualBonds[5], key5);
        bondsDict.Add(virtualBonds[6], key6);
        bondsDict.Add(virtualBonds[7], key7);
        bondsDict.Add(virtualBonds[8], key8);
        bondsDict.Add(virtualBonds[9], key9);
        bondsDict.Add(virtualBonds[10], key10);
        bondsDict.Add(virtualBonds[11], key11);
        bondsDict.Add(virtualBonds[12], key12);
        bondsDict.Add(virtualBonds[13], key13);
    }

    private VTransforms GetDictEntryByIndices(int index1, int index2)
    {
        VTransforms value = new VTransforms(pointsDict[pointNames[index1]], pointsDict[pointNames[index2]]);
        return value;
    }

    private void InitializeVirtualBonds()
    {
        virtualBonds = new GameObject[maxVirtualBondCount];
        for (int i = 0; i < maxVirtualBondCount; i++)
        {
            virtualBonds.SetValue(GameObject.CreatePrimitive(PrimitiveType.Cylinder), i);
            virtualBonds[i].transform.localScale = new Vector3(0.02f, 0.1f, 0.02f);
            Destroy(virtualBonds[i].GetComponent<CapsuleCollider>());
            virtualBonds[i].GetComponent<MeshRenderer>().material = virtualBondMaterial;
            virtualBonds[i].transform.parent = points3D[0].parent;
            virtualBonds[i].SetActive(false);
        }
    }

    private void OnEnable()
    {
        slider.onValueChanged.AddListener(AnimateBonds);
    }

    private void AnimateBonds(float value)
    {
        if (value == 0)
        {
            ToggleVirtualBonds(false);
            drawBond.ToggleDrawnBonds(true);
        }
        else
        {
            drawBond.ToggleDrawnBonds(false);
            foreach (var virtualBond in virtualBonds)
            {
                Transform origin = bondsDict[virtualBond].origin;
                Transform end = bondsDict[virtualBond].end;

                Vector3 v0 = origin.position;
                Vector3 v1 = end.position;

                Vector3 refVector = v1 - v0;
                Vector3 midPoint = (v0 + v1) / 2;
                float distance = refVector.magnitude;
                Vector3 refDir = refVector / distance;

                virtualBond.SetActive(true);

                virtualBond.transform.up = refDir;
                virtualBond.transform.localScale = new Vector3(0.02f, distance / 2f, 0.02f);
                virtualBond.transform.position = midPoint;
            }
        }
    }

    private void TestVirtualBonds()
    {
        foreach (var virtualBond in virtualBonds)
        {
            Transform origin = bondsDict[virtualBond].origin;
            Transform end = bondsDict[virtualBond].end;

            Vector3 v0 = origin.position;
            Vector3 v1 = end.position;

            Vector3 refVector = v1 - v0;
            Vector3 midPoint = (v0 + v1) / 2;
            float distance = refVector.magnitude;
            Vector3 refDir = refVector / distance;

            virtualBond.SetActive(true);

            virtualBond.transform.up = refDir;
            virtualBond.transform.localScale = new Vector3(0.02f, distance / 2f, 0.02f);
            virtualBond.transform.position = midPoint;
        }
    }

    private void ToggleVirtualBonds(bool state)
    {
        foreach (var virtualBond in virtualBonds)
        {
            virtualBond.SetActive(state);
        }
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(AnimateBonds);
    }
}
