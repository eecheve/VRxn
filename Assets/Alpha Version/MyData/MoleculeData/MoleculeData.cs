using System;
using System.Collections.Generic;
using UnityEngine;
using static Enumerators;

[CreateAssetMenu()]
[ExecuteInEditMode]
public class MoleculeData : ScriptableObject
{
    [Serializable] public struct OrbitalEnergy
    {
        public OrbitalType orbital;
        public float energy;
    }
    [Serializable] public struct OrbitalCoefficient
    {
        public OrbitalType orbital;
        public float[] coefficients;
    }
    [Serializable] public struct OrbitalDepiction
    {
        public OrbitalType orbital;
        public Sprite image;
    }


    [SerializeField] private string moleculeName = "";
    [SerializeField] private Sprite moleculeSprite = null;
    [SerializeField] private bool rotatable = false;
    [SerializeField] private float[] rotationBarrier = null;
    [SerializeField] private OrbitalEnergy[] orbitalEnergies = null;
    [SerializeField] private OrbitalCoefficient[] orbitalCoeffs = null;
    [SerializeField] private OrbitalDepiction[] orbitalDepictions = null;

    public string MoleculeName { get; private set;  }
    public Sprite MoleculeSprite { get; private set; }
    public bool Rotatable { get { return rotatable; } }
    public float[] RotationBarrier { get { return rotationBarrier; } }
    public Dictionary<OrbitalType, float> OrbitalEnergyDict { get; private set; }
    public Dictionary<OrbitalType, float[]> OrbitalCoeffsDict { get; private set; }
    public Dictionary<OrbitalType, Sprite> OrbitalDepictions { get; private set; }

    public void InitiallizeDicts()
    {
        //PopulateEnergyDict();
        //PopulateCoeffsDict();
    }

    public void Awake()
    {
        PopulateEnergyDict();
        PopulateCoeffsDict();
        PopulateOrbitalDepDict();

        MoleculeName = moleculeName;
        MoleculeSprite = moleculeSprite;
    }

    private void PopulateEnergyDict()
    {
        OrbitalEnergyDict = new Dictionary<OrbitalType, float>();
        foreach (var orbitalEnergy in orbitalEnergies)
        {
            if (!OrbitalEnergyDict.ContainsKey(orbitalEnergy.orbital))
            {
                OrbitalEnergyDict.Add(orbitalEnergy.orbital, orbitalEnergy.energy);
            }
        }
    }

    private void PopulateCoeffsDict()
    {
        OrbitalCoeffsDict = new Dictionary<OrbitalType, float[]>();
        foreach (var orbitalCoeff in orbitalCoeffs)
        {
            if (!OrbitalCoeffsDict.ContainsKey(orbitalCoeff.orbital))
            {
                OrbitalCoeffsDict.Add(orbitalCoeff.orbital, orbitalCoeff.coefficients);
            }
        }
    }

    private void PopulateOrbitalDepDict()
    {
        OrbitalDepictions = new Dictionary<OrbitalType, Sprite>();
        foreach (var orbitalDepict in orbitalDepictions)
        {
            if (!OrbitalDepictions.ContainsKey(orbitalDepict.orbital))
            {
                OrbitalDepictions.Add(orbitalDepict.orbital, orbitalDepict.image);
            }
        }
    }
}
