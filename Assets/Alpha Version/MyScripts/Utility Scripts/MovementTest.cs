using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTest : MonoBehaviour
{
    private void Update()
    {
        DebugPosition();
    }

    private void DebugPosition()
    {
        Debug.Log(transform.name + " current pos : (" +
                  transform.position.x.ToString() + "," +
                  transform.position.y.ToString() + "," +
                  transform.position.z.ToString() + ")");
    }

    private void DebugRotation()
    {
        Debug.Log(transform.name + "current rot : (" +
                  transform.rotation.x.ToString() + "," +
                  transform.rotation.y.ToString() + "," +
                  transform.rotation.z.ToString() + ")");
    }
}
