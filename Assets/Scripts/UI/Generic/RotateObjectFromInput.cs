using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using static Enumerators;

public class RotateObjectFromInput : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference input = null;
    [SerializeField] [Range(-180, 180)] private float angle = 0;
    [SerializeField] private CartesianAxis rotationAxis = CartesianAxis.y;

    private void OnEnable()
    {
        input.action.performed += RotateObject;
    }

    private void RotateObject(InputAction.CallbackContext obj)
    {

        var cardinal = CardinalUtility.GetNearestCardinal(obj.ReadValue<Vector2>());
        switch (cardinal)
        {
            case Cardinal.North:
                break;
            case Cardinal.South:
                break;
            case Cardinal.West:
                Debug.Log("RotateObjectFromInput: object should rotate left");
                RotateOnAngle(-angle);
                break;
            case Cardinal.East:
                Debug.Log("RotateObjectFromInput: object should rotate right");
                RotateOnAngle(angle);
                break;
            default:
                Assert.IsTrue(false, $"Unhandled {nameof(Cardinal)}={cardinal}");
                break;
        }
    }

    private void RotateOnAngle(float angle)
    {
        Vector3 euler = transform.rotation.eulerAngles;

        if (rotationAxis == CartesianAxis.x)
        {
            euler += new Vector3(angle, 0, 0);
        }
        else if(rotationAxis == CartesianAxis.y)
        {
            euler += new Vector3(0, angle, 0);
        }
        else
        {
            euler += new Vector3(0, 0, angle);
        }

        Quaternion rotation = Quaternion.Euler(euler);
        transform.rotation = rotation;
    }

    private void OnDisable()
    {
        input.action.performed -= RotateObject;
    }
}
