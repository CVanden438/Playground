using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    // general,
    // helm,
    // chest,
    // legs,
    // boots,
    // jewellery,
    // weapon,
    // spell
    material,
    equipment
}

[CreateAssetMenu(fileName = "Item", menuName = "Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite sprite;
    public ItemType itemType;
}
