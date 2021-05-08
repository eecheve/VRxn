using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class WhiteboardInfluenceZone : MonoBehaviour
{
    public UnityEvent triggerEnterEvents;
    public UnityEvent triggerExitEvents;
    
    [SerializeField] private DrawLine drawSystem = null;
    [SerializeField] private Color highlight = Color.white;

    private Color initialColor;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialColor = spriteRenderer.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("WhiteboardInfluenceZone: A collider has entered the fray");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("WhiteboardInfluenceZone: The collider is the player");
            //ToggleDrawSystem(true);
            spriteRenderer.color = highlight;

            triggerEnterEvents.Invoke();
        }
    }

    public void ToggleDrawSystem(bool state)
    {
        if (drawSystem != null)
            drawSystem.enabled = state;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //ToggleDrawSystem(false);
            spriteRenderer.color = initialColor;

        }

        triggerExitEvents.Invoke();
    }
}
