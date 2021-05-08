using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VideoTutorialDescriptions : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private TextMeshProUGUI textMesh = null;
    [SerializeField] private GameObject teleport = null;
    [SerializeField] private GameObject grab = null;
    [SerializeField] private GameObject distanceGrab = null;

    [Header("Descriptions w/ video")]
    [TextArea] [SerializeField] private string teleportDescription = "";
    [TextArea] [SerializeField] private string grabDescription = "";
    [TextArea] [SerializeField] private string distanceGrabDescription = "";

    [Header("Descriptions w/out video")]
    [TextArea] [SerializeField] private string helpDescription = "";
    [TextArea] [SerializeField] private string undoDescription = "";
    [TextArea] [SerializeField] private string prismDescription = "";
    [TextArea] [SerializeField] private string mosDescription = "";


    private void Awake()
    {
        textMesh.text = "";
    }

    public void SetTutorialDescription(string parameter)
    {
        if (parameter.Equals("teleport"))
        {
            textMesh.text = teleportDescription;
            
            teleport.SetActive(true);
            grab.SetActive(false);
            distanceGrab.SetActive(false);
        }
        else if (parameter.Equals("grab"))
        {
            textMesh.text = grabDescription;

            teleport.SetActive(false);
            grab.SetActive(true);
            distanceGrab.SetActive(false);
        }
        else if (parameter.Equals("distanceGrab"))
        {
            textMesh.text = distanceGrabDescription;

            teleport.SetActive(false);
            grab.SetActive(false);
            distanceGrab.SetActive(true);
        }
        else if (parameter.Equals("help"))
        {
            textMesh.text = helpDescription;

            teleport.SetActive(false);
            grab.SetActive(false);
            distanceGrab.SetActive(false);
        }
        else if (parameter.Equals("undo"))
        {
            textMesh.text = undoDescription;

            teleport.SetActive(false);
            grab.SetActive(false);
            distanceGrab.SetActive(false);
        }
        else if (parameter.Equals("prism"))
        {
            textMesh.text = prismDescription;

            teleport.SetActive(false);
            grab.SetActive(false);
            distanceGrab.SetActive(false);
        }
        else if (parameter.Equals("mos"))
        {
            textMesh.text = mosDescription;

            teleport.SetActive(false);
            grab.SetActive(false);
            distanceGrab.SetActive(false);
        }
        else
        {
            Debug.LogError("VideoTutorialDescription_SetTutorialDescription(): wrong parameter name");
        }
    }

}
