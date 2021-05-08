using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DrawLine : MonoBehaviour
{
    [Header("Drawing Attributes")]
    [SerializeField] private LayerMask layerMask = 0;
    [SerializeField] private GameObject lineObject = null;

    [Header("UI Attributes")]
    [SerializeField] private ButtonsPanel buttonsPanel = null;

    private List<GameObject> iconsToManage = new List<GameObject>();
    private List<GameObject> drawnElements = new List<GameObject>();
    private List<GameObject> bondList = new List<GameObject>();
    private int iconCount = 0;

    private GameObject elementIcon = null;
    private Gradient lineGradient = new Gradient();
    private GradientColorKey[] colorKeys = new GradientColorKey[4];
    private GradientAlphaKey[] alphaKeys = new GradientAlphaKey[1];

    private void OnEnable()
    {
        TriggerButtonWatcher.Instance.onLeftTriggerPress.AddListener(OnTriggerButtonEvent);
        TriggerButtonWatcher.Instance.onRightTriggerPress.AddListener(OnTriggerButtonEvent);

        PrimaryButtonWatcher.Instance.onRightPrimaryPress.AddListener(ManageUndoFromButton);
    }

    private void OnTriggerButtonEvent(bool pressed)
    {
        if (pressed)
        {
            Debug.Log("DrawLine: Listening to trigger button");
            HandleDrawingSystem(GameManager.Instance.LeftUIController.transform);
            HandleDrawingSystem(GameManager.Instance.RightUIController.transform);
        }
    }
   
    private void HandleDrawingSystem(Transform controllerT)
    {
        Ray ray = new Ray(controllerT.position, controllerT.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, layerMask))
        {
            Debug.Log("DrawLine: raycast working");
            if (hit.collider.gameObject.CompareTag("Ink"))
            {
                Debug.Log("DrawLine: detecting click on icon");
                
                iconsToManage.Add(hit.collider.gameObject);
                if (iconsToManage.Count == 2)
                {
                    Debug.Log("DrawLine: will connect with " + iconsToManage[1].name);
                    SpawnLineOnHit(iconsToManage[0], iconsToManage[1]);
                    iconsToManage.Clear();
                }
            }
            else if (hit.collider.gameObject.CompareTag("Whiteboard"))
            {
                Debug.Log("DrawLine: whiteboard detected");
                InstantiateIconOnHit(hit);
            }
        }
    }

    private void InstantiateIconOnHit(RaycastHit hit)
    {
        if (elementIcon != null)
            SpawnIconOnHit(hit);
    }

    private void SpawnIconOnHit(RaycastHit hit)
    {
        Debug.Log("DrawLine: hiting the transform " + hit.transform.name);
        Debug.Log("DrawLine: Spawning icon on hit");
        //var go = Instantiate(elementIcon, hit.point + (hit.transform.forward * -0.075f), hit.transform.rotation, transform);
        var go = Instantiate(elementIcon, hit.point + (hit.transform.forward * -0.075f), hit.transform.rotation, hit.transform);
        go.name = go.name.Replace("_icon_w", "");
        go.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
        iconCount++;
        go.name += iconCount.ToString();
        iconsToManage.Add(go);
        drawnElements.Add(go);
    }
    
    private void SpawnLineOnHit(RaycastHit hit)
    {
        Vector3 midPoint = (iconsToManage[0].transform.position + (hit.point + hit.transform.up * 0.05f)) / 2;
        Vector3 origin = iconsToManage[0].transform.position;
        Vector3 end = hit.point;
        string name1 = iconsToManage[0].name;
        string name2 = iconsToManage[1].name;

        //CreateLine(midPoint, origin, end, name1, name2, "-");
        CreateLine(iconsToManage[0].transform, iconsToManage[1].transform, "-");

        iconsToManage.Clear();
    }

    private void SpawnLineOnHit(GameObject obj1, GameObject obj2)
    {
        Vector3 midPoint = (obj1.transform.position + obj2.transform.position) / 2;
        Vector3 origin = obj1.transform.position;
        Vector3 end = obj2.transform.position;

        Vector3 refVector = end - origin;
        Vector3 rotVector = new Vector3(refVector.y, -refVector.x, refVector.z);
        Vector3 dir = rotVector.normalized;

        var single1 = bondList.Where(obj => obj.name == (obj1.name + "-" + obj2.name)).FirstOrDefault();
        var single2 = bondList.Where(obj => obj.name == (obj2.name + "-" + obj1.name)).FirstOrDefault();
        var double1 = bondList.Where(obj => obj.name == (obj1.name + "=" + obj2.name)).FirstOrDefault();
        var double2 = bondList.Where(obj => obj.name == (obj2.name + "=" + obj1.name)).FirstOrDefault();
        var triple1 = bondList.Where(obj => obj.name == (obj1.name + "#" + obj2.name)).FirstOrDefault();
        var triple2 = bondList.Where(obj => obj.name == (obj2.name + "#" + obj1.name)).FirstOrDefault();

        if (single1 != null || single2 != null)
        {
            SearchAndDestroy(single1, bondList);
            SearchAndDestroy(single2, bondList);

            Vector3 origin1 = origin + (dir * 0.03f);
            Vector3 origin2 = origin - (dir * 0.03f);
            Vector3 end1 = end + (dir * 0.03f);
            Vector3 end2 = end - (dir * 0.03f);

            CreateLine(obj1.transform, obj2.transform, dir, 0.03f, "=");
            CreateLine(obj1.transform, obj2.transform, dir, -0.03f, "=");
        }
        else if(double1 != null || double2 != null)
        {
            SearchAndDestroy(double1, bondList);
            SearchAndDestroy(double2, bondList);

            var double3 = bondList.Where(obj => obj.name == (obj1.name + "=" + obj2.name)).FirstOrDefault();
            var double4 = bondList.Where(obj => obj.name == (obj2.name + "=" + obj1.name)).FirstOrDefault();
            SearchAndDestroy(double3, bondList);
            SearchAndDestroy(double4, bondList);

            Vector3 origin1 = origin + (dir * 0.05f);
            Vector3 origin2 = origin - (dir * 0.05f);
            Vector3 end1 = end + (dir * 0.05f);
            Vector3 end2 = end - (dir * 0.05f);

            CreateLine(obj1.transform, obj2.transform, dir, 0.05f, "#");
            CreateLine(obj1.transform, obj2.transform, "#");
            CreateLine(obj1.transform, obj2.transform, dir, -0.05f, "#");
        }
        else if(triple1 != null || triple2 != null)
        {
            return;
        }
        else
        {
            CreateLine(obj1.transform, obj2.transform, "-");
        }
    }

    private void SearchAndDestroy(GameObject item, List<GameObject> list)
    {
        if (list.Contains(item))
        {
            drawnElements.Remove(item);
            bondList.Remove(item);
            Destroy(item);
        }
    }

    private void CreateLine(Transform origin, Transform end, string bondType)
    {
        string oName = origin.name;
        string eName = end.name;

        Vector3 o = origin.position;
        Vector3 e = end.position;
        Vector3 midPoint = (o + e) / 2;

        GameObject line = Instantiate(lineObject, midPoint, Quaternion.identity, origin.parent);
        line.name = oName + bondType + eName;
        line.GetComponent<LineHolder>().SetLinePoints(origin, end);

        LineRenderer lineRend = line.GetComponent<LineRenderer>();
        SetLineGradient(lineRend);

        lineRend.SetPosition(0, o);
        lineRend.SetPosition(1, e);

        drawnElements.Add(line);
        bondList.Add(line);
    }

    private void CreateLine(Transform origin, Transform end, Vector3 direction, float scale, string bondType)
    {
        string oName = origin.name;
        string eName = end.name;

        Vector3 o = origin.position + (direction * scale);
        Vector3 e = end.position + (direction * scale);
        Vector3 midPoint = (o + e) / 2;

        GameObject line = Instantiate(lineObject, midPoint, Quaternion.identity, origin.parent);
        line.name = oName + bondType + eName;

        GameObject newOrigin = new GameObject();
        GameObject newEnd = new GameObject();

        newOrigin.transform.parent = origin.parent;
        newEnd.transform.parent = origin.parent;
        
        newOrigin.transform.position = o;
        newEnd.transform.position = e;

        line.GetComponent<LineHolder>().SetLinePoints(newOrigin.transform, newEnd.transform);

        LineRenderer lineRend = line.GetComponent<LineRenderer>();
        SetLineGradient(lineRend);

        lineRend.SetPosition(0, o);
        lineRend.SetPosition(1, e);

        drawnElements.Add(line);
        bondList.Add(line);
    }

    private void SetLineGradient(LineRenderer lineRend)
    {
        SpriteRenderer spr1 = iconsToManage[0].GetComponent<SpriteRenderer>();
        SpriteRenderer spr2 = elementIcon.GetComponent<SpriteRenderer>();
        Color color1 = spr1.color;
        Color color2 = spr2.color;
        
        colorKeys[0].color = color1;
        colorKeys[0].time = 0f;
        colorKeys[1].color = color1;
        colorKeys[1].time = 0.500f;
        colorKeys[2].color = color2;
        colorKeys[2].time = 0.501f;
        colorKeys[3].color = color2;
        colorKeys[3].time = 1f;

        alphaKeys[0].alpha = 1f;
        alphaKeys[0].time = 0f;

        lineGradient.SetKeys(colorKeys, alphaKeys);
        lineRend.colorGradient = lineGradient;
    }

    public void SetElement(GameObject element)
    {
        elementIcon = element;
        iconsToManage.Clear();
    }

    private void ManageUndoFromButton(bool pressed)
    {
        if (pressed && buttonsPanel != null && buttonsPanel.gameObject.activeSelf == true)
        {
            UndoDrawing();
        }
    }

    public void UndoDrawing()
    {
        if (drawnElements.Any())
        {
            var lastDrawing = drawnElements.Last();
            
            if (lastDrawing != null)
            {
                drawnElements.Remove(lastDrawing);

                if (!lastDrawing.name.Contains("-") && !lastDrawing.name.Contains("=") && !lastDrawing.name.Contains("#"))
                {
                    RemoveItemFromList(lastDrawing, iconsToManage);
                    iconCount--;
                }
                else
                {
                    RemoveItemFromList(lastDrawing, bondList);
                }
                Destroy(lastDrawing);
                StartCoroutine(WaitEndOfFrame());
                bondList.RemoveAll(GameObject => GameObject == null);
                iconsToManage.RemoveAll(GameObject => GameObject == null);
                drawnElements.RemoveAll(GameObject => GameObject == null);
            }
        }
    }

    private void RemoveItemFromList(GameObject item, List<GameObject> list)
    {
        if (list.Contains(item))
        {
            list.Remove(item);
        }
    }

    private IEnumerator WaitEndOfFrame()
    {
        yield return new WaitForEndOfFrame();
    }

    public void ClearDrawing()
    {
        foreach (var item in drawnElements)
        {
            Destroy(item);
        }
        StartCoroutine(WaitEndOfFrame());

        iconsToManage.Clear();
        bondList.Clear();
        drawnElements.Clear();
        iconCount = 0;
    }

    private void OnDisable()
    {
        TriggerButtonWatcher.Instance.onLeftTriggerPress.RemoveListener(OnTriggerButtonEvent);
        TriggerButtonWatcher.Instance.onRightTriggerPress.RemoveListener(OnTriggerButtonEvent);

        PrimaryButtonWatcher.Instance.onRightPrimaryPress.RemoveListener(ManageUndoFromButton);
    }
}
