using UnityEngine;
using System.Linq;

public class MolecularInfoExtractor : MonoSingleton<MolecularInfoExtractor>
{
    public MoleculeData LeftMoleculeData { get; set; }
    public MoleculeData RightMoleculeData { get; set; }

    private void OnEnable()
    {
        GameManager.OnLeftFirstGrab += ManageLeftGrabInformation;
        GameManager.OnLeftHasSwapped += ManageLeftGrabInformation;

        GameManager.OnRightFirstGrab += ManageRightGrabInformation;
        GameManager.OnRightHasSwapped += ManageRightGrabInformation;
    }

    public void ManageLeftGrabInformation()
    {
        if(GameManager.Instance.LeftGrabbedParent != null)
        {
            string moleculeName = GameManager.Instance.LeftGrabbedParent.name;
            LeftMoleculeData = GetMolecularDataFromName(moleculeName);
            Debug.Log(LeftMoleculeData.name);
        }
    }

    public void ManageRightGrabInformation()
    {
        if (GameManager.Instance.RightGrabbedParent != null)
        {
            string moleculeName = GameManager.Instance.RightGrabbedParent.name;
            RightMoleculeData = GetMolecularDataFromName(moleculeName);
        }
    }

    private MoleculeData GetMolecularDataFromName(string name)
    {
        return MoleculeDataList.Instance.MoleculeList.Where(obj => obj.name == name).SingleOrDefault();
    }

    private void OnDisable()
    {
        GameManager.OnLeftFirstGrab -= ManageLeftGrabInformation;
        GameManager.OnLeftHasSwapped -= ManageLeftGrabInformation;

        GameManager.OnRightFirstGrab -= ManageRightGrabInformation;
        GameManager.OnRightHasSwapped -= ManageRightGrabInformation;
    }
}
