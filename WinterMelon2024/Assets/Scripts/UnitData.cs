using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "New Data Object/ New Unit Data")]
public class UnitData : ScriptableObject, IComparable<UnitData>
{
    public int baseCost;
    public float baseCooldown;
    public Sprite unitArt;
    public Unit unit;

    public int CompareTo(UnitData _other)
    {
        return baseCost - _other.baseCost;
    }
}
