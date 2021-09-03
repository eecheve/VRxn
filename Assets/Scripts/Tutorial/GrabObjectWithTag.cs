using UnityEngine;

public class GrabObjectWithTag : Condition
{
    [SerializeField] private string m_tag = "";
    
    private SnapMeshToTransform snapMesh;
    private UpdateTextAfterCondition updateText;

    protected override void Awake()
    {
        base.Awake();
        snapMesh = GetComponent<SnapMeshToTransform>();
        updateText = GetComponent<UpdateTextAfterCondition>();
    }

    private void OnEnable()
    {
        Debug.Log(name + "Grab enabled");
        
        GameManager.OnLeftFirstGrab += LeftObjectGrabbed;
        GameManager.OnRightFirstGrab += RightObjectGrabbed;

        GameManager.OnLeftHasSwapped += LeftObjectGrabbed;
        GameManager.OnRightHasSwapped += RightObjectGrabbed;
    }

    private void LeftObjectGrabbed()
    {
        Transform grabbed = GameManager.Instance.LeftGrabbed;
        ObjectGrabbed(grabbed);
    }

    private void RightObjectGrabbed()
    {
        Transform grabbed = GameManager.Instance.RightGrabbed;
        ObjectGrabbed(grabbed);
    }

    private void ObjectGrabbed(Transform grabbed)
    {
        Debug.Log("GrabObjectWithTag: grabbing " + grabbed.name);
        if (grabbed.CompareTag(m_tag))
        {
            if (snapMesh != null)
            {
                snapMesh.Object = grabbed;
                snapMesh.SnapMesh();
            }

            if (updateText != null)
                updateText.UpdateText(true);

            FulfillCondition(); 
        }
        else
        {
            if(updateText != null)
                updateText.UpdateText(false);
        }
    }

    protected override void FulfillCondition()
    {
        base.FulfillCondition();
        Debug.Log($"Fulfilling condition in {name}");
    }

    private void OnDisable()
    {
        GameManager.OnLeftFirstGrab -= LeftObjectGrabbed;
        GameManager.OnRightFirstGrab -= RightObjectGrabbed;

        GameManager.OnLeftHasSwapped -= LeftObjectGrabbed;
        GameManager.OnRightHasSwapped -= RightObjectGrabbed;
    }
}
