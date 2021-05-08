using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalHighlighter : MonoBehaviour
{
    private Light m_light;
    private MeshRenderer m_mesh;
    private MeshCollider m_collider;
    private Animator m_animator;
    
    private void Awake()
    {
        m_light = GetComponentInChildren<Light>();
        m_mesh = GetComponent<MeshRenderer>();
        m_collider = GetComponent<MeshCollider>();
        m_animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerExit(Collider other)
    {
        CompareTagAndSetTrigger(other, "Molecule", "Animate");
        CompareTagAndSetTrigger(other, "Water", "AnimateTutorial");
        CompareTagAndSetTrigger(other, "TutorialMolecule", "AnimateTutorial2");

        
        /*if (other.CompareTag("Molecule"))
        {
            light.enabled = false;
            mesh.enabled = false;
            collider.enabled = false;
            GetComponentInParent<Animator>().SetTrigger("Animate");
        }
        else if (other.CompareTag("Water"))
        {
            light.enabled = false;
            mesh.enabled = false;
            collider.enabled = false;
            GetComponentInParent<Animator>().SetTrigger("AnimateTutorial");
        }
        else if (other.CompareTag("TutorialMolecule"))
        {
            light.enabled = false;
            mesh.enabled = false;
            collider.enabled = false;
            GetComponentInParent<Animator>().SetTrigger("AnimateTutorial2");
        }*/
    }

    private void CompareTagAndSetTrigger(Collider other, string tag, string trigger)
    {
        if (other.CompareTag(tag))
        {
            SetMeshAndLights(false);
            m_animator.SetTrigger(trigger);
        }
    }

    public void SetMeshAndLights(bool state)
    {
        m_light.enabled = state;
        m_mesh.enabled = state;
        m_collider.enabled = state;
    }

    public void ActivatePedestal(string trigger)
    {
        m_animator.SetTrigger(trigger);
        SetMeshAndLights(true);
    }
}
