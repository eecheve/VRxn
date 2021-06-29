using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enumerators;

public class RotateOnTriggerEnter : MonoBehaviour
{
    [SerializeField] private Transform[] objectsToRotate = null;
    
    [Tooltip("Object's local axis to rotate")]
    [SerializeField] private CartesianAxis rotateAxis = CartesianAxis.x;

    [Tooltip("Object's local axis will align to this trigger zone's reference")]
    [SerializeField] private CartesianAxis referenceAxis = CartesianAxis.x;

    [Tooltip("If checked, rotation is inverted")]
    [SerializeField] private bool inverted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player has entered zone " + name);
            
            if (inverted == false)
            {
                if (referenceAxis == CartesianAxis.x)
                {
                    if (rotateAxis == CartesianAxis.x)
                    {
                        for (int i = 0; i < objectsToRotate.Length; i++)
                        {
                            objectsToRotate[i].right = transform.right;
                        }
                    }
                    else if (rotateAxis == CartesianAxis.y)
                    {
                        for (int i = 0; i < objectsToRotate.Length; i++)
                        {
                            objectsToRotate[i].up = transform.right;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < objectsToRotate.Length; i++)
                        {
                            objectsToRotate[i].forward = transform.right;
                        }
                    }
                }
                else if (referenceAxis == CartesianAxis.y)
                {
                    if (rotateAxis == CartesianAxis.x)
                    {
                        for (int i = 0; i < objectsToRotate.Length; i++)
                        {
                            objectsToRotate[i].right = transform.up;
                        }
                    }
                    else if (rotateAxis == CartesianAxis.y)
                    {
                        for (int i = 0; i < objectsToRotate.Length; i++)
                        {
                            objectsToRotate[i].up = transform.up;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < objectsToRotate.Length; i++)
                        {
                            objectsToRotate[i].forward = transform.up;
                        }
                    }
                }
                else
                {
                    if (rotateAxis == CartesianAxis.x)
                    {
                        for (int i = 0; i < objectsToRotate.Length; i++)
                        {
                            objectsToRotate[i].right = transform.forward;
                        }
                    }
                    else if (rotateAxis == CartesianAxis.y)
                    {
                        for (int i = 0; i < objectsToRotate.Length; i++)
                        {
                            objectsToRotate[i].up = transform.forward;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < objectsToRotate.Length; i++)
                        {
                            objectsToRotate[i].forward = transform.forward;
                        }
                    }
                }
            }
            else
            {
                if (referenceAxis == CartesianAxis.x)
                {
                    if (rotateAxis == CartesianAxis.x)
                    {
                        for (int i = 0; i < objectsToRotate.Length; i++)
                        {
                            objectsToRotate[i].right = -transform.right;
                        }
                    }
                    else if (rotateAxis == CartesianAxis.y)
                    {
                        for (int i = 0; i < objectsToRotate.Length; i++)
                        {
                            objectsToRotate[i].up = -transform.right;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < objectsToRotate.Length; i++)
                        {
                            objectsToRotate[i].forward = -transform.right;
                        }
                    }
                }
                else if (referenceAxis == CartesianAxis.y)
                {
                    if (rotateAxis == CartesianAxis.x)
                    {
                        for (int i = 0; i < objectsToRotate.Length; i++)
                        {
                            objectsToRotate[i].right = -transform.up;
                        }
                    }
                    else if (rotateAxis == CartesianAxis.y)
                    {
                        for (int i = 0; i < objectsToRotate.Length; i++)
                        {
                            objectsToRotate[i].up = -transform.up;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < objectsToRotate.Length; i++)
                        {
                            objectsToRotate[i].forward = -transform.up;
                        }
                    }
                }
                else
                {
                    if (rotateAxis == CartesianAxis.x)
                    {
                        for (int i = 0; i < objectsToRotate.Length; i++)
                        {
                            objectsToRotate[i].right = -transform.forward;
                        }
                    }
                    else if (rotateAxis == CartesianAxis.y)
                    {
                        for (int i = 0; i < objectsToRotate.Length; i++)
                        {
                            objectsToRotate[i].up = -transform.forward;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < objectsToRotate.Length; i++)
                        {
                            objectsToRotate[i].forward = -transform.forward;
                        }
                    }
                }
            }
        }
    }
}
