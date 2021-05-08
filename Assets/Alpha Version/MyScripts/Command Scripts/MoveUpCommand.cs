using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpCommand : ICommand
{
    private Transform _transform;
    private float _speed;

    public MoveUpCommand(Transform transform, float speed)
    {
        this._transform = transform;
        this._speed = speed;
    }
    public void Execute()
    {
        _transform.position += (Vector3.up * _speed);
    }

    public void Undo()
    {
        _transform.position -= (Vector3.up * _speed);
    }
}
