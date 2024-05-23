using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    melee,
    range,
    magic
}

[CreateAssetMenu(fileName = "Wep", menuName = "Weapon")]
public class WeaponSO : EquipmentSO
{
    public float damage;
    public float range;
    public float cooldown;
    public int repeat = 0;
    public int proj = 1;
    public WeaponType weaponType;
}
