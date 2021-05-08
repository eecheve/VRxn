using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Enumerators;

public class ChemistryManager : MonoBehaviour
{
    public UnityEvent detectingFromTop;
    public UnityEvent detectingFromBottom;

    [SerializeField] private LayerMask layerMask = 0;

    [Header("dienophile")]
    [SerializeField] private Transform C2 = null;
    [SerializeField] private Direction directionC2 = Direction.D_down;

    [Header("diene")]
    [SerializeField] private Transform C6 = null;
    [SerializeField] private Direction directionC6 = Direction.D_up;

    private Transform dienophile;
    private Transform diene;
    private Vector3 vector2;
    private Vector3 vector6;

    private void Awake()
    {
        dienophile = C2.parent;
        diene = C6.parent;
        Debug.Log(diene.name + ", " + dienophile.name);

        vector2 = InitiallizeDirection(directionC2, C2);
        vector6 = InitiallizeDirection(directionC6, C6);
    }

    private Vector3 InitiallizeDirection(Direction direction, Transform t)
    {
        Vector3 value;

        switch (direction)
        {
            case Direction.D_up:
                value = t.up;
                break;
            case Direction.D_down:
                value = -t.up;
                break;
            case Direction.D_left:
                value = -t.right;
                break;
            case Direction.D_right:
                value = t.right;
                break;
            case Direction.D_forward:
                value = t.forward;
                break;
            case Direction.D_back:
                value = -t.forward;
                break;
            default:
                value = Vector3.zero;
                break;
        }

        return value;
    }

    private void Update()
    {
        Ray ray2up = new Ray(C2.position, vector2);
        Ray ray6up = new Ray(C6.position, vector6);
        Ray ray2down = new Ray(C2.position, -vector2);
        Ray ray6down = new Ray(C6.position, -vector6);

        bool flag1 = Physics.Raycast(ray2up, 1.0f, layerMask);
        bool flag2 = Physics.Raycast(ray6up, 1.0f, layerMask);
        bool flag3 = Physics.Raycast(ray2down, 1.0f, layerMask);
        bool flag4 = Physics.Raycast(ray6down, 1.0f, layerMask);

        if (flag1 && flag2)
        {
            Debug.Log("coming from the top");
            detectingFromTop.Invoke();
        }
        else if (flag3 && flag4)
        {
            Debug.Log("coming from bottom");
            detectingFromTop.Invoke();
        }
    }
}
