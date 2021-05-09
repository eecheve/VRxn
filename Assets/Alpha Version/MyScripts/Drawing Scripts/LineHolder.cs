using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enumerators;

[RequireComponent(typeof(LineRenderer))]
public class LineHolder : MonoBehaviour
{
    private Transform m_origin;
    private Transform m_end;
    private LineRenderer m_line;

    private GrabAndRotate leftRotate;
    private GrabAndRotate rightRotate;

    public BondType BondType { get; set; }

    private void Start()
    {
        m_line = GetComponent<LineRenderer>();
    }

    private void OnEnable()
    {
        leftRotate = ControllerReferences.Instance.LUIController.GetComponent<GrabAndRotate>();
        rightRotate = ControllerReferences.Instance.RUIController.GetComponent<GrabAndRotate>();

        leftRotate.OnSpriteRotated += RefreshLinePoints;
        rightRotate.OnSpriteRotated += RefreshLinePoints;
    }

    public void SetLinePoints(Transform origin, Transform end)
    {
        m_origin = origin;
        m_end = end;
    }

    public void RefreshLinePoints()
    {
        if(m_origin != null && m_end != null)
        {
            m_line.SetPosition(0, m_origin.position);
            m_line.SetPosition(1, m_end.position);
        }
    }
    
    private void OnDestroy()
    {
        leftRotate.OnSpriteRotated -= RefreshLinePoints;
        rightRotate.OnSpriteRotated -= RefreshLinePoints;
    }
}
