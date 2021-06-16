using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventTeleporter : MonoBehaviour
{
    [SerializeField] private Transform player = null;

    public void Teleport(Transform newT)
    {
        player.position = newT.position;
        player.rotation = newT.rotation;
    }
}
