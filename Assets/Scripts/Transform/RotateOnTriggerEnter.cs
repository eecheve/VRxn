using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enumerators;

public class RotateOnTriggerEnter : MonoBehaviour
{
    [SerializeField] private Transform objectToRotate = null;
    
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
                        objectToRotate.right = transform.right;

                    else if (rotateAxis == CartesianAxis.y)
                        objectToRotate.up = transform.right;

                    else
                        objectToRotate.forward = transform.right;
                }
                else if (referenceAxis == CartesianAxis.y)
                {
                    if (rotateAxis == CartesianAxis.x)
                        objectToRotate.right = transform.up;

                    else if (rotateAxis == CartesianAxis.y)
                        objectToRotate.up = transform.up;

                    else
                        objectToRotate.forward = transform.up;
                }
                else
                {
                    if (rotateAxis == CartesianAxis.x)
                        objectToRotate.right = transform.forward;

                    else if (rotateAxis == CartesianAxis.y)
                        objectToRotate.up = transform.forward;

                    else
                        objectToRotate.forward = transform.forward;
                }
            }
            else
            {
                if (referenceAxis == CartesianAxis.x)
                {
                    if (rotateAxis == CartesianAxis.x)
                        objectToRotate.right = -transform.right;

                    else if (rotateAxis == CartesianAxis.y)
                        objectToRotate.up = -transform.right;

                    else
                        objectToRotate.forward = -transform.right;
                }
                else if (referenceAxis == CartesianAxis.y)
                {
                    if (rotateAxis == CartesianAxis.x)
                        objectToRotate.right = -transform.up;

                    else if (rotateAxis == CartesianAxis.y)
                        objectToRotate.up = -transform.up;

                    else
                        objectToRotate.forward = -transform.up;
                }
                else
                {
                    if (rotateAxis == CartesianAxis.x)
                        objectToRotate.right = -transform.forward;

                    else if (rotateAxis == CartesianAxis.y)
                        objectToRotate.up = -transform.forward;

                    else
                        objectToRotate.forward = -transform.forward;
                }
            }
        }
    }
    
}
