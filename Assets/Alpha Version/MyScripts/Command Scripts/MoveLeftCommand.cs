using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftCommand : ICommand
{
    private Transform _transform;
    private float _speed;

    public MoveLeftCommand(Transform transform, float speed)
    {
        this._transform = transform;
        this._speed = speed;
    }
    public void Execute()
    {
        _transform.position += (Vector3.left * _speed);
    }

    public void Undo()
    {
        _transform.position -= (Vector3.left * _speed);
    }
}
