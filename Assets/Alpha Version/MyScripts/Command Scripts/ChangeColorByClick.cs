using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorByClick : ICommand
{
    private GameObject _objectClicked;
    private Color _newColor;
    private Color _previousColor;

    public ChangeColorByClick(GameObject objectClicked, Color color)
    {
        /// stores which object was last clicked, what's is current color and a new color
        this._objectClicked = objectClicked;
        this._newColor = color;
        this._previousColor = _objectClicked.GetComponent<MeshRenderer>().material.color;
    }

    public void Execute()
    {
        Material mat = _objectClicked.GetComponent<MeshRenderer>().material;
        _previousColor = mat.color;
        mat.color = _newColor;
    }

    public void Undo()
    {
        Material mat = _objectClicked.GetComponent<MeshRenderer>().material;
        mat.color = _previousColor;
    }
}
