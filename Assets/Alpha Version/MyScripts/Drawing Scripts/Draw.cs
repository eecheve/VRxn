using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Draw : MonoBehaviour
{
    [Header("Drawing Attributes")]
    [SerializeField] private LayerMask layerMask = 0;
    [SerializeField] private GameObject lineObject = null; //line object prefab. instantiated to form the bonds.

    [Header("UI Attributes")]
    [SerializeField] private GameObject highlight = null; //visual aid that changes position to show the current selected icon

    private GameObject elementIcon = null; //icon to instantiate, modified externally by pressing buttons
    private int iconCount = 0;

    private GameObject iconInHistory = null; //this will save the last icon selected, used in the DrawBond()
    private GameObject selectedIcon = null; //this will save the current icon selected

    private List<GameObject> drawnElements = new List<GameObject>(); //these lists will hold the instantiated objects to implement the undo functionality
    private List<GameObject> bondList = new List<GameObject>();

    private Gradient lineGradient = new Gradient();
    private GradientColorKey[] colorKeys = new GradientColorKey[4];
    private GradientAlphaKey[] alphaKeys = new GradientAlphaKey[1];

    public delegate void ElementCreated();
    public static event ElementCreated OnElementCreated;
    
    public delegate void BondCreated();
    public static event BondCreated OnSingleBondCreated;
    public static event BondCreated OnDoubleBondCreated;
    public static event BondCreated OnTripleBondCreated;

    private void OnEnable()
    {
        TriggerButtonWatcher.Instance.onLeftTriggerPress.AddListener(ManageDrawInput);
        TriggerButtonWatcher.Instance.onRightTriggerPress.AddListener(ManageDrawInput);

        PrimaryButtonWatcher.Instance.onRightPrimaryPress.AddListener(ManageUndoFromButton);

        if(highlight != null)
        {
            highlight.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void ManageDrawInput(bool pressed)
    {
        if(pressed)
        {
            DrawInput(GameManager.Instance.LeftUIController.transform);
            DrawInput(GameManager.Instance.RightUIController.transform);
        }
    }

    private void DrawInput(Transform controller)
    {
        Ray ray = new Ray(controller.position, controller.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, layerMask))
        {
            if (hit.collider.gameObject.CompareTag("Ink") || hit.collider.gameObject.CompareTag("DryInk"))
            {
                MoveHighlighterToHit(hit);
                if (selectedIcon != null)
                    iconInHistory = selectedIcon;

                selectedIcon = hit.collider.gameObject;

                if(selectedIcon != null && iconInHistory != null)
                {
                    if(selectedIcon != iconInHistory)
                    {
                        DrawBond(selectedIcon, iconInHistory);

                        iconInHistory = null;
                        selectedIcon = null;
                        highlight.GetComponent<SpriteRenderer>().enabled = false;
                    }
                    else
                    {
                        iconInHistory = null;
                        selectedIcon = null;
                        highlight.GetComponent<SpriteRenderer>().enabled = false;
                    }
                    
                }
            }
            else if (hit.collider.gameObject.CompareTag("Whiteboard"))
            {
                InstantiateIconOnHit(hit);
            }
        }
    }

    private void DrawBond(GameObject obj1, GameObject obj2)
    {
        Vector3 refVector = obj2.transform.position - obj1.transform.position;
        Vector3 rotVector = new Vector3(refVector.y, -refVector.x, refVector.z);
        Vector3 dir = rotVector.normalized; //perpendicular direction from refVector

        var single1 = bondList.Where(obj => obj.name == (obj1.name + "-" + obj2.name)).FirstOrDefault();
        var single2 = bondList.Where(obj => obj.name == (obj2.name + "-" + obj1.name)).FirstOrDefault();
        var double1 = bondList.Where(obj => obj.name == (obj1.name + "=" + obj2.name)).FirstOrDefault();
        var double2 = bondList.Where(obj => obj.name == (obj2.name + "=" + obj1.name)).FirstOrDefault();
        var triple1 = bondList.Where(obj => obj.name == (obj1.name + "#" + obj2.name)).FirstOrDefault();
        var triple2 = bondList.Where(obj => obj.name == (obj2.name + "#" + obj1.name)).FirstOrDefault();

        if (single1 != null || single2 != null)
        {
            SearchAndDestroy(single1);
            SearchAndDestroy(single2);

            CreateLine(obj1.transform, obj2.transform, dir, 0.03f, "=");
            CreateLine(obj1.transform, obj2.transform, dir, -0.03f, "=");

            if (OnDoubleBondCreated != null)
                OnDoubleBondCreated();
        }
        else if (double1 != null || double2 != null)
        {
            SearchAndDestroy(double1);
            SearchAndDestroy(double2);

            var double3 = bondList.Where(obj => obj.name == (obj1.name + "=" + obj2.name)).FirstOrDefault();
            var double4 = bondList.Where(obj => obj.name == (obj2.name + "=" + obj1.name)).FirstOrDefault();
            SearchAndDestroy(double3);
            SearchAndDestroy(double4);

            CreateLine(obj1.transform, obj2.transform, dir, 0.05f, "#");
            CreateLine(obj1.transform, obj2.transform, "#");
            CreateLine(obj1.transform, obj2.transform, dir, -0.05f, "#");

            if (OnTripleBondCreated != null)
                OnTripleBondCreated();
        }
        else if (triple1 != null || triple2 != null)
        {
            return;
        }
        else
        {
            CreateLine(obj1.transform, obj2.transform, "-");
            
            if (OnSingleBondCreated != null)
                OnSingleBondCreated();
        }
    }

    private void CreateLine(Transform transform1, Transform transform2, string bondType)
    {
        string oName = transform1.name;
        string eName = transform2.name;

        Vector3 origin = transform1.position;
        Vector3 end = transform2.position;
        Vector3 midPoint = (origin + end) / 2;

        GameObject line = Instantiate(lineObject, midPoint, Quaternion.identity, transform1);
        line.name = oName + bondType + eName;
        line.GetComponent<LineHolder>().SetLinePoints(transform1, transform2);

        LineRenderer lineRend = line.GetComponent<LineRenderer>();
        SetLineGradient(lineRend);

        lineRend.SetPosition(0, origin);
        lineRend.SetPosition(1, end);

        drawnElements.Add(line);
        bondList.Add(line);

        if (bondType.Equals("-"))
        {
            line.GetComponent<LineHolder>().BondType = Enumerators.BondType.SingleBond;
        }
        else if (bondType.Equals("="))
        {
            line.GetComponent<LineHolder>().BondType = Enumerators.BondType.DoubleBond;
        }
        else
        {
            line.GetComponent<LineHolder>().BondType = Enumerators.BondType.TripleBond;
        }
    }

    private void CreateLine(Transform origin, Transform end, Vector3 direction, float scale, string bondType)
    {
        string oName = origin.name;
        string eName = end.name;

        Vector3 o = origin.position + (direction * scale);
        Vector3 e = end.position + (direction * scale);
        Vector3 midPoint = (o + e) / 2;

        GameObject line = Instantiate(lineObject, midPoint, Quaternion.identity, origin);
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

        if (bondType.Equals("-"))
        {
            line.GetComponent<LineHolder>().BondType = Enumerators.BondType.SingleBond;
        }
        else if (bondType.Equals("="))
        {
            line.GetComponent<LineHolder>().BondType = Enumerators.BondType.DoubleBond;
        }
        else
        {
            line.GetComponent<LineHolder>().BondType = Enumerators.BondType.TripleBond;
        }

        if (OnSingleBondCreated != null)
            OnSingleBondCreated();
    }

    private void SetLineGradient(LineRenderer lineRend)
    {
        SpriteRenderer spr1 = iconInHistory.GetComponent<SpriteRenderer>();
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

    private void SearchAndDestroy(GameObject item)
    {
        if (item != null)
        {
            RemoveItemFromList(item, drawnElements);
            RemoveItemFromList(item, bondList);
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

    private void MoveHighlighterToHit(RaycastHit hit)
    {
        if(highlight != null)
        {
            highlight.GetComponent<SpriteRenderer>().enabled = true;
            highlight.transform.position = hit.point + (hit.transform.forward * -0.075f);
        }
    }

    private void InstantiateIconOnHit(RaycastHit hit)
    {
        if(elementIcon != null)
        {
            var go = Instantiate(elementIcon, hit.point + (hit.transform.forward * -0.075f), hit.transform.rotation, hit.transform);
            go.name = go.name.Replace("_icon_w", ""); //<--- this could be a source of bugs if I ever change the name of the icons!!!
            go.transform.localScale = new Vector3(0.065f, 0.065f, 0.065f);
            iconCount++;
            go.name += iconCount.ToString(); //names necessary because from this it depends the draw bond function.
            drawnElements.Add(go);

            if (OnElementCreated != null)
                OnElementCreated();
        }
    }

    public void Erase()
    {
        GameObject go = selectedIcon;
        if (go != null)
        {
            highlight.GetComponent<SpriteRenderer>().enabled = false;
            if(go.transform.childCount > 0)
            {
                for (int i = 0; i < go.transform.childCount; i++)
                {
                    GameObject child = go.transform.GetChild(i).gameObject;
                    SearchAndDestroy(child);
                }
            }
            SearchAndDestroy(go);
            selectedIcon = null;
        }
    }



    public void UndoDrawing()
    {
        if (drawnElements.Any())
        {
            var lastDrawing = drawnElements.Last();
            SearchAndDestroy(lastDrawing);
        }
    }

    private void ManageUndoFromButton(bool pressed)
    {
        if (pressed)
            UndoDrawing();
    }

    public void SetElement(GameObject element)
    {
        elementIcon = element;
    }

    public void ClearDrawing()
    {
        highlight.GetComponent<SpriteRenderer>().enabled = false;

        foreach (var item in drawnElements)
        {
            Destroy(item);
        }

        bondList.Clear();
        drawnElements.Clear();
        iconCount = 0;
    }

    private void OnDisable()
    {
        TriggerButtonWatcher.Instance.onLeftTriggerPress.RemoveListener(ManageDrawInput);
        TriggerButtonWatcher.Instance.onRightTriggerPress.RemoveListener(ManageDrawInput);

        PrimaryButtonWatcher.Instance.onRightPrimaryPress.RemoveListener(ManageUndoFromButton);
    }
}
