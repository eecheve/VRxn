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
    [SerializeField] private Vector3 positionOffset = Vector3.zero;
    [SerializeField] private Vector3 colliderSize = Vector3.one;

    private bool isCurrentTutorial = false;
    private BoxCollider col;

    public TutorialCompleted OnTriggerEntered;

    private void Awake()
    {
        col = GetComponent<BoxCollider>();

        Vector3 localPos = transform.InverseTransformPoint(colliderTransform.position);
        col.center = localPos + positionOffset;
        col.size = colliderSize;
    }

    public override void CheckIfHappening()
    {
        isCurrentTutorial = true;
        Debug.Log("TriggerEventTutorial: bool value is " + isCurrentTutorial.ToString());
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TriggerEventTutorial: " + name + " " +  other.gameObject.name);

        /*if (!isCurrentTutorial)
        {
            Debug.Log("TriggerEventTutorial: it is not current tutorial");
            return;
        }*/
        
        if(other.gameObject.CompareTag(referenceTag))
        {
            Debug.Log("TriggerEventTutorial: object with tag has entered trigger zone");
            OnTriggerEntered?.Invoke();
            isCurrentTutorial = false;
            TutorialManager.Instance.CompletedTutorial();
        }
    }

    
}
