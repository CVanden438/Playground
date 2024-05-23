using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpell
{
    // SpellSO Data { get; set; }
    public void Cast(Vector3 spawnPos, Vector3 targetPos, GameObject caster);
}

public class ProjectileSpell : MonoBehaviour, ISpell
{
    public SpellSO data;
    public GameObject projectile;

    public virtual void Cast(Vector3 spawnPos, Vector3 targetPos, GameObject caster)
    {
        var proj = Instantiate(projectile, spawnPos, Quaternion.identity);
        proj.GetComponent<HomingProjectile>().targetPos = targetPos;
        proj.GetComponent<HomingProjectile>().data = data;
    }
}
