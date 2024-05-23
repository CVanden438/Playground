using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatValues
{
    public StatType type;
    public float value;
}

[CreateAssetMenu(fileName = "Armour", menuName = "Item/Armour")]
public class ArmourSO : EquipmentSO
{
    public float baseDefence;
}
