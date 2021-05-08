using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ControllerReferences : MonoSingleton<ControllerReferences>
{
    [SerializeField] private XRDirectInteractor rightDirectInteractor = null;
    [SerializeField] private XRRayInteractor rightRayInteractor = null;

    [SerializeField] private XRDirectInteractor leftDirectInteractor = null;
    [SerializeField] private XRRayInteractor leftRayInteractor = null;

    public XRDirectInteractor RDirectInteractor { get { return rightDirectInteractor; } set { rightDirectInteractor = value; } }
    public XRRayInteractor RRayInteractor { get { return rightRayInteractor; } set { rightRayInteractor = value; } }
    public XRDirectInteractor LDirectInteractor { get { return leftDirectInteractor; } set { leftDirectInteractor = value; } }
    public XRRayInteractor LRayInteractor { get { return leftRayInteractor; } set { leftRayInteractor = value; } }

}
