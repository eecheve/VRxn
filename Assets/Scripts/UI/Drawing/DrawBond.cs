using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DrawBond : MonoBehaviour
{
    [SerializeField] private SelectElement selector = null;
    [SerializeField] private GameObject lineObject = null;
    [SerializeField] private Button button = null;

    private List<GameObject> bondList = new List<GameObject>();

    private Gradient lineGradient = new Gradient();
    private GradientColorKey[] colorKeys = new GradientColorKey[4];
    private GradientAlphaKey[] alphaKeys = new GradientAlphaKey[1];

    private GameObject obj1;
    private GameObject obj2;

    private void OnEnable()
    {
        button.onClick.AddListener(DrawTheBond);
    }

    public void DrawTheBond()
    {
        obj1 = selector.CurrentSelected;
        obj2 = selector.LastSelected;

        if(obj1 != null && obj2 != null)
        {
            Debug.Log("DrawBond: the two objects to connect are detected");
            Debug.Log("DrawBond: current is " + obj1.name);
            Debug.Log("DrawBond: last is " + obj2.name);
            Vector3 refVector = obj2.transform.position - obj1.transform.position;
            Vector3 rotVector = Vector3.Cross(refVector, obj1.transform.forward);
            Vector3 dir = rotVector.normalized; //perpendicular direction from refVector

            var single1 = bondList.Where(obj => obj.name.Contains(obj1.name + "-" + obj2.name)).FirstOrDefault();
            var single2 = bondList.Where(obj => obj.name.Contains(obj2.name + "-" + obj1.name)).FirstOrDefault();
            var double1 = bondList.Where(obj => obj.name.Contains(obj1.name + "=" + obj2.name)).FirstOrDefault();
            var double2 = bondList.Where(obj => obj.name.Contains(obj2.name + "=" + obj1.name)).FirstOrDefault();
            var triple1 = bondList.Where(obj => obj.name.Contains(obj1.name + "#" + obj2.name)).FirstOrDefault();
            var triple2 = bondList.Where(obj => obj.name.Contains(obj2.name + "#" + obj1.name)).FirstOrDefault();

            if (single1 != null || single2 != null)
            {
                Debug.Log("DrawBond: there was a single bond here");
                SearchAndDestroy(single1);
                SearchAndDestroy(single2);

                CreateLine(obj1.transform, obj2.transform, dir, 0.03f, "=");
                CreateLine(obj1.transform, obj2.transform, dir, -0.03f, "=");
            }
            else if (double1 != null || double2 != null)
            {
                Debug.Log("DrawBond: there was a double bond here");
                SearchAndDestroy(double1);
                SearchAndDestroy(double2);

                var double3 = bondList.Where(obj => obj.name == (obj1.name + "=" + obj2.name)).FirstOrDefault();
                var double4 = bondList.Where(obj => obj.name == (obj2.name + "=" + obj1.name)).FirstOrDefault();
                SearchAndDestroy(double3);
                SearchAndDestroy(double4);

                CreateLine(obj1.transform, obj2.transform, dir, 0.05f, "#");
                CreateLine(obj1.transform, obj2.transform, "#");
                CreateLine(obj1.transform, obj2.transform, dir, -0.05f, "#");
            }
            else if (triple1 != null || triple2 != null)
            {
                Debug.Log("DrawBond: there was a triple bond here");
                SearchAndDestroy(triple1);
                SearchAndDestroy(triple2);

                var triple3 = bondList.Where(obj => obj.name == (obj1.name + "#" + obj2.name)).FirstOrDefault();
                var triple4 = bondList.Where(obj => obj.name == (obj2.name + "#" + obj1.name)).FirstOrDefault();
                SearchAndDestroy(triple3);
                SearchAndDestroy(triple4);

                var triple5 = bondList.Where(obj => obj.name == (obj1.name + "#" + obj2.name)).FirstOrDefault();
                var triple6 = bondList.Where(obj => obj.name == (obj2.name + "#" + obj1.name)).FirstOrDefault();
                SearchAndDestroy(triple5);
                SearchAndDestroy(triple6);

                CreateLine(obj1.transform, obj2.transform, "-");
            }
            else
            {
                Debug.Log("DrawBond: There were no bonds between the elements");
                CreateLine(obj1.transform, obj2.transform, "-");
            }
        }
        else
        {
            Debug.Log("DrawBond: Function called when one or two of the objects is deleted");
            if(obj1 == null)
            {
                Debug.Log("DrawBond: The problem is the current selected");
            }
            else if(obj2 == null)
            {
                Debug.Log("DrawBond: The problem is the last selected");
            }
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

    private void CreateLine(Transform origin, Transform end, Vector3 direction, float offset, string bondType)
    {
        string oName = origin.name;
        string eName = end.name;

        Vector3 o = origin.position + (direction * offset);
        Vector3 e = end.position + (direction * offset);
        Vector3 midPoint = (o + e) / 2;

        GameObject line = Instantiate(lineObject, midPoint, Quaternion.identity, origin);
        line.name = oName + bondType + eName;

        GameObject newOrigin = new GameObject();
        GameObject newEnd = new GameObject();

        newOrigin.transform.parent = origin.parent;
        newEnd.transform.parent = end.parent;

        newOrigin.transform.position = o;
        newEnd.transform.position = e;

        line.GetComponent<LineHolder>().SetLinePoints(newOrigin.transform, newEnd.transform);

        LineRenderer lineRend = line.GetComponent<LineRenderer>();
        SetLineGradient(lineRend);

        lineRend.SetPosition(0, o);
        lineRend.SetPosition(1, e);

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

    private void SetLineGradient(LineRenderer lineRend)
    {
        Image image1 = obj1.GetComponent<Image>();
        Image image2 = obj2.GetComponent<Image>();
        
        Color color1 = image1.color;
        Color color2 = image2.color;

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

    public void SearchAndDestroy(GameObject item)
    {
        if (item != null)
        {
            Debug.Log("DrawBond: " + item.name + " is found");
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

    private void OnDisable()
    {
        button.onClick.RemoveListener(DrawTheBond);
    }
}
