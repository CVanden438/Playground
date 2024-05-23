using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireblastSpell : MonoBehaviour, ISpell
{
    public SpellSO data;
    public GameObject explosionAnimation;

    public void Cast(Vector3 spawnPos, Vector3 targetPos, GameObject caster)
    {
        var hits = Physics2D.OverlapCircleAll(targetPos, 2);
        Instantiate(explosionAnimation, targetPos, Quaternion.identity);
    }
}
