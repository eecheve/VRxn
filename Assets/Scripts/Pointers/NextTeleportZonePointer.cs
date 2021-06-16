using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class NextTeleportZonePointer : MonoBehaviour
{
    [Tooltip("Player Transform")]
    [SerializeField] private Transform player = null;

    [Tooltip("Transform of the position to teleport to")]
    [SerializeField] private Transform reference = null;

    [Tooltip("Offset to polish the position of the line renderer")]
    [SerializeField] private Vector3 lineOffset = new Vector3();

    [Tooltip("Offset to polish the position of the end arrow")]
    [SerializeField] private Vector3 arrowEndOffset = new Vector3();

    [Tooltip("Offset to polish the position of the middle arrow")]
    [SerializeField] private Vector3 arrowMiddleOffset = new Vector3();

    [Tooltip("Rotation offset to correct the rotation of the instantiated GO")]
    [SerializeField] private Vector3 rotationOffset = new Vector3();

    [Tooltip("Prefab of the arrow object to instantiate")]
    [SerializeField] private GameObject arrow = null;

    private LineRenderer lineRenderer;
    private GameObject arrowEnd;
    private GameObject arrow2;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;

        arrow.transform.position = reference.position;

        Quaternion rotation = Quaternion.Euler(rotationOffset);

        arrowEnd = Instantiate(arrow, reference.position + arrowEndOffset, rotation, transform);
        arrowEnd.gameObject.SetActive(false);

        arrow2 = Instantiate(arrow, transform);
        arrow2.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        lineRenderer.enabled = true;
        arrowEnd.gameObject.SetActive(true);
        arrow2.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (player == null)
            Debug.LogError("NextTeleportZonePointer: player not assigned in " + name);

        if (reference == null)
            Debug.LogError("NextTeleportZonePointer: reference zone not assigned in " + name);

        lineRenderer.SetPosition(0, player.position + lineOffset);
        lineRenderer.SetPosition(1, reference.position + lineOffset);

        Vector3 midPoint = (player.position + reference.position)/2;
        Vector3 refVector = (reference.position - player.position).normalized;
        Quaternion rot = Quaternion.LookRotation(refVector);
        rot *= Quaternion.Euler(0, -90, 0);

        arrow2.transform.position = midPoint + lineOffset + arrowMiddleOffset;
        arrow2.transform.rotation = rot;
    }

    private void OnDisable()
    {
        lineRenderer.enabled = false;
        arrowEnd.gameObject.SetActive(false);
        arrow2.gameObject.SetActive(false);
    }
}
