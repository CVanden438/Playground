using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpellType
{
    single,
    drain
}

[CreateAssetMenu(fileName = "Spell", menuName = "Spell")]
public class SpellSO : ScriptableObject
{
    public string spellName;
    public string spellDescription;
    public float damage;
    public int manaCost;
    public float cooldown;
    public float range;
    public int repeat = 0;
    public SpellType spellType;
    public GameObject prefab;
}
