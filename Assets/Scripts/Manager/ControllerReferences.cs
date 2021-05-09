using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ControllerReferences : MonoSingleton<ControllerReferences>
{
    [SerializeField] private XRDirectInteractor rightDirectInteractor = null;
    [SerializeField] private XRRayInteractor rightRayInteractor = null;
    [SerializeField] private XRRayInteractor leftUIInteractor = null;

    [SerializeField] private XRDirectInteractor leftDirectInteractor = null;
    [SerializeField] private XRRayInteractor leftRayInteractor = null;
    [SerializeField] private XRRayInteractor rightUIInteractor = null;


    public XRDirectInteractor RDirectInteractor { get { return rightDirectInteractor; } private set { rightDirectInteractor = value; } }
    public XRRayInteractor RRayInteractor { get { return rightRayInteractor; } private set { rightRayInteractor = value; } }
    public XRDirectInteractor LDirectInteractor { get { return leftDirectInteractor; } private set { leftDirectInteractor = value; } }
    public XRRayInteractor LRayInteractor { get { return leftRayInteractor; } private set { leftRayInteractor = value; } }
    public XRRayInteractor RUIController { get { return rightUIInteractor; } private set { rightUIInteractor = value; } }
    public XRRayInteractor LUIController { get { return leftUIInteractor; } private set { leftUIInteractor = value; } }

}
