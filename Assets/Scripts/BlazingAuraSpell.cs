using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlazingAuraSpell : MonoBehaviour, ISpell
{
    public GameObject blazingAura;
    GameObject activeAura;
    public SpellSO data;

    public void Cast(Vector3 spawnPos, Vector3 targetPos, GameObject caster)
    {
        if (!activeAura)
        {
            activeAura = Instantiate(blazingAura, caster.transform);
        }
        else
        {
            Destroy(activeAura);
        }
    }
}
