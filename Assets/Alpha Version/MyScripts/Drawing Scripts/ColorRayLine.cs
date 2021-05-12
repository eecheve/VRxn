using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRInteractorLineVisual))]
[RequireComponent(typeof(MoveAndRotate2D))]
public class ColorRayLine : MonoBehaviour
{
    [SerializeField] private LayerMask drawLayerMask = 0;
    [SerializeField] private LayerMask grabLayerMask = 0;

    //public bool CanDraw { get; private set; } = false;
    //public bool CanGrab { get; private set; } = false;

    private XRInteractorLineVisual lineVisual;
    private Gradient defaultGradient;

    private Gradient drawingGradient;
    private GradientColorKey[] drawColorKeys;
    private GradientAlphaKey[] drawAlphaKeys;

    private Gradient grabbingGradient;
    private GradientColorKey[] grabColorKeys;
    private GradientAlphaKey[] grabAlphaKeys;

    private MoveAndRotate2D moveRotate;

    private void Awake()
    {
        lineVisual = GetComponent<XRInteractorLineVisual>();
        moveRotate = GetComponent<MoveAndRotate2D>();

        defaultGradient = lineVisual.validColorGradient;

        drawingGradient = new Gradient();
        drawColorKeys = new GradientColorKey[3];
        drawAlphaKeys = new GradientAlphaKey[3];

        grabbingGradient = new Gradient();
        grabColorKeys = new GradientColorKey[3];
        grabAlphaKeys = new GradientAlphaKey[3];

        SetGradientColor(drawingGradient, drawColorKeys, drawAlphaKeys, Color.cyan);
        SetGradientColor(grabbingGradient, grabColorKeys, grabAlphaKeys, Color.magenta);
    }

    private void Update()
    {
        RaycastLogic();
    }

    private void RaycastLogic()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (moveRotate.GrabIconEnabled == false)
        {
            if (Physics.Raycast(ray, 100f, drawLayerMask))
            {
                if (lineVisual.validColorGradient != drawingGradient)
                    lineVisual.validColorGradient = drawingGradient;
            }

            if (!Physics.Raycast(ray, 100f, drawLayerMask) && !Physics.Raycast(ray, out hit, 100f, grabLayerMask))
            {
                if (lineVisual.validColorGradient != defaultGradient)
                    lineVisual.validColorGradient = defaultGradient;
            }
        }
        else
        {
            if (Physics.Raycast(ray, out hit, 100f, grabLayerMask))//<-- two different types of Physics.Raycast
            {
                if (lineVisual.validColorGradient != grabbingGradient)
                    lineVisual.validColorGradient = grabbingGradient;
            }
            
            if (!Physics.Raycast(ray, 100f, drawLayerMask) && !Physics.Raycast(ray, out hit, 100f, grabLayerMask))
            {
                if (lineVisual.validColorGradient != defaultGradient)
                    lineVisual.validColorGradient = defaultGradient;
            }
        }
    }
   
    private void SetGradientColor(Gradient gradient, GradientColorKey[] colorKeys, GradientAlphaKey[] alphaKeys, Color color2)
    {
        colorKeys[0].time = 0f;
        colorKeys[0].color = Color.white;
        colorKeys[1].time = 0.3f;
        colorKeys[1].color = color2;
        colorKeys[2].time = 1.0f;
        colorKeys[2].color = color2;

        alphaKeys[0].time = 0f;
        alphaKeys[0].alpha = 0f;
        alphaKeys[1].time = 0.3f;
        alphaKeys[1].alpha = 1.0f;
        alphaKeys[2].time = 1.0f;
        alphaKeys[2].alpha = 1.0f;

        gradient.SetKeys(colorKeys, alphaKeys);
    }

    public void ChangeLineColor(string colorName)
    {
        if (ColorUtility.TryParseHtmlString(colorName, out Color color))
        {
            SetGradientColor(drawingGradient, drawColorKeys, drawAlphaKeys, color);
        }
        else
        {
            Debug.LogError("ColorRayLine_ChangeLineColor(): invalid color name");
        }
    }

    public void RevertLineColorToDefault()
    {
        lineVisual.validColorGradient = defaultGradient;
    }
}
