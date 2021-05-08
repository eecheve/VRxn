using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static Enumerators;
using System;
using Unity.XR;

public class ConnectAssign : MonoBehaviour
{
    #region singleton
    private static ConnectAssign _instance;
    public static ConnectAssign Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("ConnectAssign is not in the scene");

            return _instance;
        }
    }
    #endregion

    [SerializeField] private Material carbonMat = null;
    [SerializeField] private Material altCarbonMat = null;

    private Transform[] childTransforms;

    public List<Transform> AtomList { get; private set; } = new List<Transform>();
    public List<Transform> BondList { get; private set; } = new List<Transform>();

    public void LoadInformation()
    {
        AtomList.Clear();
        BondList.Clear();

        _instance = this;
        childTransforms = ChildrenTransformGetter.Instance.ObjectTransforms;
    }

    public void AssignCollider()
    {
        foreach (var child in childTransforms)
        {
            if (child.name == ChildrenTransformGetter.Instance.objectToModify.name)
            {
                continue;
            }
            else if (child.name.Contains("-"))
            {
                AddBoxCollider(child);
                if (!BondList.Contains(child))
                    BondList.Add(child);
            }
            else if (child.name.Contains("="))
            {
                AddBoxCollider(child);
                if (!BondList.Contains(child))
                    BondList.Add(child);
            }
            else if (child.name.Contains("#"))
            {
                AddBoxCollider(child);
                if (!BondList.Contains(child))
                    BondList.Add(child);
            }
            else
            {
                AddSphereCollider(child);
                AddXRGrabOffset(child);
                if (!AtomList.Contains(child))
                    AtomList.Add(child);
            }
        }
    }

    public void AssignConnectivity()
    {
        foreach (var atom in AtomList)
        {
            Connectivity connectivity = atom.gameObject.GetComponent<Connectivity>();
            if (connectivity == null)
            {
                atom.gameObject.AddComponent<Connectivity>();
            }
            else
            {
                Debug.Log("Connectivity has already been assigned");
            }

        }
    }

    public void RemoveConnectivity()
    {
        foreach (var atom in AtomList)
        {
            Connectivity connectivity = atom.gameObject.GetComponent<Connectivity>();
            if (connectivity != null)
            {
                DestroyImmediate(connectivity);
            }
        }
    }

    private void AddSphereCollider(Transform objTransform)
    {
        if (objTransform.gameObject.GetComponent<SphereCollider>() == null)
        {
            objTransform.gameObject.AddComponent<SphereCollider>();
        }
        else
        {
            Debug.Log("Sphere collider already added");
        }
    }

    private void AddBoxCollider(Transform objTransform)
    {
        if (objTransform.gameObject.GetComponent<BoxCollider>() == null)
        {
            objTransform.gameObject.AddComponent<BoxCollider>();
        }
        else
        {
            Debug.Log("Box collider already added");
        }
    }

    private void AddXRGrabOffset(Transform objTransform)
    {
        if (objTransform.gameObject.GetComponent<XROffsetGrabbable>() == null)
        {
            objTransform.gameObject.AddComponent<XROffsetGrabbable>();
        }
        else
        {
            Debug.Log("XROffsetGrabbable already added");
        }
    }

    public void RemoveSphereColliders()
    {
        foreach (var child in childTransforms)
        {
            SphereCollider collider = child.gameObject.GetComponent<SphereCollider>();
            if (collider != null)
            {
                DestroyImmediate(collider);
            }
        }
    }

    public void RemoveBoxColliders()
    {
        foreach (var child in childTransforms)
        {
            BoxCollider collider = child.gameObject.GetComponent<BoxCollider>();
            if (collider != null)
            {
                DestroyImmediate(collider);
            }
        }
    }

    public void RemoveAllRigidbodies()
    {
        foreach (var item in childTransforms)
        {
            Rigidbody rb = item.GetComponent<Rigidbody>();
            if (rb != null)
            {
                DestroyImmediate(rb);
            }
        }
    }

    public void SetupAtomRigidbodies()
    {
        foreach (var atom in AtomList)
        {
            Rigidbody rb = atom.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;
                rb.drag = 5f;
                rb.angularDrag = 5f;
            }
        }
    }

    public void SetupAtomConnectivities()
    {
        List<Transform> singleBonds = new List<Transform>();
        List<Transform> doubleBonds = new List<Transform>();
        List<Transform> aromBonds = new List<Transform>();
        List<Transform> tripleBonds = new List<Transform>();

        foreach (var bond in BondList)
        {
            if (bond.name.Contains("-") && !bond.name.Contains("="))
                singleBonds.Add(bond);
            else if (bond.name.Contains("-") && bond.name.Contains("="))
                aromBonds.Add(bond);
            else if (bond.name.Contains("=") && !bond.name.Contains("#"))
                doubleBonds.Add(bond);
            else if (bond.name.Contains("=") && bond.name.Contains("#"))
                aromBonds.Add(bond);
            else if (bond.name.Contains("#") && !bond.name.Contains("="))
                tripleBonds.Add(bond);
        }

        foreach (var atom in AtomList)
        {
            Connectivity connect = atom.GetComponent<Connectivity>();
            AssignConnectivityAtomLabel(atom, connect);

            foreach (var singleBond in singleBonds)
            {
                int pos = singleBond.name.IndexOf('-');
                string atom1 = singleBond.name.Remove(0, pos + 1);
                string atom2 = singleBond.name.Remove(pos, singleBond.name.Length - pos);

                AssignAtomConnectivityByName(atom, connect, atom1, atom2);
                AddBondInfoComponent(singleBond, BondType.SingleBond);
                PopulateBondInfo(singleBond, atom1, atom2);
            }
            foreach (var aromBond in aromBonds)
            {
                int pos = aromBond.name.IndexOf('-');
                string atom1 = aromBond.name.Remove(0, pos + 1);
                int posDot = atom1.IndexOf('.');
                if (posDot != -1)
                    atom1 = atom1.Remove(posDot, atom1.Length - posDot);
                atom1 = atom1.Trim(new char[] { '=' });

                int pos2 = aromBond.name.IndexOf('=');
                string atom2 = aromBond.name.Remove(pos2, aromBond.name.Length - pos2);
                atom2 = atom2.Trim(new char[] { '-' });

                AssignAtomConnectivityByName(atom, connect, atom1, atom2);
                AddBondInfoComponent(aromBond, BondType.AromaticBond);
                PopulateBondInfo(aromBond, atom1, atom2);
            }
            foreach (var doubleBond in doubleBonds)
            {
                int pos = doubleBond.name.IndexOf('=');
                string atom1 = doubleBond.name.Remove(0, pos + 1);
                int posDot1 = atom1.IndexOf('.');
                if (posDot1 != -1)
                    atom1 = atom1.Remove(posDot1, atom1.Length - posDot1);

                string atom2 = doubleBond.name.Remove(pos, doubleBond.name.Length - pos);
                int posDot2 = atom2.IndexOf('.');
                if (posDot2 != -1)
                    atom2 = atom2.Remove(posDot2, atom2.Length - posDot2);

                AssignAtomConnectivityByName(atom, connect, atom1, atom2);
                AddBondInfoComponent(doubleBond, BondType.DoubleBond);
                PopulateBondInfo(doubleBond, atom1, atom2);
            }
            foreach (var tripleBond in tripleBonds)
            {
                int pos = tripleBond.name.IndexOf('#');
                string atom1 = tripleBond.name.Remove(0, pos + 1);
                int posDot1 = atom1.IndexOf('.');
                if (posDot1 != -1)
                    atom1 = atom1.Remove(posDot1, atom1.Length - posDot1);

                string atom2 = tripleBond.name.Remove(pos, tripleBond.name.Length - pos);
                int posDot2 = atom2.IndexOf('.');
                if (posDot2 != -1)
                    atom2 = atom2.Remove(posDot2, atom2.Length - posDot2);

                AssignAtomConnectivityByName(atom, connect, atom1, atom2);
                AddBondInfoComponent(tripleBond, BondType.TripleBond);
                PopulateBondInfo(tripleBond, atom1, atom2);
            }
            AssignAtomElectronicGeometry(connect);
            AddGraffOffsetComponent(atom);
        }
    }

    private void AssignConnectivityAtomLabel(Transform atom, Connectivity connect)
    {
        if (atom.gameObject.CompareTag("DieneEnd"))
            connect.atomLabel = AtomLabel.DieneEnd;

        else if (atom.gameObject.CompareTag("Dienophile"))
            connect.atomLabel = AtomLabel.Dienophile;

        else
            connect.atomLabel = AtomLabel.Backbone;

        atom.gameObject.tag = "Molecule";
    }

    private void AddGraffOffsetComponent(Transform atom)
    {
        if (atom.gameObject.GetComponent<XROffsetGrabbable>() == null)
        {
            atom.gameObject.AddComponent<XROffsetGrabbable>();
        }
    }

    private void PopulateBondInfo(Transform bond, string atom1, string atom2)
    {
        if (bond.gameObject.GetComponent<BondInfo>() != null)
        {
            BondInfo bondInfo = bond.gameObject.GetComponent<BondInfo>();
            if (bondInfo.AlreadyAssigned == false)
            {
                Transform a1 = AtomList.Where(obj => obj.name == atom1).SingleOrDefault();
                Transform a2 = AtomList.Where(obj => obj.name == atom2).SingleOrDefault();

                bondInfo.atom1 = a1;
                bondInfo.atom2 = a2;
                bondInfo.AlreadyAssigned = true;
            }
        }
    }

    private void AssignAtomConnectivityByName(Transform atom, Connectivity connect, string atom1, string atom2)
    {
        if (atom1.Equals(atom.name))
        {
            Transform t = AtomList.Where(obj => obj.name == atom2).SingleOrDefault();
            if (connect.connectedAtoms.Contains(t) == false)
            {
                connect.connectedAtoms.Add(t);
            }
        }
        else if (atom2.Equals(atom.name))
        {
            Transform t = AtomList.Where(obj => obj.name == atom1).SingleOrDefault();
            if (connect.connectedAtoms.Contains(t) == false)
            {
                connect.connectedAtoms.Add(t);
            }
        }
    }

    private void AddBondInfoComponent(Transform bond, BondType bondType)
    {
        if (bond.gameObject.GetComponent<BondInfo>() == null)
        {
            bond.gameObject.AddComponent<BondInfo>();
            bond.gameObject.GetComponent<BondInfo>().bondType = bondType;
        }
    }
    private void AssignAtomElectronicGeometry(Connectivity connect)
    {
        switch (connect.connectedAtoms.Count)
        {
            case 1:
                connect.eGeometry = EGeometry.Terminal;
                break;
            case 2:
                connect.eGeometry = EGeometry.Linear;
                break;
            case 3:
                connect.eGeometry = EGeometry.TrigonalPlanar;
                break;
            case 4:
                connect.eGeometry = EGeometry.Tetrahedral;
                break;
            case 5:
                connect.eGeometry = EGeometry.TrigonalBipyramidal;
                break;
            case 6:
                connect.eGeometry = EGeometry.Octahedral;
                break;
            default:
                connect.eGeometry = EGeometry.Terminal;
                break;
        }
    }

    public void AssignFixedJoints()
    {
        List<(Transform, Transform)> atomPairs = GetNonRedundantAtomPairs(); //<-- BUG: some of the fjs are wrong!!

        foreach (var pair in atomPairs)
        {
            Transform atomToAssign = AtomList.Where(obj => obj.name == pair.Item1.name).SingleOrDefault();
            Transform atomConnected = AtomList.Where(obj => obj.name == pair.Item2.name).SingleOrDefault();
            FixedJoint fj = atomToAssign.gameObject.AddComponent<FixedJoint>();
            fj.enablePreprocessing = false;
            fj.connectedBody = atomConnected.gameObject.GetComponent<Rigidbody>();
        }
    }

    public void ParentBondsToAtoms()
    {
        foreach (var bond in BondList)
        {
            int index = 0;
            for (int i = 0; i < bond.name.Length; i++)
            {
                if (char.IsLetter(bond.name, i) || char.IsNumber(bond.name, i))
                    index = i;
                else
                    break;
            }
            string atomLabel = bond.name.Remove(index + 1); //<--- Parents the objects to the first atom
            Transform theParent = AtomList.Where(obj => obj.name == atomLabel).SingleOrDefault();
            bond.parent = theParent;
        }
    }

    public void ResetParents()
    {
        foreach (var bond in BondList)
        {
            bond.parent = childTransforms[0];
        }
    }

    public List<(Transform, Transform)> GetNonRedundantAtomPairs()
    {
        List<(Transform, Transform)> atomPairs = new List<(Transform, Transform)>();

        foreach (var atom in AtomList)
        {
            Connectivity connect = atom.GetComponent<Connectivity>();
            List<Transform> connectedAtoms = connect.connectedAtoms;
            foreach (var connectedAtom in connectedAtoms)
            {
                atomPairs.Add((atom, connectedAtom));
            }
        }
        for (int i = 0; i < atomPairs.Count; i++)
        {
            for (int j = i + 1; j < atomPairs.Count; j++)
            {
                if (atomPairs[i].Item1 == atomPairs[j].Item2)
                {
                    if (atomPairs[i].Item2 == atomPairs[j].Item1)
                    {
                        atomPairs.RemoveAt(j);
                    }
                }
            }
        }
        return atomPairs;
    }

    public void RemoveAllFixedJoints()
    {
        foreach (var atom in AtomList)
        {
            FixedJoint[] fjs = atom.GetComponents<FixedJoint>();
            if (fjs != null)
            {
                foreach (var fj in fjs)
                {
                    DestroyImmediate(fj);
                }
            }
        }
    }

    public void RemoveXROffsetGrabbables()
    {
        foreach (var atom in AtomList)
        {
            XROffsetGrabbable offset = atom.GetComponent<XROffsetGrabbable>();
            if (offset != null)
            {
                DestroyImmediate(offset);
            }
        }
    }

    public void RemoveBondInfoComponents()
    {
        foreach (var bond in BondList)
        {
            BondInfo info = bond.GetComponent<BondInfo>();
            if (info != null)
            {
                DestroyImmediate(info);
            }
        }
    }

    public void ReassignCarbonMaterial()
    {
        foreach (var child in childTransforms)
        {
            if (child.GetComponent<MeshRenderer>() != null)
            {
                Material[] materials = child.GetComponent<MeshRenderer>().sharedMaterials;

                if(materials != null)
                {
                    if (materials[0].Equals(carbonMat))
                    {
                        //Debug.Log("ConnectAssign: " + child.name + " has carbon material");
                        if(materials.Length > 1)
                        {
                            Debug.Log("ConnectAssign: " + child.name + " has two materials");
                            Material[] newMats = new Material[2];
                            newMats[0] = altCarbonMat;
                            newMats[1] = materials[1];
                            child.GetComponent<MeshRenderer>().sharedMaterials = newMats;
                        }
                        else
                        {
                            Debug.Log("ConnectAssign: " + child.name + " only has one material");
                            Material[] newMats = new Material[1];
                            newMats[0] = altCarbonMat;
                            child.GetComponent<MeshRenderer>().sharedMaterials = newMats;
                        }
                    }
                }
            }
        }
    }

    public void ClearLists()
    {
        if (AtomList != null)
            AtomList.Clear();
        if (BondList != null)
            BondList.Clear();
    }

    private void OnDestroy()
    {
        ClearLists();
    }
}
