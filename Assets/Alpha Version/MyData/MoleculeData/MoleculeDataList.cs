using System.Collections.Generic;
using UnityEngine;

public class MoleculeDataList : MonoSingleton<MoleculeDataList>
{
    [SerializeField] private List<MoleculeData> moleculeList = new List<MoleculeData>();
    public List<MoleculeData> MoleculeList { get { return moleculeList; } }

    public void InitiallizeDictionaries()
    {
        foreach (var moleculeData in moleculeList)
        {
            moleculeData.InitiallizeDicts();
        }
    }
}
