using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConditionTutorial))]
public class MoveFromSpot : MonoBehaviour
{
    [SerializeField] private Transform player = null;
    [SerializeField] private Transform refSpot = null;
    
    private Vector3 initialPos;
    private ConditionTutorial condition;
    private Vector3 refVector;

    private void OnEnable()
    {
        condition = GetComponent<ConditionTutorial>();
        initialPos = refSpot.position;
    }

    private void Update()
    {
        refVector = player.position - initialPos;

        Debug.Log(name + " MoveFromSpot: " + refVector.magnitude.ToString());

        if(refVector.magnitude > 0.1)
        {
            Debug.Log(name + " MoveFromSpot: Player has moved from spot");
            condition.FulfillCondition();
            this.enabled = false;
        }
    }
}
