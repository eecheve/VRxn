using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRightCommand : ICommand
{
    private Transform _transform;
    private float _speed;

    public MoveRightCommand(Transform transform, float speed)
    {
        this._transform = transform;
        this._speed = speed;
    }
    public void Execute()
    {
        _transform.position += (Vector3.right * _speed);
    }

    public void Undo()
    {
        _transform.position -= (Vector3.right * _speed);
    }
}
