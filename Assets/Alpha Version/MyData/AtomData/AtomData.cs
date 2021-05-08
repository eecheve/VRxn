using UnityEngine;
using static Enumerators;

[CreateAssetMenu()]
public class AtomData : ScriptableObject
{
    public AtomType atom;
    public AtomSymbol symbol;
    public string[] bondType;
    public float[] bondLength;
    public float atomicMass;
}
