using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // public Stat maxHP;
    // public Stat defence;
    public List<Stat> stats;

    void Awake()
    {
        stats.Add(new Stat(100, StatType.health));
        stats.Add(new Stat(0, StatType.defence));
        stats.Add(new Stat(10, StatType.movespeed));
    }
}
