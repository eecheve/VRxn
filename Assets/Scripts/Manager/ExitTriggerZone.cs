using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExitTriggerZone : MonoBehaviour
{
    public UnityEvent TriggerZoneEvents;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TriggerZoneEvents?.Invoke();
        }
    }
}
