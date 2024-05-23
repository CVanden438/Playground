using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    helm,
    chest,
    legs,
    boots,
    jewellery,
    weapon,
    spell
}

// [CreateAssetMenu(fileName = "Item", menuName = "Item")]
public class EquipmentSO : ItemSO
{
    public EquipmentType equipmentType;
    public List<StatValues> stats;
}
