using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionRaycast : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask = 0;

    public List<Ring> Rings { get; set; } = new List<Ring>();

    public delegate void ObservationTask();
    public event ObservationTask OnTwoRingsObserved;
    public event ObservationTask OnObservationTaskCompleted;
    public event ObservationTask OnHandsObserved;

    private bool ringsCounted = false;

    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, layerMask))
        {
            Ring ring = hit.collider.gameObject.GetComponent<Ring>();

            if (ring != null && ring.Observed == false)
            {
                if (Rings.Contains(ring) == false)
                {
                    Rings.Add(ring);
                    ring.GetComponent<SpriteRenderer>().color = Color.blue;
                }
            }
            else if (hit.collider.CompareTag("Hands"))
            {
                Debug.Log("VisionRaycast: I am observing my hands!");

                if (OnHandsObserved != null)
                    OnHandsObserved();
            }
        }

        if(ringsCounted == false)
        {
            RingCount();
        }
    }

    private void RingCount()
    {
        if (Rings.Count == 2)
        {
            ButtonHelperManager.Instance.ToggleHelpers(ButtonHelperManager.Instance.RotateHelper, true);

            if (OnTwoRingsObserved != null)
                OnTwoRingsObserved();
        }
        else if (Rings.Count == 3)
        {
            ButtonHelperManager.Instance.ToggleHelpers(ButtonHelperManager.Instance.RotateHelper, false);

            if (OnObservationTaskCompleted != null)
                OnObservationTaskCompleted();

            ringsCounted = true;
        }
    }
}
