using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DistanceEvents : MonoBehaviour
{
    public UnityEvent thresholdSurpassed;

    [SerializeField] private Transform ref1 = null;
    [SerializeField] private Transform ref2 = null;

    private float maxSqrDist;

    private void OnEnable()
    {
        maxSqrDist = (ref1.transform.position - ref2.transform.position).sqrMagnitude + 1.2f;
    }

    private void Update()
    {
        Vector3 refVector = ref2.position - ref1.position;
        if(refVector.sqrMagnitude > maxSqrDist)
        {
            thresholdSurpassed.Invoke();
        }
    }
}
