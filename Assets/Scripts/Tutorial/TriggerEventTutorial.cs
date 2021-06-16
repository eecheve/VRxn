using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerEventTutorial : Tutorial
{
    [Tooltip("The tag of the object to look for on trigger enter")]
    [SerializeField] private string referenceTag = "";

    [Tooltip("The transform where you want the collider to be placed")]
    [SerializeField] private Transform colliderTransform = null;
    [SerializeField] private Vector3 colliderSize = Vector3.one;

    private bool isCurrentTutorial = false;
    private BoxCollider col;

    public TutorialCompleted OnTriggerEntered;

    private void Awake()
    {
        col = GetComponent<BoxCollider>();

        Vector3 localPos = transform.InverseTransformPoint(colliderTransform.position);
        col.center = localPos;
        col.size = colliderSize;
        col.size = colliderSize;
    }

    public override void CheckIfHappening()
    {
        isCurrentTutorial = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isCurrentTutorial)
            return;

        if(other.CompareTag(referenceTag))
        {
            Debug.Log("TriggerEventTutorial: object with tag has entered trigger zone");
            TutorialManager.Instance.CompletedTutorial();
            OnTriggerEntered?.Invoke();
            isCurrentTutorial = false;
        }
    }

    
}
