using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enumerators;

public class BondInfo : MonoBehaviour
{
    public BondType bondType;

    public Transform atom1;
    public Transform atom2;

    public bool AlreadyAssigned { get; set; } = false;
}
