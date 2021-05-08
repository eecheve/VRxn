using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCommand : MonoBehaviour, ICommand
{
    private GameObject _toInstantiate;
    private Vector3 _position;
    private Transform _refTransform;

    public InstantiateCommand(GameObject toInstantiate, Vector3 position, Transform refTransform)
    {
        this._toInstantiate = toInstantiate;
        this._position = position;
        this._refTransform = refTransform;
    }

    public void Execute()
    {
        Instantiate(_toInstantiate, _position + (_refTransform.up * 0.01f), _refTransform.rotation, _refTransform);
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}