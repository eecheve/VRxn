using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireworks : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] particles = null;

    public void Fire()
    {
        Debug.Log($"Fireworks is being listened at");
        foreach (var particle in particles)
        {
            Debug.Log($"Playing the particle {particle.name}");
            particle.Play();
        }
    }
}
