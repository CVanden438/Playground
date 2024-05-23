using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkSpell : MonoBehaviour, ISpell
{
    public SpellSO data;

    public void Cast(Vector3 spawnPos, Vector3 targetPos, GameObject caster)
    {
        // Teleport the player to the target position
        caster.transform.position = targetPos;
    }
}
