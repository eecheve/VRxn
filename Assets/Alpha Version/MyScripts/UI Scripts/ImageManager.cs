using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ImageManager : MonoBehaviour
{
    public UnityEvent imageUpdate;

    public void ImageUpdate()
    {
        imageUpdate.Invoke();
    }
}
