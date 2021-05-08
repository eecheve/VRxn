using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRController))]
public class DrawIcons : MonoBehaviour
{
    [SerializeField] private LayerMask drawLayerMask = 0;
    [SerializeField] private GameObject lineObject = null;

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
        if(GetComponent<XRController>().controllerNode == XRNode.RightHand)
        {
            TriggerButtonWatcher.Instance.onRightTriggerPress.AddListener(OnTriggerButtonEvent);
        }
        else if(GetComponent<XRController>().controllerNode == XRNode.LeftHand)
        {
            TriggerButtonWatcher.Instance.onLeftTriggerPress.AddListener(OnTriggerButtonEvent);
        }
    }

    private void OnTriggerButtonEvent(bool pressed)
    {
        if (pressed)
        {
            Debug.Log("DrawIcons: Listening to trigger button");
            HandleDrawingSystem();
        }
    }

    private void HandleDrawingSystem()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, drawLayerMask))
        {
            Debug.Log("DrawIcons: raycast working");
            if (hit.collider.gameObject.CompareTag("Ink"))
            {
                Debug.Log("DrawIcons: detecting click on icon");

                iconsToManage.Add(hit.collider.gameObject);
                if (iconsToManage.Count == 2)
                {
                    Debug.Log("DrawIcons: will connect with " + iconsToManage[1].name);
                    SpawnLineOnHit(iconsToManage[0], iconsToManage[1]);
                    iconsToManage.Clear();
                }
            }
            else if (hit.collider.gameObject.CompareTag("Whiteboard"))
            {
                Debug.Log("DrawIcons: whiteboard detected");
                InstantiateIconOnHit(hit);
            }
        }
    }

    private void InstantiateIconOnHit(RaycastHit hit)
    {
        if (elementIcon != null)
        {
            Debug.Log("DarIcons: Element icon is " + elementIcon.name);
            SpawnIconOnHit(hit);
            if (iconsToManage.Count > 1)
            {
                SpawnLineOnHit(hit);
            }
        }
    }

    private void SpawnIconOnHit(RaycastHit hit)
    {
        Debug.Log("DrawIcons: hiting the transform " + hit.transform.name);
        Debug.Log("DrawIcons: Spawning icon on hit");
        //var go = Instantiate(elementIcon, hit.point + (hit.transform.forward * -0.075f), hit.transform.rotation, transform);
        var go = Instantiate(elementIcon, hit.point + (hit.transform.forward * -0.075f), hit.transform.rotation, hit.transform);
        go.name = go.name.Replace("_icon_w", "");
        go.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
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

        CreateLine(midPoint, origin, end, name1, name2, "-");

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

            CreateLine(midPoint, origin1, end1, obj1.name, obj2.name, "=");
            CreateLine(midPoint, origin2, end2, obj1.name, obj2.name, "=");
        }
        else if (double1 != null || double2 != null)
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

            CreateLine(midPoint, origin1, end1, obj1.name, obj2.name, "#");
            CreateLine(midPoint, origin, end, obj1.name, obj2.name, "#");
            CreateLine(midPoint, origin2, end2, obj1.name, obj2.name, "#");
        }
        else if (triple1 != null || triple2 != null)
        {
            return;
        }
        else
        {
            CreateLine(midPoint, origin, end, obj1.name, obj2.name, "-");
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

    private void CreateLine(Vector3 midPoint, Vector3 origin, Vector3 end, string name1, string name2, string bondType)
    {
        var line = Instantiate(lineObject, midPoint, Quaternion.identity, transform);
        line.name = name1 + bondType + name2;
        drawnElements.Add(line);
        bondList.Add(line);

        LineRenderer lineRend = line.GetComponent<LineRenderer>();
        SetLineGradient(lineRend);

        lineRend.SetPosition(0, origin);
        lineRend.SetPosition(1, end);
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

    public void UndoDrawing()
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
        if (GetComponent<XRController>().controllerNode == XRNode.RightHand)
        {
            TriggerButtonWatcher.Instance.onRightTriggerPress.RemoveListener(OnTriggerButtonEvent);
        }
        else if (GetComponent<XRController>().controllerNode == XRNode.LeftHand)
        {
            TriggerButtonWatcher.Instance.onLeftTriggerPress.RemoveListener(OnTriggerButtonEvent);
        }
    }
}
